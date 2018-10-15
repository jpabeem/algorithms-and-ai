using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.entity;

namespace SteeringCS.util.goals.evaluation
{
    public class MoveToPositionEvaluator : GoalEvaluator
    {
        public override float CalculateDesirability(MovingEntity me)
        {
            return 5f;
        }

        // no evaluator for this goal because it is only fired when a click happens
        public override void SetGoal(MovingEntity me)
        {
            // get the brain and add the goal
            //me.Brain.AddGoalMovetoPosition();
        }
    }
}
