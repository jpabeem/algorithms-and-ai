using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.entity;

namespace SteeringCS.util.goals
{
    public enum GoalState
    {
        ACTIVE,
        INACTIVE,
        COMPLETED,
        FAILED
    }

    public abstract class Goal
    {
        public GoalState GoalStatus { get; set; }
        public MovingEntity OwnerEntity { get; set; }

        public bool IsComplete
        {
            get
            {
                return GoalStatus == GoalState.COMPLETED;
            }
        }

        public bool IsActive
        {
            get
            {
                return GoalStatus == GoalState.ACTIVE;
            }
        }

        public bool IsInactive
        {
            get
            {
                return GoalStatus == GoalState.INACTIVE;
            }
        }

        public bool HasFailed
        {
            get
            {
                return GoalStatus == GoalState.FAILED;
            }
        }

        public Goal(MovingEntity entity)
        {
            OwnerEntity = entity;
            GoalStatus = GoalState.INACTIVE;
        }

        public virtual void ReactivateIfFailed()
        {
            if (HasFailed)
            {
                GoalStatus = GoalState.INACTIVE;
            }
        }

        public virtual void ActivateIfInactive()
        {
            if (IsInactive)
            {
                Activate();
            }
        }

        public abstract void Activate();

        public abstract GoalState Process();

        public abstract void Terminate();

        public abstract void Render();
    }
}
