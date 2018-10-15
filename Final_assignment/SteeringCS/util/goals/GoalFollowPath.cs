using SteeringCS.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.util.goals
{
    public class GoalFollowPath : CompositeGoal
    {
        private SBPath Path { get; set; }

        public GoalFollowPath(MovingEntity me, SBPath path) : base(me)
        {
            Path = path;
        }

        public override void Activate()
        {
            GoalStatus = GoalState.ACTIVE;

            // turn auto arbitrate off, so we can be sure this goal is fully executed without interruption
            OwnerEntity.MyWorld.Settings.Set("AutoArbitrate", false);

            // get reference to the next target on the path
            var frontNode = Path.Nodes.First();
            Path.Nodes.Remove(frontNode);

            var isLastNode = Path.Nodes.Count > 1 ? false : true;

            // keep adding subgoals (traverse edge in this case) which makes the goal follow a path
            AddSubGoal(new GoalTraverseEdge(OwnerEntity, frontNode, isLastNode));
        }

        public override GoalState Process()
        {
            // if status is inactive, call Activate()
            ActivateIfInactive();

            /* If there are no subgoals and there is still an edge left to traverse
             * Add the edge as a subgoal
             */
            GoalStatus = ProcessSubgoals();

            /* If there are no subgoals present, check to see if the path still has edges.
             * If true, activate to grab the next edge
             */
            if (GoalStatus == GoalState.COMPLETED && Path.Nodes.Count > 0)
            {
                Activate();
            }

            return GoalStatus;
        }

        public override void Render()
        {
            throw new NotImplementedException();
        }

        public override void Terminate()
        {
            OwnerEntity.MyWorld.Settings.Set("AutoArbitrate", true);
        }
    }
}
