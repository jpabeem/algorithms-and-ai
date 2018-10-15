using SteeringCS.behaviour;
using SteeringCS.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.util.goals
{
    public class GoalSeekToPosition : Goal
    {
        private Vector2D Position { get; set; }
        private int TimesStuck { get; set; }
        public double ExpectedTime { get; private set; }
        public long StartTime { get; private set; }

        public const double MarginOfError = 2.5;

        public GoalSeekToPosition(MovingEntity me, Vector2D position) : base(me)
        {
            Position = position;
        }

        public override void Activate()
        {
            GoalStatus = GoalState.ACTIVE;

            OwnerEntity.Target = Position;

            var vehicle = (Vehicle)OwnerEntity;
            vehicle.SetBehaviour(Behaviour.ARRIVE);

            StartTime = Clock.GetCurrentTimeInSeconds();
            ExpectedTime = EntityHelper.CalculateTimeToReachPosition(OwnerEntity, Position);

            ExpectedTime += MarginOfError;

            OwnerEntity.Target = Position;
        }

        public override GoalState Process()
        {
            ActivateIfInactive();

            if (IsStuck())
            {
                GoalStatus = GoalState.FAILED;

                /*
                 * If we have been stuck 5 times or less => retry
                 * We dont want to keep retrying forever.
                 */
                if (TimesStuck < 5)
                    ReactivateIfFailed();
                else
                {
                    Console.WriteLine("Timed out!");
                    TimesStuck = 0;
                }
            }
            else if (EntityHelper.IsAtPosition(OwnerEntity, Position))
            {
                GoalStatus = GoalState.COMPLETED;
            }

            return GoalStatus;
        }

        public override void Render()
        {
            throw new NotImplementedException();
        }

        public override void Terminate()
        {
            var vehicle = (Vehicle)OwnerEntity;
            vehicle.SetBehaviour(Behaviour.DEFAULT);
            vehicle.Target = new Vector2D(0, 0);
            Console.WriteLine("Terminated!");
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
    }
}
