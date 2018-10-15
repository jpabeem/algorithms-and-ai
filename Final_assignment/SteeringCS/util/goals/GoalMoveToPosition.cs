using SteeringCS.behaviour;
using SteeringCS.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.util.goals
{
    public class GoalMoveToPosition : CompositeGoal
    {
        private Vector2D Destination { get; set; }

        public GoalMoveToPosition(MovingEntity me, Vector2D destination) : base(me)
        {
            Destination = destination;
            me.MyWorld.Target.Pos = Destination;
        }

        public override void Activate()
        {
            GoalStatus = GoalState.ACTIVE;

            // turn auto arbitrate off, so we can be sure this goal is fully executed
            OwnerEntity.MyWorld.Settings.Set("AutoArbitrate", false);

            RemoveAllSubgoals();

            SubGoals.AddFirst(new GoalSeekToPosition(OwnerEntity, Destination));
        }

        public override GoalState Process()
        {
            ActivateIfInactive();

            GoalStatus = ProcessSubgoals();

            ReactivateIfFailed();

            GoalStatus = ProcessSubgoals();

            return GoalStatus;
        }

        public override void Render()
        {
            base.Render();
        }

        public override void Terminate()
        {
            var vehicle = (Vehicle)OwnerEntity;
            vehicle.SetBehaviour(Behaviour.DEFAULT);
            OwnerEntity.MyWorld.Settings.Set("AutoArbitrate", true);
        }
    }
}
