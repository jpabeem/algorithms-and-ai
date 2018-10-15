using SteeringCS.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.util.goals
{
    public class GoalBuryDeadFish : CompositeGoal
    {
        private int TimesStuck { get; set; }
        public double ExpectedTime { get; private set; }
        public long StartTime { get; private set; }
        public const double MarginOfError = 2.5;

        public GoalBuryDeadFish(MovingEntity me) : base(me)
        {

        }

        public override void Activate()
        {
            GoalStatus = GoalState.ACTIVE;
            AddSubGoal(new GoalSeekToPosition(OwnerEntity, OwnerEntity.MyWorld.Grave.Pos));

            StartTime = Clock.GetCurrentTimeInSeconds();
            ExpectedTime = EntityHelper.CalculateTimeToReachPosition(OwnerEntity, OwnerEntity.MyWorld.Grave.Pos);

            ExpectedTime += MarginOfError;
        }

        public override GoalState Process()
        {
            ActivateIfInactive();

            GoalStatus = ProcessSubgoals();

            if (IsStuck())
            {
                if (TimesStuck < 5)
                {
                    // if any of the subgoals have failed then this goal replans
                    ReactivateIfFailed();
                }
                else
                {
                    Console.WriteLine("Timed out!");
                    TimesStuck = 0;
                }
            }

            return GoalStatus;
        }

        private bool IsStuck()
        {
            var timeTaken = Clock.GetCurrentTimeInSeconds() - StartTime;

            if (timeTaken > ExpectedTime)
            {
                Console.WriteLine("A bot is stuck!!");
                TimesStuck++;
                return true;
            }

            return false;
        }

        public override void Render()
        {
            base.Render();
        }

        public override void Terminate()
        {
            // no longer carrying the dead fish
            OwnerEntity.CarriesDeadFish = false;

            // remove DeadFish from world, he's finally free!
            OwnerEntity.MyWorld.entities.Remove(OwnerEntity.MyWorld.DeadFish);
            OwnerEntity.MyWorld.DeadFish = null;

            OwnerEntity.MyWorld.BuriedFish += 1;
        }
    }
}
