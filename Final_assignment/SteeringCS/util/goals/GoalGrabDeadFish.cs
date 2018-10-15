using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.entity;

namespace SteeringCS.util.goals
{
    public class GoalGrabDeadFish : CompositeGoal
    {
        public GoalGrabDeadFish(MovingEntity me) : base(me)
        {
        }

        public override void Activate()
        {
            GoalStatus = GoalState.ACTIVE;
            SubGoals.AddFirst(new GoalSeekToPosition(OwnerEntity, OwnerEntity.MyWorld.DeadFish.Pos));
            Console.WriteLine("Dead fish position: {0}", OwnerEntity.MyWorld.DeadFish.Pos);
        }

        public override GoalState Process()
        {
            ActivateIfInactive();

            GoalStatus = ProcessSubgoals();

            if (HasDeadFish())
            {
                GoalStatus = GoalState.COMPLETED;
            }

            ReactivateIfFailed();

            return GoalStatus;
        }

        private bool HasDeadFish()
        {
            return OwnerEntity.CarriesDeadFish;
        }

        public override void Render()
        {
            base.Render();
        }

        public override void Terminate()
        {
            OwnerEntity.CarriesDeadFish = true;
            Console.WriteLine("Dead fish grabbed!");
        }
    }
}
