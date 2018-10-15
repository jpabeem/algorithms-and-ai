using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.entity;
using SteeringCS.util.goals.evaluation;

namespace SteeringCS.util.goals
{
    /// <summary>
    /// The brain behind the goal driven behaviour.
    /// </summary>
    public class GoalThink : CompositeGoal
    {
        private List<GoalEvaluator> Evaluators { get; set; }


        public GoalThink(MovingEntity me) : base(me)
        {
            Evaluators = new List<GoalEvaluator>();
            Evaluators.Add(new ExploreGoalEvaluator());
            Evaluators.Add(new MoveToPositionEvaluator());
            Evaluators.Add(new WanderGoalEvaluator());
            Evaluators.Add(new CleanUpFishEvaluator());
        }

        public override void Activate()
        {
            GoalStatus = GoalState.ACTIVE;

            Arbitrate();
        }

        public override GoalState Process()
        {
            ActivateIfInactive();

            GoalStatus = ProcessSubgoals();

            if (GoalStatus == GoalState.COMPLETED || GoalStatus == GoalState.FAILED)
                GoalStatus = GoalState.INACTIVE;

            return GoalStatus;
        }

        public override void Render()
        {
            base.Render();
        }

        public override void Terminate()
        {
            // we never have to terminate our GoalThink because it is the brain behind this all
        }

        /// <summary>
        /// Returns true if the goal type passed as a parameter is the same
        /// as the goal or any of its subgoals.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool NotPresent(Goal type)
        {
            if (SubGoals.Count > 0)
            {
                return (SubGoals.First.Value.GetType() != type.GetType());
            }

            return true;
        }

        /// <summary>
        /// Evaluate all possible goals and pick the goal with the highest evaluation score.
        /// </summary>
        public void Arbitrate()
        {
            if (Evaluators.Count < 1)
                return;

            float highestScore = 0;
            GoalEvaluator bestEvaluator = Evaluators.First();

            foreach (var evaluator in Evaluators)
            {
                var score = evaluator.CalculateDesirability(OwnerEntity);
                if (score > highestScore)
                {
                    highestScore = score;
                    bestEvaluator = evaluator;
                }
            }

            bestEvaluator.SetGoal(OwnerEntity);
        }

        public void AddGoalMovetoPosition(Vector2D destination)
        {
            AddSubGoal(new GoalMoveToPosition(OwnerEntity, destination));
        }

        public void AddGoalFollowPath(SBPath path)
        {
            AddSubGoal(new GoalFollowPath(OwnerEntity, path));
        }

        public void AddGoalCleanUpFish()
        {
            if (NotPresent(new GoalCleanUpDeadFish(OwnerEntity)))
            {
                
                /* Remove all subgoals because this goal is super important
                 * think about the environment of these fish!
                 */
                RemoveAllSubgoals();
                AddSubGoal(new GoalCleanUpDeadFish(OwnerEntity));
            }
        }

        public void AddGoalWander()
        {
            if (NotPresent(new GoalWander(OwnerEntity)))
            {
                RemoveAllSubgoals();
                AddSubGoal(new GoalWander(OwnerEntity));
            }
        }

        public void AddGoalExplore()
        {
            if (NotPresent(new GoalExplore(OwnerEntity, OwnerEntity.MyWorld.DeadFish)))
            {
                RemoveAllSubgoals();
                AddSubGoal(new GoalExplore(OwnerEntity, OwnerEntity.MyWorld.DeadFish));
            }
        }
    }
}
