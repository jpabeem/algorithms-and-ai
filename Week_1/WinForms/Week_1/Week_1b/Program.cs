using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week_1b
{
    class Program
    {
        static void Main(string[] args)
        {
            //NQueens nq = new NQueens(4);
            //nq.SolveBacktracking();
            //nq.SolveDepthFirst();
            //Console.WriteLine("Solutions possible: {0}", nq.solutionCount);

            BridgeGame bg = new BridgeGame();
            bg.SolveBacktracking();

            BridgeGame test = new BridgeGame();
            Tuple<int, int> firstMove = Tuple.Create(1, 3);
            test.MoveRight(test.bridgeState, firstMove);
            test.bridgeState.MoveLeft(1);

            Tuple<int, int> secondMove = Tuple.Create(8, 12);
            test.MoveRight(test.bridgeState, secondMove);
            test.bridgeState.MoveLeft(3);

            Tuple<int, int> thirdMove = Tuple.Create(1, 3);
            test.MoveRight(test.bridgeState, thirdMove);
            test.bridgeState.MoveLeft(1);

            Tuple<int, int> fourthMove = Tuple.Create(1, 6);
            test.MoveRight(test.bridgeState, fourthMove);
            //Console.WriteLine(test.bridgeState.IsSolution());

            Console.WriteLine(bg.solutions.Count);
            bg.PrintSolutions();

            //List<Tuple<int, int>> results = bg.GetAllUniquePossibleMoves(bg.bridgeState);
            //foreach (var result in results)
            //{
            //    Console.WriteLine("Tuple: {0} & {1}", result.Item1, result.Item2);
            //}

            //List<BridgeState> nextStates = bg.GetNextMoveStates(bg.bridgeState);

            //foreach(var state in nextStates)
            //{
            //    Console.WriteLine(state.elapsedTime);
            //}


            //bg.SolveDepthFirst();

            Console.ReadLine();
        }


    }
}
