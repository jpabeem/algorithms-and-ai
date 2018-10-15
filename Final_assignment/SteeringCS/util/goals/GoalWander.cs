using SteeringCS.behaviour;
using SteeringCS.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.util.goals
{
    public class GoalWander : Goal
    {
        public GoalWander(MovingEntity me) : base(me) { }

        public override void Activate()
        {
            GoalStatus = GoalState.ACTIVE;
            OwnerEntity.SB = new WanderBehaviour(OwnerEntity);
        }

        public override GoalState Process()
        {
            ActivateIfInactive();
            return GoalStatus;
        }

        public override void Render()
        {
            throw new NotImplementedException();
        }

        public override void Terminate()
        {
            OwnerEntity.SB = new SeekBehaviour(OwnerEntity);
        }
    }
}
