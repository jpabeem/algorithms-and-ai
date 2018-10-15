using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.entity;

namespace SteeringCS.util.goals
{
    /// <summary>
    /// This goal:
    /// - Locates a dead fish
    /// - Picks it up
    /// - Bring fish to Grave
    /// </summary>
    public class GoalCleanUpDeadFish : CompositeGoal
    {
        private DeadFish DeadFish { get; set; }
        private Vector2D DeadFishLocation { get; set; }

        public GoalCleanUpDeadFish(MovingEntity me) : base(me)
        {
        }

        public override void Activate()
        {
            GoalStatus = GoalState.ACTIVE;

            // add the bury dead fish as third goal (Brings the dead fish to the gravestone)
            AddSubGoal(new GoalBuryDeadFish(OwnerEntity));

            // add the grab fish goal as second goal (Grab the dead fish)
            AddSubGoal(new GoalGrabDeadFish(OwnerEntity));

            // add the explore goal as first goal (Locates a dead fish)
            AddSubGoal(new GoalExplore(OwnerEntity, OwnerEntity.MyWorld.DeadFish));
        }

        public override GoalState Process()
        {
            ActivateIfInactive();

            GoalStatus = ProcessSubgoals();

            ReactivateIfFailed();

            GoalStatus = ProcessSubgoals();

            return GoalStatus;
        }

        private bool IsInRange()
        {
            return true;
        }

        public override void Render()
        {
            base.Render();
        }

        public override void Terminate()
        {
            var vehicle = (Vehicle)OwnerEntity;
            vehicle.SetBehaviour(Behaviour.DEFAULT);
        }
    }
}
