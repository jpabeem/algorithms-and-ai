using SteeringCS.behaviour;
using SteeringCS.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.util.goals
{
    public class GoalExplore : Goal
    {
        // The type we are looking for
        private BaseGameEntity TargetEntity { get; set; }

        public GoalExplore(MovingEntity me, BaseGameEntity entity) : base (me)
        {
            TargetEntity = entity;
        }

        public override void Activate()
        {
            GoalStatus = GoalState.ACTIVE;
            var vehicle = (Vehicle)OwnerEntity;
            vehicle.SetBehaviour(Behaviour.EXPLORE, TargetEntity);
        }

        /// <summary>
        /// TODO: check for a time out in case the explorer gets stuck somewhere, or cannot find his
        /// target. (30 seconds?)
        /// </summary>
        /// <returns></returns>
        public override GoalState Process()
        {
            ActivateIfInactive();

            if (ExploringCompleted())
            {
                GoalStatus = GoalState.COMPLETED;
                OwnerEntity.TargetFound = false;
            }

            return GoalStatus;
        }

        private bool ExploringCompleted()
        {
            if (OwnerEntity.TargetFound)
            {
                return true;
            }

            return false;
        }

        public override void Render()
        {
            throw new NotImplementedException();
        }

        public override void Terminate()
        {
            OwnerEntity.SB = new PathFollowingBehaviour(OwnerEntity);
        }
    }
}
