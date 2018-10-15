using SteeringCS.behaviour;
using SteeringCS.entity;
using SteeringCS.util;
using SteeringCS.util.spatial_partitioning;
using SteeringCS.util.sprites;
using SteeringCS.world;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SteeringCS
{
    public enum CursorMode { DEBUG_INFO, SEEK };
    public class World
    {
        public List<MovingEntity> entities { get; private set; }
        public List<Vehicle> toBeAddedVehicles { get; private set; }
        public List<BaseGameEntity> obstacles { get; private set; }
        public List<Vertex> drawnPath { get; set; }

        public Fishy Fishy { get; set; }

        public DeadFish DeadFish { get; set; }

        public Obstacle Grave { get; set; }

        public WorldSettings Settings { get; private set; }

        public Vector2D CurrentTargetPathFollowing { get; set; }

        public SBPath SteeringPath { get; set; }

        public Graph WorldGraph { get; set; }
        public Grid WorldGrid { get; set; }

        public Random RandomGenerator { get; set; }

        public int BuriedFish { get; set; }

        /* image variables */
        public Image imgIdleRight, imgIdleLeft, imgIdleUp, imgIdleDown, imgFish;

        public Vehicle Target { get; set; }
        public CursorRadius CursorTarget { get; set; }
        public BaseGameEntity DebugEntity { get; set; }
        public FlockingHelper FlockingHelper { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public int VertexSize { get; set; }

        public CursorMode Mode { get; set; }

        public int FrameRate { get; set; }

        public World(int w, int h)
        {
            Width = w;
            Height = h;

            entities = new List<MovingEntity>();
            toBeAddedVehicles = new List<Vehicle>();
            obstacles = new List<BaseGameEntity>();
            WorldGraph = new Graph();
            WorldGrid = new Grid(this);
            Fishy = null;

            CurrentTargetPathFollowing = new Vector2D(0, 0);

            CursorTarget = new CursorRadius(new Point(w / 2, h / 2), this);
            DebugEntity = null;

            FrameRate = 1;

            InitWorldSettings();

            VertexSize = 32;
            RandomGenerator = new Random();

            SteeringPath = null;

            Populate();

            GenerateGraph();

            // add a dead fish after population and graph generation
            AddNewDeadFish();
            
            InitSprites();
        }

        /// <summary>
        /// Returns all vehicles of a specific type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<MovingEntity> GetVehiclesForType(Type type)
        {
            List<MovingEntity> vehicles = new List<MovingEntity>();

            try
            {
                vehicles = entities.Where(e => type == e.SB.GetType()).ToList();

            }catch(Exception e)
            {
                Console.WriteLine("Oops, something went wrong, error: {0}", e.Message);
            }
            return vehicles;
        }

        /// <summary>
        /// Returns one vehicle of a specific steering behaviour type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public MovingEntity GetVehicleForType(Type type, bool AllowFishy = false)
        {
            MovingEntity movingEntity;

            // allow to include 'Fishy' in the search for a given steering behaviour type.
            if (AllowFishy)
                movingEntity = entities.First(e => type == e.SB.GetType());
            else
                movingEntity = entities.First(e => type == e.SB.GetType() && !(e.Equals(Fishy)));
            return movingEntity;
        }


        /// <summary>
        /// Predicate which is used to determine if a random location is valid.
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        private bool IsValidLocation(Vector2D location)
        {
            return (location.X > 25 && location.X < Width - 25
                    && location.Y > 25 && location.Y < Height - 25);
        }

        /// <summary>
        /// Retrieve a random VALID point in our world as a vector.
        /// </summary>
        /// <returns></returns>
        private Vector2D GetRandomVectorPointInWorld()
        {
            var randomPoint = WorldGraph.vertexMap.ElementAt(RandomGenerator.Next(0, WorldGraph.vertexMap.Count)).Value;
            var splitted = randomPoint.name.Split(';');
            var location = new Vector2D(double.Parse(splitted[0]), double.Parse(splitted[1]));

            // allow valid locations only
            while (!(IsValidLocation(location)))
            {
                randomPoint = WorldGraph.vertexMap.ElementAt(RandomGenerator.Next(0, WorldGraph.vertexMap.Count)).Value;
                splitted = randomPoint.name.Split(';');
                location = new Vector2D(double.Parse(splitted[0]), double.Parse(splitted[1]));
            }

            return new Vector2D(double.Parse(splitted[0]), double.Parse(splitted[1]));
        }

        /// <summary>
        /// Add a new dead fish to the game world.
        /// </summary>
        private void AddNewDeadFish()
        {
            if (DeadFish != null) return;

            DeadFish deadFish = new DeadFish(GetRandomVectorPointInWorld(), this);
            deadFish.VColor = Color.Chocolate;
            deadFish.SpriteStrategy = new DeadFishSprite();
            DeadFish = deadFish;
            entities.Add(deadFish);
        }

        /// <summary>
        /// Handles the creation of new dead fish in the game world.
        /// </summary>
        public void HandleNewDeadFish()
        {
            if (DeadFish != null) return;

            int randomNum = RandomGenerator.Next(0, 4000);

            if (randomNum < 10)
            {
                AddNewDeadFish();
            }
        }

        /// <summary>
        /// Populate the Game World during runtime with a new Agent.
        /// </summary>
        /// <param name="b">Type of behaviour.</param>
        public void PopulateDuringRuntime(Behaviour behaviour)
        {
            Vehicle agent = new Vehicle(new Vector2D(10, 10), this, behaviour);
            agent.VColor = GetRandomColor();
            toBeAddedVehicles.Add(agent);
        }

        private Color GetRandomColor()
        {
            return Color.FromArgb(RandomGenerator.Next(256), RandomGenerator.Next(256), RandomGenerator.Next(256));
        }

        /// <summary>
        /// Populate the current world with different types of entities.
        /// </summary>
        private void Populate()
        {
            FillCircle fillCircle = new FillCircle();
            OutlineCircle outlineCircle = new OutlineCircle();
            RedFishSprite redFishSprite = new RedFishSprite();
            WaterSourceLargeSprite waterLarge = new WaterSourceLargeSprite();
            FoodSourceSprite foodSource = new FoodSourceSprite();
            GrassSprite grass = new GrassSprite();
            FleeFishSprite fleeSprite = new FleeFishSprite();
            WanderFishSprite wanderSprite = new WanderFishSprite();
            PursuitFishSprite pursuitSprite = new PursuitFishSprite();
            BlueFishSprite blueFishSprite = new BlueFishSprite();
            DeadFishSprite deadFishSprite = new DeadFishSprite();

            Target = new Vehicle(new Vector2D(100, 60), this);
            Target.VColor = Color.DarkRed;
            Target.Scale = 3;
            Target.Pos = new Vector2D(150, 120);

            Fishy fishy = new Fishy(new Vector2D(45, 50), this);
            fishy.VColor = Color.Blue;
            fishy.SpriteStrategy = blueFishSprite;
            Fishy = fishy;
            entities.Add(fishy);

            //Vehicle seekVehicle = new Vehicle(new Vector2D(10, 10), this, Behaviour.ARRIVE);
            //seekVehicle.VColor = Color.Blue;
            //seekVehicle.SpriteStrategy = blueFishSprite;
            //entities.Add(seekVehicle);

            //Vehicle pathFollowingVehicle = new Vehicle(new Vector2D(20, 20), this, Behaviour.PATH_FOLLOWING);
            //pathFollowingVehicle.VColor = Color.Yellow;
            //pathFollowingVehicle.SpriteStrategy = redFishSprite;
            //entities.Add(pathFollowingVehicle);

            // modified flee behaviour
            Vehicle slowpokeVehicle = new Vehicle(new Vector2D(250, 250), this, Behaviour.SLOWPOKE);
            slowpokeVehicle.VColor = Color.Green;
            slowpokeVehicle.SpriteStrategy = fleeSprite;
            entities.Add(slowpokeVehicle);

            // default wander vehicle
            Vehicle wanderVehicle = new Vehicle(new Vector2D(40, 40), this, Behaviour.WANDER);
            wanderVehicle.VColor = Color.Purple;
            wanderVehicle.SpriteStrategy = wanderSprite;
            entities.Add(wanderVehicle);

            int pursuitVehicles = 10;
            Random randomGen = new Random();
            for (int i = 0; i < pursuitVehicles; i++)
            {
                Vehicle pursuitVehicle = new Vehicle(new Vector2D(randomGen.Next(0, (Width - 40)), randomGen.Next(0, (Height - 40))), this, Behaviour.PURSUIT);
                pursuitVehicle.VColor = GetRandomColor();
                pursuitVehicle.SpriteStrategy = pursuitSprite;
                entities.Add(pursuitVehicle);
            }

            Obstacle smallCircleObs = new Obstacle(new Vector2D(180, 180), this, ObstacleShape.CIRCLE);
            obstacles.Add(smallCircleObs);

            Obstacle graveStoneObs = new Obstacle(new Vector2D(65, 550), this, ObstacleShape.CIRCLE, null, ObstacleType.GRAVE_STONE);
            graveStoneObs.Scale = 45;
            Grave = graveStoneObs;
            obstacles.Add(graveStoneObs);

            Obstacle largeCircleObs = new Obstacle(new Vector2D(730, 210), this, ObstacleShape.CIRCLE);
            largeCircleObs.Scale = 100;
            obstacles.Add(largeCircleObs);

            FlockingHelper = new FlockingHelper(entities);
        }

        /// <summary>
        /// If a new entity is added to the game world, add it to the list of to be added vehicles.
        /// </summary>
        public void HandleNewEntities()
        {
            if (toBeAddedVehicles.Count > 0)
            {
                toBeAddedVehicles.ForEach(
                    e => entities.Add(e)
                );

                if (Mode == CursorMode.DEBUG_INFO)
                {
                    DebugEntity = toBeAddedVehicles.Last();
                }

                toBeAddedVehicles.Clear();
            }
        }

        /// <summary>
        /// Initialize the WorldSettings dictionary.
        /// </summary>
        private void InitWorldSettings()
        {
            Settings = new WorldSettings(
                new Dictionary<string, bool>
                {
                    { "GraphEnabled", false },
                    { "GamePaused", false },
                    { "GraphDrawn", false },
                    { "ForceRedraw", false },
                    { "ShowSpatialGrid", false },
                    { "ShowAdjacentBuckets", false },
                    { "SpritesEnabled", false },
                    { "EnforceNonPenetrationConstraint", false },
                    { "HandleNewVehicle", false },
                    { "ToggleObstacleBoundingBox", false },
                    { "AutoArbitrate", true },
                    { "ShowGeneratedPath", true },
                    { "ShowTarget", true },
                }
            );
        }

        /// <summary>
        /// Returns a vertex name represented as a string.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private string FormattedVertexString(Point point)
        {
            return string.Format("{0};{1}", point.X, point.Y);
        }

        /// <summary>
        /// Checks if the given point is out of bounds.
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        private bool IsOutOfBounds(Point point)
        {
            if (point.X < 0 || point.X > Width)
                return true;

            if (point.Y < 0 || point.Y > Height)
                return true;

            return false;
        }

        /// <summary>
        /// Checks if a point within the game world is valid, by checking the given point 
        /// against colission with obstacles & the bounds of the game world.
        /// </summary>
        /// <param name="point">The point which has to be checked</param>
        /// <returns>bool</returns>
        private bool IsAllowed(Point point)
        {
            foreach (var obstacle in obstacles)
            {
                if (obstacle is Obstacle o)
                {
                    if (o.Type == ObstacleType.DEFAULT)
                    {
                        var distance = EntityHelper.Distance(new Vector2D(point), obstacle.Pos);

                        if (IsOutOfBounds(point))
                            return false;

                        if ((distance < obstacle.Scale * 1.2))
                            return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Generate a graph representation of the current world.
        /// </summary>
        /// <returns></returns>
        public void GenerateGraph()
        {
            WorldGraph.Reset();

            var queue = new Queue<Point>();

            var startX = 4;
            var startY = 4;

            // modifier for the width/height between each vertex
            int modifier = VertexSize;

            var watch = System.Diagnostics.Stopwatch.StartNew();
            Console.WriteLine("Starting timer"); // start of benchmarking
            watch.Start();

            for (int width = startX; width < Width; width += modifier)
            {
                for (int height = startY; height < Height; height += modifier)
                {
                    var currentPoint = new Point(width, height);

                    // check if the point is allowed (no colission with static obstacles)
                    bool pointAllowed = IsAllowed(currentPoint);

                    if (pointAllowed)
                    {
                        string current = FormattedVertexString(currentPoint);
                        WorldGraph.AddVertex(current);

                        /*
                            Define all the points related to the current point
                            
                            topLeftCorner         |    top    |        topRightCorner
                            left                  CURRENT_POINT                 right
                            bottomLeftCorner      |  bottom   |     bottomRightCorner
                        */
                        var rightPoint = new Point(currentPoint.X + modifier, currentPoint.Y);
                        var topRightCornerPoint = new Point(currentPoint.X + modifier, currentPoint.Y + modifier);
                        var leftPoint = new Point(currentPoint.X - modifier, currentPoint.Y);
                        var topLeftCornerPoint = new Point(currentPoint.X - modifier, currentPoint.Y + modifier);
                        var topPoint = new Point(currentPoint.X, currentPoint.Y + modifier);
                        var bottomPoint = new Point(currentPoint.X, currentPoint.Y - modifier);
                        var bottomRightCornerPoint = new Point(currentPoint.X + modifier, currentPoint.Y - modifier);
                        var bottomLeftCornerPoint = new Point(currentPoint.X - modifier, currentPoint.Y - modifier);
                        var possiblePoints = new List<Point> { rightPoint, topRightCornerPoint, topLeftCornerPoint, leftPoint, topPoint, bottomPoint, bottomLeftCornerPoint, bottomRightCornerPoint };

                        foreach (var point in possiblePoints)
                        {
                            if (IsAllowed(point))
                            {
                                if (!WorldGraph.ContainsEdge(current, FormattedVertexString(point)))
                                    //{
                                    WorldGraph.AddEdge(FormattedVertexString(currentPoint), FormattedVertexString(point), 1);
                                //queue.Enqueue(point);
                                Console.WriteLine("Added point {0} to the queue: ", point);
                            }
                        }
                    }
                }
            }

            watch.Stop();
            Console.WriteLine("Elapsed: {0}", watch.Elapsed); // end of benchmarking
        }

        public void InitSettingsForm(Settings form)
        {
            FlockingHelper.SettingsForm = form;
        }

        /// <summary>
        /// Initialize the sprites.
        /// </summary>
        private void InitSprites()
        {
            imgIdleRight = SteeringCS.Properties.Resources.idle_right;
            imgIdleLeft = SteeringCS.Properties.Resources.idle_left;
            imgIdleUp = SteeringCS.Properties.Resources.idle_up;
            imgIdleDown = SteeringCS.Properties.Resources.idle_down;
            imgFish = SteeringCS.Properties.Resources.redFishRight;
        }

        /// <summary>
        /// Update all the (moving) entities within the world.
        /// </summary>
        /// <param name="timeElapsed"></param>
        public void Update(float timeElapsed)
        {
            lock (entities)
            {
                for (int i = 0; i < entities.Count; i++)
                {
                    var me = entities[i];
                    WorldGrid.AddToBucket(me);
                    Type behaviour = me.SB.GetType();
                    me.Update(timeElapsed);

                    // enable flocking for PursuitBehaviour only
                    if (behaviour == typeof(PursuitBehaviour) || behaviour == typeof(WanderBehaviour))
                        DoFlocking(me);

                    if (behaviour == typeof(ExploreBehaviour))
                        DoFlocking(me);

                    if (behaviour == typeof(SlowpokeBehaviour))
                        DoFlocking(me);

                    AvoidWalls(me);

                    if (behaviour == (typeof(WanderBehaviour)) || behaviour == typeof(FleeBehaviour))
                        me.Velocity.Normalize(4);
                }
            }
            HandleNewEntities();

            //process the currently active goal. Note this is required even if the bot
            //is under user control. This is because a goal is created whenever a user 
            //clicks on an area of the map that necessitates a path planning request.
            Fishy.Brain.Process();
        }

        /// <summary>
        /// Compute and apply the wall avoidance vector of a moving entity.
        /// </summary>
        /// <param name="me">The moving entity</param>
        private void AvoidWalls(MovingEntity me)
        {
            var wallAvoidanceVector = FlockingHelper.ComputeWallAvoidance(me);
            me.Velocity.X += wallAvoidanceVector.X * 0.1;
            me.Velocity.Y += wallAvoidanceVector.Y * 0.1;
        }

        /// <summary>
        /// Enable flocking on a moving entity.
        /// </summary>
        /// <param name="me"></param>
        private void DoFlocking(MovingEntity me)
        {
            if (Settings.Get("EnforceNonPenetrationConstraint"))
            {
                var entities = GetVehiclesForType(typeof(PursuitBehaviour));
                FlockingHelper.EnforceNonPenetrationConstraint(me, entities);
            }
            var alignment = FlockingHelper.ComputeAlignment(me);
            var cohesion = FlockingHelper.ComputeCohesion(me);
            var separation = FlockingHelper.ComputeSeparation(me);

            me.Velocity.X += alignment.X * FlockingHelper.AlignmentWeight + cohesion.Y * FlockingHelper.CohesionWeight + separation.X * FlockingHelper.SeparationWeight;
            me.Velocity.X += alignment.Y * FlockingHelper.AlignmentWeight + cohesion.Y * FlockingHelper.CohesionWeight + separation.Y * FlockingHelper.SeparationWeight;
        }

        /// <summary>
        /// Render all the entities on the screen.
        /// </summary>
        /// <param name="g"></param>
        public void Render(Graphics g)
        {
            // render obstacles before entities
            obstacles.ForEach(o => o.Render(g));

            lock (entities)
            {
                entities.ForEach(e => e.Render(g));
            }
            RenderDebugMode(g);

            if (Settings.Get("ShowTarget"))
            {
                Target.Render(g);
            }

            HandleNewEntities();
            HandleNewDeadFish();
        }

        /// <summary>
        /// Render the debug entity info on screen.
        /// </summary>
        /// <param name="g"></param>
        private void RenderDebugMode(Graphics g)
        {
            if (Mode == CursorMode.DEBUG_INFO)
            {
                if (DebugEntity != null)
                {
                    // draw the debug string on the entity
                    string text = "Debug";
                    System.Drawing.Font drawFont = new System.Drawing.Font("Arial", 8, FontStyle.Bold);
                    System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
                    float x = (float)DebugEntity.Pos.X - DebugEntity.Scale / 1.5f;
                    float y = (float)DebugEntity.Pos.Y;
                    System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();
                    g.DrawString(text, drawFont, drawBrush, x, y, drawFormat);

                    // draw the rectangle around the entity
                    double leftCorner = DebugEntity.Pos.X - (DebugEntity.Scale * 1.5);
                    double rightCorner = DebugEntity.Pos.Y - (DebugEntity.Scale * 1.5);
                    int size = (int)DebugEntity.Scale * 3;

                    Pen debugPen = new Pen(Color.Black)
                    {
                        DashStyle = System.Drawing.Drawing2D.DashStyle.Dash,
                        Width = 1.5f
                    };
                    g.DrawRectangle(debugPen, new Rectangle((int)leftCorner, (int)rightCorner, size, size));
                }

                CursorTarget.Render(g);
            }
        }
    }
}
