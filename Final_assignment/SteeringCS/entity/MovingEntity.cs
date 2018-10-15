using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.behaviour;
using SteeringCS.util;
using SteeringCS.util.fuzzy_logic;
using SteeringCS.util.goals;
using SteeringCS.util.sprites;

namespace SteeringCS.entity
{
    public enum CollisionDirection { DEFAULT, NEGATIVE_X, POSITIVE_X, NEGATIVE_Y, POSITIVE_Y };
    public enum EntityDirection { LEFT, RIGHT, UP, DOWN, UNKNOWN }
    public abstract class MovingEntity : BaseGameEntity
    {
        public Vector2D Velocity { get; set; }
        public Vector2D Ahead { get; set; }
        public Vector2D Heading { get; set; }
        public Vector2D Avoidance { get; set; }

        public Goal Goal { get; set; }

        public GoalThink Brain { get; set; }

        public bool TargetFound { get; set; }
        
        public bool CarriesDeadFish { get; set; }

        public int Hunger { get; set; }

        public FuzzyModule FuzzyModule { get; set; }

        // target position, used within several steering behaviours
        public Vector2D Target { get; set; }

        public float ExploreRadius { get; set; }

        public double Angle
        {
            get
            {
                return 90 + (180 * GetAngle(Velocity)) / Math.PI;
            }
        }

        public EntityDirection Direction
        {
            get
            {
                var direction = EntityDirection.UNKNOWN;
                var angle = Angle;

                // because we have fish only, we allow the LEFT - RIGHT directions only
                if ((angle >= 0 && angle < 180))
                    direction = EntityDirection.RIGHT;

                if (angle < 0 || angle >= 180)
                    direction = EntityDirection.LEFT;

                // UP - DOWN - LEFT - RIGHT
                //if ((angle >= 0 && angle <= 45) || (angle <= 0 && angle >= -45))
                //    direction = EntityDirection.UP;
                //if (angle >= 135 && angle <= 225)
                //    direction = EntityDirection.DOWN;
                //if (angle >= 45 && angle <= 135)
                //    direction = EntityDirection.RIGHT;
                //if (angle >= 225 && angle <= 270 || angle <= -45 && angle >= -90)
                //    direction = EntityDirection.LEFT;
                return direction;
            }
        }

        public float Mass { get; set; }
        public float MaxSpeed { get; set; }
        public float SlowingRadius { get; set; }
        public float WanderAngle { get; set; }
        public const float MAX_FORCE = 5.4f;
        public const float CIRCLE_DISTANCE = 6f;
        public const float CIRCLE_RADIUS = 8f;
        public const float ANGLE_CHANGE = 5f;
        public const float MAX_SEE_AHEAD = 40f;
        public const float AVOID_FORCE = 100f;

        public SBPath Path;

        public SteeringBehaviour SB { get; set; }

        public MovingEntity(Vector2D pos, World w, ISpriteMode strategy = null) : base(pos, w)
        {
            Mass = 10;
            MaxSpeed = 10;
            SlowingRadius = 2;
            WanderAngle = 0;
            Velocity = new Vector2D();
            Ahead = new Vector2D();
            Heading = new Vector2D();
            Avoidance = new Vector2D();
            Target = new Vector2D();

            FuzzyModule = new FuzzyModule();

            // default: doesn't carry any fish
            CarriesDeadFish = false;

            // path following
            Path = null;

            // sprite mode
            if (strategy == null)
                SpriteStrategy = new FillCircle(); // defaults to outline circle
            else
                SpriteStrategy = strategy;
        }

    /*
     * Check for collision.
     * TODO: extend function with circle based collision detection (line intersects circle)
     */
    private bool IsCollision()
        {
            return Pos.X + 5 >= MyWorld.Width || Pos.Y + 5 >= MyWorld.Width || Pos.X + 5 <= 0 || Pos.Y + 5 <= 0;
        }

        private Tuple<bool, CollisionDirection> CheckCollision()
        {
            if (Pos.X + 5 >= MyWorld.Width)
                return new Tuple<bool, CollisionDirection>(true, CollisionDirection.POSITIVE_X);
            else if (Pos.X - 5 <= 0)
                return new Tuple<bool, CollisionDirection>(true, CollisionDirection.NEGATIVE_X);
            else if (Pos.Y + 5 >= MyWorld.Width)
                return new Tuple<bool, CollisionDirection>(true, CollisionDirection.POSITIVE_Y);
            else if (Pos.Y - 5 <= 0)
               return new Tuple<bool, CollisionDirection>(true, CollisionDirection.NEGATIVE_Y);
            return new Tuple<bool, CollisionDirection>(false, CollisionDirection.DEFAULT);
        }

        protected double GetAngle(Vector2D vector)
        {
            return Math.Atan2(vector.Y, vector.X);
        }

