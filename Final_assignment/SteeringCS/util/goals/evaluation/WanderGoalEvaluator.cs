using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.entity;

namespace SteeringCS.util.goals.evaluation
{
    public class WanderGoalEvaluator : GoalEvaluator
    {
        public override float CalculateDesirability(MovingEntity me)
        {
            return 15f;
        }

        public override void SetGoal(MovingEntity me)
        {
            me.Brain.AddGoalWander();
        }
    }
}
