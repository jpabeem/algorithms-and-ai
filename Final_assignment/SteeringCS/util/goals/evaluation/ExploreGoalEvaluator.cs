using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.entity;

namespace SteeringCS.util.goals
{
    public class ExploreGoalEvaluator : GoalEvaluator
    {
        public override float CalculateDesirability(MovingEntity me)
        {
            return 10f;
        }

        public override void SetGoal(MovingEntity me)
        {
            // get the brain and add the goal
            me.Brain.AddGoalExplore();
        }
    }
}
