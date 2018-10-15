using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.entity;

namespace SteeringCS.util.goals
{
    public class CompositeGoal : Goal
    {
        public LinkedList<Goal> SubGoals { get; set; }

        public CompositeGoal(MovingEntity me) : base(me) 
        {
            SubGoals = new LinkedList<Goal>();
        }

        public override void Activate()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Add a subgoal to front of the subgoal list.
        /// </summary>
        /// <param name="goal"></param>
        public virtual void AddSubGoal(Goal goal)
        {
            SubGoals.AddFirst(goal);
        }

        public GoalState ProcessSubgoals()
        {
            // remove all completed and failed goals from the front of the subgoal list
            while (SubGoals.Count > 0 && (SubGoals.First.Value.IsComplete || SubGoals.First.Value.HasFailed))
            {
                SubGoals.First.Value.Terminate();
                SubGoals.RemoveFirst();
            }

            // if any goals remain, process the one at the front of the list
            if (SubGoals.Count > 0)
            {
                // grab the status of the frontmost subgoal
                var statusOfSubGoals = SubGoals.First.Value.Process();

                /* 
                 * Test for the special case where the subgoal at the front of the list
                 * reports "completed" and the subgoal list contains additional goals.
                 * When this is the case, the "active" status is returned.
                 */
                if (statusOfSubGoals == GoalState.COMPLETED && SubGoals.Count > 1)
                    return GoalState.ACTIVE;

                return statusOfSubGoals;
            }
            else // no more subgoals to process - return "completed"
            {
                return GoalState.COMPLETED;
            }
        }

        public override GoalState Process()
        {
            throw new NotImplementedException();
        }

        public override void Terminate()
        {
            throw new NotImplementedException();
        }

        public void RemoveAllSubgoals()
        {
            foreach(var goal in SubGoals)
            {
                goal.Terminate();
            }
            SubGoals.Clear();
        }

        public override void Render()
        {
            throw new NotImplementedException();
        }
    }
}
