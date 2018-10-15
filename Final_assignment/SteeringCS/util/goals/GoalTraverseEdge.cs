using SteeringCS.behaviour;
using SteeringCS.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.util.goals
{
    public class GoalTraverseEdge : Goal
    {
        //public Edge Edge { get; private set; }
        public Vector2D Edge { get; private set; }
        public bool LastEdge { get; private set; }
        public double ExpectedTime { get; private set; }
        public long StartTime { get; private set; }
        public const double MarginOfError = 5.0;

        public GoalTraverseEdge(MovingEntity me, Vector2D edge, bool lastEdge) : base(me)
        {
            Edge = edge;
            LastEdge = lastEdge;
        }

        public override void Activate()
        {
            GoalStatus = GoalState.ACTIVE;
            StartTime = Clock.GetCurrentTimeInSeconds();

            //ExpectedTime = EntityHelper.CalculateTimeToReachPosition(OwnerEntity, Edge.GetDestination());
            ExpectedTime = EntityHelper.CalculateTimeToReachPosition(OwnerEntity, Edge);

            ExpectedTime += MarginOfError;

            //OwnerEntity.Target = Edge.GetDestination();
            OwnerEntity.Target = Edge;

            // set appropriate steering behaviour.
            // if last edge, the entity should use arrival behaviour
            if (LastEdge)
            {
                OwnerEntity.SB = new ArriveBehaviour(OwnerEntity);
            }
            else
            {
                OwnerEntity.SB = new SeekBehaviour(OwnerEntity);
            }

            // the edge behaviour flag may specify a type of movement that neccesitates a change in the bot's behaviour as it follows this edge

        }

        public override GoalState Process()
        {
            ActivateIfInactive();

            if (IsStuck())
            {
                GoalStatus = GoalState.FAILED;
            }
            else if (EntityHelper.IsAtPosition(OwnerEntity, Edge, 6))
            {
                GoalStatus = GoalState.COMPLETED;
            }

            return GoalStatus;
        }

        private bool IsStuck()
        {
            var timeTaken = Clock.GetCurrentTimeInSeconds() - StartTime;

            if (timeTaken > ExpectedTime)
            {
                Console.WriteLine("A bot is stuck!!");
                return true;
            }

            return false;
        }

        public override void Render()
        {
            throw new NotImplementedException();
        }

        public override void Terminate()
        {
            // turn off steering behaviours
            OwnerEntity.Target = null;
        }
    }
}
