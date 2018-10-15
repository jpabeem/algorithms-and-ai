using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.util;
using SteeringCS.util.fuzzy_logic;
using SteeringCS.util.goals;

namespace SteeringCS.entity
{
    public class Fishy : Vehicle
    {
        /* 
         * NOTE: on default, auto arbitration of goals is OFF
         * The number of times a second a bot thinks about changing strategy 
         */
        public const int GoalAppraisalUpdateFrequency = 4;

        public double DesirabilityScore { get; set; }

        /* returns the update frequency in milliseconds*/
        private long updateFrequencyInMs
        {
            get
            {
                if (MyWorld.Settings.Get("AutoArbitrate"))
                    return 1000 / GoalAppraisalUpdateFrequency;
                else
                {

                    /*
                     * If 'AutoArbitrate' is off => return a really big number with a safe 
                     * threshhold to prevent a long overflow.
                     */
                    return long.MaxValue - (lastUpdateTime + lastUpdateTime);
                }
            }
        }
        private long lastUpdateTime { get; set; }

        public Fishy(Vector2D pos, World w, Behaviour b = Behaviour.DEFAULT) : base(pos, w, b)
        {
            MaxSpeed = 5;
            Brain = new GoalThink(this);
            lastUpdateTime = Clock.GetCurrentTimeInMillis();
            InitializeFuzzyModule();
        }

        private void UpdateHunger()
        {
            int random = MyWorld.RandomGenerator.Next(0, 250);

            if (random < 5)
            {
                Hunger++;
                if (Hunger >= 40)
                {
                    Hunger = 0;
                }
            }
        }

        /// <summary>
        /// This method checks if re-arbitration of the high level goals 
        /// within our brain is needed.
        /// Together with the update frequency, this handles changing events in our 
        /// game world.
        /// </summary>
        private void ArbitrationRegulator()
        {
            // check if we need to re-arbitrate our brain
            var timeInMillis = Clock.GetCurrentTimeInMillis();

            // if it's time to Arbitrate again
            if (timeInMillis > (lastUpdateTime + updateFrequencyInMs))
            {
                Brain.Arbitrate();
                lastUpdateTime = timeInMillis;
            }
        }

        private void InitializeFuzzyModule()
        {
            FuzzyVariable DistToTarget = FuzzyModule.CreateFLV("DistToTarget");
            FzSet Target_Close = DistToTarget.AddLeftShoulderSet("Target_Close", 0, 50, 300);
            FzSet Target_Medium = DistToTarget.AddTriangularSet("Target_Medium", 50, 150, 300);
            FzSet Target_Far = DistToTarget.AddRightShoulderSet("Target_Far", 300, 600, 1000);

            FuzzyVariable Hunger = FuzzyModule.CreateFLV("Hunger");
            FzSet Hunger_Large = Hunger.AddRightShoulderSet("Hunger_Lots", 15, 30, 100);
            FzSet Hunger_Normal = Hunger.AddTriangularSet("Hunger_Normal", 2.5, 15, 30);
            FzSet Hunger_Low = Hunger.AddTriangularSet("Hunger_Low", 0, 2.5, 15);

            FuzzyVariable Perception = FuzzyModule.CreateFLV("Perception");
            FzSet Reality = Perception.AddRightShoulderSet("Reality", 50, 75, 100);
            FzSet Fantasy = Perception.AddTriangularSet("Fantasy", 25, 50, 75);
            FzSet OuterSpace = Perception.AddLeftShoulderSet("Outer space", 0, 25, 50);

            FuzzyModule.AddRule(new FzAND(Target_Close, Hunger_Large), Fantasy);
            FuzzyModule.AddRule(new FzAND(Target_Close, Hunger_Normal), Reality);
            FuzzyModule.AddRule(new FzAND(Target_Close, Hunger_Low), Reality);

            FuzzyModule.AddRule(new FzAND(Target_Medium, Hunger_Large), OuterSpace);
            FuzzyModule.AddRule(new FzAND(Target_Medium, Hunger_Normal), Fantasy);
            FuzzyModule.AddRule(new FzAND(Target_Medium, Hunger_Low), Reality);

            FuzzyModule.AddRule(new FzAND(Target_Far, Hunger_Large), OuterSpace);
            FuzzyModule.AddRule(new FzAND(Target_Far, Hunger_Normal), Fantasy);
            FuzzyModule.AddRule(new FzAND(Target_Far, Hunger_Low), Reality);
        }

        private void HandleFuzzyLogic()
        {
            if (MyWorld.DeadFish == null)
                return;

            var distToFish = EntityHelper.Distance(Pos, MyWorld.DeadFish.Pos);

            FuzzyModule.Fuzzify("DistToTarget", distToFish);
            FuzzyModule.Fuzzify("Hunger", Hunger);

            var desirabilityScore = FuzzyModule.DeFuzzify("Perception", DefuzzifyType.MAX_AV);
            DesirabilityScore = desirabilityScore;

            Console.WriteLine("Desirability: {0}", DesirabilityScore);
        }

        /// <summary>
        /// Render the fish.
        /// </summary>
        /// <param name="g"></param>
        public override void Render(Graphics g)
        {
            ArbitrationRegulator();
            UpdateHunger();
            HandleFuzzyLogic();

            double leftCorner = Pos.X - Scale;
            double rightCorner = Pos.Y - Scale;

            float size = Scale * 2;

            Pen p = new Pen(VColor, 2);
            Brush brush = new SolidBrush(VColor);
            Pen vectorPen = new Pen(Color.Black, 2);
            Pen collisionPen = new Pen(Color.Red, 2);

            SpriteStrategy.RenderSprite(g, this);

            if (MyWorld.Mode == CursorMode.DEBUG_INFO)
            {
                g.DrawLine(vectorPen, (int)Pos.X, (int)Pos.Y, (int)Pos.X + (int)(Heading.X * MAX_SEE_AHEAD), (int)Pos.Y + (int)(Heading.Y * MAX_SEE_AHEAD));
                g.DrawLine(collisionPen, (int)Pos.X, (int)Pos.Y, (int)Pos.X + (int)(Velocity.X * 2), (int)Pos.Y + (int)(Velocity.Y * 2));
            }

            // draw the explorer radius
            var exploreRadiusSize = ExploreRadius * 2;
            var explorePen = new Pen(Color.DarkGray, 2);
            explorePen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            g.DrawEllipse(explorePen, new Rectangle((int)(Pos.X - ExploreRadius), (int)(Pos.Y - ExploreRadius), (int)exploreRadiusSize, (int)exploreRadiusSize));
        }
    }
}
