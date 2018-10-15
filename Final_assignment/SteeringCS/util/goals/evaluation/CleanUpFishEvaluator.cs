using SteeringCS.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.util.goals.evaluation
{
    public class CleanUpFishEvaluator : GoalEvaluator
    {
        public override float CalculateDesirability(MovingEntity me)
        {
            if (me.MyWorld.DeadFish != null)
                return 100f;
            else
                return 0f;
        }

        public override void SetGoal(MovingEntity me)
        {
            // get the brain and add the goal
            me.Brain.AddGoalCleanUpFish();
        }
    }
}
