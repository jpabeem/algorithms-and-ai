using SteeringCS.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.util.goals
{
    public abstract class GoalEvaluator
    {
        /// <summary>
        /// Calculate the desirability for a given goal.
        /// </summary>
        /// <param name="me"></param>
        /// <returns></returns>
        public abstract float CalculateDesirability(MovingEntity me);

        /// <summary>
        /// Set the goal on the moving entity.
        /// </summary>
        /// <param name="me"></param>
        public abstract void SetGoal(MovingEntity me);
    }
}