        #region Obstacle avoidance helper methods
        private bool LineIntersectsRectangle(Vector2D position, Vector2D ahead, Rectangle r)
        {
            var vector = Velocity.Clone();
            vector.Normalize();
            vector.Multiply(MAX_SEE_AHEAD * 0.5 * Velocity.Length() / MaxSpeed);

            var ahead2 = position.Clone().Add(vector);
            return IsInsideRectangle(ahead2, r) || IsInsideRectangle(ahead2, r) || IsInsideRectangle(Pos, r);
        }

        private bool IsInsideRectangle(Vector2D vec, Rectangle rect)
        {
            return vec.X >= rect.X && vec.X <= (rect.X + rect.Width) && vec.Y >= rect.Y && vec.Y <= (rect.Y + rect.Height);
        }

        private bool LineIntersectsCircle(Vector2D position, Vector2D ahead, UtilCircle circle)
        {
            var vector = Velocity.Clone();
            vector.Normalize();
            vector.Multiply(MAX_SEE_AHEAD * 0.5 * Velocity.Length() / MaxSpeed);

            var ahead2 = position.Clone().Add(vector);
            return EntityHelper.Distance(circle.Pos, ahead) <= circle.Radius + (Scale + 5) || EntityHelper.Distance(circle.Pos, ahead2) <= circle.Radius + (Scale + 5) || EntityHelper.Distance(circle.Pos, Pos) <= circle.Radius + (Scale + 5);
        }
        #endregion

       
        private Vector2D CollisionAvoidance()
        {
            var vector = Velocity.Clone();
            vector.Normalize();
            vector.Multiply(MAX_SEE_AHEAD * Velocity.Length() / MaxSpeed);

            var ahead = Pos.Clone().Add(vector);

            BaseGameEntity mostThreatening = null;

            // retrieve a list of obstacles, depending on spatial partitioning setting

            var obstacles = new List<BaseGameEntity>();
            if (MyWorld.Settings.Get("ShowSpatialGrid"))
            {
                obstacles = MyWorld.WorldGrid.GetAllAdjacentEntities(this);
            }
            else
            {
                foreach(var entity in MyWorld.obstacles)
                {
                    if (entity is Obstacle obstacle)
                    {
                        if (obstacle.Type == ObstacleType.DEFAULT)
                        {
                            obstacles.Add(obstacle);
                        }
                    }
                }
            }

            foreach (var obs in obstacles)
            {

                if (obs == null)
                    continue;

                if (obs.GetType() != typeof(Obstacle))
                    continue;

                // cast BaseGameEntity to obstacle
                Obstacle obstacle = (Obstacle)obs;
                ObstacleShape obstacleShape = obstacle.Shape;
                bool collision = false;

                switch (obstacleShape)
                {
                    case ObstacleShape.CIRCLE:
                        collision = LineIntersectsCircle(Pos, ahead, new UtilCircle(obstacle.Pos, obstacle.Scale));
                        break;
                    case ObstacleShape.RECTANGLE:
                        Rectangle rect = new Rectangle(new Point((int)obstacle.Pos.X, (int)obstacle.Pos.Y), new Size(new Point((int)obstacle.Scale * 2, (int)obstacle.Scale * 2)));
                        collision = LineIntersectsRectangle(Pos, ahead, rect);
                        break;
                }

                if (collision && (mostThreatening == null || EntityHelper.Distance(Pos, obstacle.Pos) < EntityHelper.Distance(Pos, mostThreatening.Pos)))
                {
                    mostThreatening = obstacle;
                }
            }

            if (mostThreatening != null)
            {
                Avoidance.X = ahead.X - mostThreatening.Pos.X;
                Avoidance.Y = ahead.Y - mostThreatening.Pos.Y;

                Avoidance.Normalize();
                Avoidance.Multiply(AVOID_FORCE);
            }
            else
            {
                // nullify the avoidance force
                Avoidance.Multiply(0);
            }

            return Avoidance;
        }

        public override void Update(float timeElapsed)
        {

            var target = MyWorld.Target.Pos.Clone();
            var currentPos = Pos.Clone();

            var steeringForce = SB.Calculate();
            steeringForce += CollisionAvoidance();

            var collision = CheckCollision();
            if (collision.Item1)
            {
                Random r = new Random();
                double offset = r.NextDouble() * r.Next(5, 10);

                switch (collision.Item2)
                {
                    case CollisionDirection.NEGATIVE_X:
                        Pos.X = MyWorld.Width - Pos.X;
                        break;
                    case CollisionDirection.POSITIVE_X:
                        Pos.X = 0 + offset;
                        break;
                    case CollisionDirection.NEGATIVE_Y:
                        Pos.Y = MyWorld.Height - Pos.Y;
                        break;
                    case CollisionDirection.POSITIVE_Y:
                        Pos.Y = 0 + offset;
                        break;
                }
            }

            steeringForce.Truncate(MaxSpeed);
            steeringForce.Multiply(1 / Mass);

            Velocity = Velocity.Add(steeringForce);
            Velocity.Truncate(MaxSpeed);

            if (Velocity.LengthSquared() > 0.00000001)
                Heading = Velocity.Clone().Normalize();

            Pos.Add(Velocity);
        }

        public override string ToString()
        {
            return String.Format("{0}", Velocity);
        }
    }
}
