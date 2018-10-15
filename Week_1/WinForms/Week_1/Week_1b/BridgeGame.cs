using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week_1b
{
    class BridgeGame
    {
        public BridgeState bridgeState { get; set; }
        public int seconds { get; private set; }
        public int steps { get; private set; }
        public bool[] gameState { get; set; }
        public IDictionary<int, int> possibilities { get; set; }

        public List<BridgeState> moves { get; set; }

        public List<BridgeState> solutions { get; set; }

        public static int OBJECT_COUNT = 0;

        public BridgeGame()
        {
            bridgeState = new BridgeState("root");
            solutions = new List<BridgeState>();
        }

        public List<Tuple<int, int>> GetAllUniquePossibleMoves(BridgeState state)
        {
            List<int> personsAtLeftSide = new List<int>(state.leftSide);

            List<Tuple<int, int>> possibleMoves = new List<Tuple<int, int>>();

            foreach (int person in personsAtLeftSide)
            {
                List<int> personsAtLeftSideWithoutCurrent = new List<int>(personsAtLeftSide);
                personsAtLeftSideWithoutCurrent.Remove(person);

                foreach(int otherPerson in personsAtLeftSideWithoutCurrent)
                {
                    Tuple<int, int> tuple = Tuple.Create(person, otherPerson);
                    if (! possibleMoves.Contains(Tuple.Create(otherPerson, person)))
                        possibleMoves.Add(tuple);
                }

            }

            return possibleMoves;
        }

        public List<Tuple<int, int>> GetNextMoves(BridgeState state)
        {
            List<Tuple<int, int>> uniquePossibleMoves = GetAllUniquePossibleMoves(state);
            return uniquePossibleMoves;
        }


        public List<BridgeState> GetNextMoveStates(BridgeState root)
        {

            if (root.torchSide == TorchSide.RIGHT || root == null)
                return new List<BridgeState>();



            List<Tuple<int, int>> uniquePossibleMoves = GetAllUniquePossibleMoves(root);
            List<BridgeState> states = new List<BridgeState>();

            List<string> moves = new List<string>(root.moves);
            List<int> leftRootSide = new List<int>(root.leftSide);
            List<int> rightRootSide = new List<int>(root.rightSide);
            int elapsedRootTime = root.elapsedTime;
            TorchSide rootTorchSide = root.torchSide;

            foreach(var move in uniquePossibleMoves)
            {
                //// if we are at the first pair of unique possible moves, reset the states
                //if (uniquePossibleMoves.Count == 10)
                //{

                //    Console.WriteLine("Starting new move with: {0} & {1}", move.Item1, move.Item2);
                //    BridgeState clone = new BridgeState("#" + BridgeGame.OBJECT_COUNT);
                //    BridgeGame.OBJECT_COUNT++;

                //    MoveRight(clone, move);
                //    states.Add(clone);
                //}
                //else
                //{
                    BridgeState clone = new BridgeState("#" + BridgeGame.OBJECT_COUNT);
                    BridgeGame.OBJECT_COUNT++;
                    clone.leftSide = new List<int>(root.leftSide);
                    clone.rightSide = new List<int>(root.rightSide);
                    clone.elapsedTime = elapsedRootTime;
                    clone.torchSide = rootTorchSide;
                    clone.moves = moves;
                    clone.lastMove = move;
                    MoveRight(clone, move);
                    states.Add(clone);

            }

            return states;
        }

        public void SolveBacktracking()
        {
            solveBacktracking(bridgeState);
        }

        public void solveBacktracking(BridgeState state)
        {
            if (state.elapsedTime <= 30)
            {
                if (IsBacktrackingSolution(state))
                {
                    Console.WriteLine("Solution found in: {0} seconds", bridgeState.elapsedTime);
                    solutions.Add(state);
                    return;
                }

                if (state.IsFlashLightAtHome() && state.moves.Count < 10)
                {
                    //var nextStates = GetNextMoveStates(state);
                    //foreach (var nextstate in nextStates)
                    //{
                    //    int slowestPlayer = state.leftSide.Max();
                    //    solveBacktracking(nextstate);
                    //    //UndoMoveRight(state, state.lastMove, slowestPlayer);
                    //}
                    List<Tuple<int, int>> nextStates = new List<Tuple<int, int>>(GetNextMoves(state));

                    foreach (Tuple<int, int> nextState in nextStates)
                    {
                        //BridgeState newState = new BridgeState(OBJECT_COUNT + "");
                        //OBJECT_COUNT++;
                        int slowestPlayer = state.leftSide.Max();
                        BridgeState newState = (BridgeState)state.Clone();
                        MoveRight(newState, nextState);
                        solveBacktracking(newState);
                        UndoMoveRight(newState, nextState, slowestPlayer);
                    }

                }
                else
                {
                    if (state.leftSide.Count > 0 && state.elapsedTime < 30)
                    {
                        //BridgeState newState = (BridgeState)state.Clone();
                        int fastestPlayer = state.rightSide.Min();

                        BringFlashlightHome(state);

                        solveBacktracking(state);

                        UndoBringFlashlightHome(state, fastestPlayer);
                    }
                    else
                    {
                        //solveBacktracking(new BridgeState(++OBJECT_COUNT + ""));
                        bool isSolution = state.IsSolution();
                    Console.WriteLine("Einde... @" + state.id + ", tijd: " + state.elapsedTime + ", solution: {0}", state.IsSolution());
                    return;
                }
            }
            }
        }

        public void PrintSolutions()
        {
            int i = 1;
            foreach(var solution in solutions)
            {
                Console.WriteLine("Solution #{0} in {1} seconds", i, solution.elapsedTime);
                foreach(var move in solution.moves)
                {
                    Console.WriteLine(move);
                }
                i++;
            }
        }

        private List<int> GetTupleAsList(Tuple<int, int> tuple)
        {
            List<int> tupleList = new List<int>
            {
                tuple.Item1,
                tuple.Item2
            };

            return tupleList;
        }

        public void UndoMoveRight(BridgeState state, Tuple<int, int> players, int slowestPlayer)
        {
            if (players == null)
                return;

            List<int> tupleAsList = GetTupleAsList(players);

            int size = state.moves.Count;
            if (size == 0)
                return;

            state.moves.RemoveAt(size - 1);

            state.leftSide.AddRange(tupleAsList);
            state.rightSide.RemoveAll(i => tupleAsList.Contains(i));
            state.elapsedTime -= slowestPlayer;

            state.torchSide = TorchSide.LEFT;
        }

        public void MoveRight(BridgeState state, Tuple<int, int> players)
        {
            List<int> tupleAsList = GetTupleAsList(players);

            int slowestPlayer = tupleAsList.Max();

            string rightMove = "--> player @ speed " + players.Item1 + " & player@speed " + players.Item2 + ", ID: " + state.id;
            state.moves.Add(rightMove);

            //Console.WriteLine("--> player@speed {0} & player@speed {1}" + ", ID: " + state.id, players.Item1, players.Item2);

            state.leftSide.RemoveAll(i => tupleAsList.Contains(i));
            state.rightSide.AddRange(tupleAsList);
            state.IncreaseElapsedTime(slowestPlayer);

            state.torchSide = TorchSide.RIGHT;
        }


        public void UndoBringFlashlightHome(BridgeState state, int fastestPlayer)
        {
            int size = state.moves.Count;
            if (size > 0)
                state.moves.RemoveAt(size - 1);

            //Console.WriteLine("<-- player@speed {0} " + ", ID: " + state.id, fastestPlayer);

            state.rightSide.Add(fastestPlayer);
            state.leftSide.Remove(fastestPlayer);
            state.elapsedTime -= fastestPlayer;
            state.torchSide = TorchSide.RIGHT;
        }

        public void BringFlashlightHome(BridgeState state)
        {
            if (state.torchSide == TorchSide.LEFT)
                return;
            int fastestPlayer = state.GetSmallestFromRightSIde();

            string leftMove = "<-- player@speed " + fastestPlayer + ", ID: " + state.id; ;
            state.moves.Add(leftMove);

            //Console.WriteLine("<-- player@speed {0} " + ", ID: " + state.id, fastestPlayer);

            state.rightSide.Remove(fastestPlayer);
            state.leftSide.Add(fastestPlayer);
            state.elapsedTime += fastestPlayer;
            state.torchSide = TorchSide.LEFT;
        }


        private bool CheckGameState()
        {
            return bridgeState.elapsedTime <= 30;
        }

        /*
         *  Check if there are seconds left and if everyone has moved to the left side of the bridge. 
         */
        private bool IsBacktrackingSolution(BridgeState state)
        {
            return state.IsSolution();
        }

        private bool IsDepthFirstSolution(BridgeState state)
        {
            return CheckGameState() && IsBacktrackingSolution(state);
        }
    }
}
