using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week_1b
{
    class NQueens
    {
        public bool[][] queens { get; private set; }
        public int solutionCount { get; private set; }
        int n;

        public bool ShowSolutions { get; set; }

        // initializes the board
        public NQueens(int n)
        {
            queens = new bool[n][];
            for (int i = 0; i < n; i++)
            {
                queens[i] = new bool[n];
            }
            this.n = n;
        }

        // driver method
        public void SolveBacktracking()
        {
            solveBacktracking(0);
        }

        void solveBacktracking(int row)
        {
           if (checkBoard())
            {
                if(isBacktrackingSolution())
                {
                    Print(queens);
                    solutionCount++;
                }
                else
                {
                    for(int i = 0; i < n; i++)
                    {
                        queens[row][i] = true;
                        solveBacktracking(row + 1);
                        queens[row][i] = false;
                    }
                }
            }
        }

        public void SolveDepthFirst()
        {
            solveDepthFirst(0);
        }

        private void solveDepthFirst(int row)
        {
            if(row == n)
            {
                if(isDepthFirstSolution())
                {
                    Print(queens);
                    solutionCount++;
                }
                return;
            }
            else
            {
                for(int i=0; i < n; i++)
                {
                    queens[i][row] = true;
                    solveDepthFirst(row + 1);
                    queens[i][row] = false;
                }
            }

        }

        /********** Helper methods **********/

        bool isDepthFirstSolution()
        {
            return (countQueens() == n) && checkBoard();
        }

        bool isBacktrackingSolution()
        {
            return countQueens() == n;
        }

        // counts the total number of queens on the board
        int countQueens()
        {
            return queens.Sum(line => line.Count(q => q));
        }

        // check if there is no conflicting situation on the board
        bool checkBoard()
        {
            // iterate through all rows
            for (int r = 0; r < n; r++)
            {
                int queenCol = -1;
                int nrOfQueens = 0;

                // check horizontally
                for (int c = 0; c < n; c++)
                {
                    if (queens[r][c])
                    {
                        nrOfQueens++;
                        queenCol = c;
                    }
                    if (nrOfQueens > 1)
                        return false;
                    if (nrOfQueens > 0)
                    {
                        // check column
                        for (int qr = r + 1; qr < n; qr++) //start from next row
                        {
                            if (queens[qr][queenCol])
                                return false; // there is another queen on this column
                        }

                        // check diagonal -> r
                        int dc = queenCol + 1;
                        for (int qr = r + 1; qr < n && dc < n; qr++) //start from next row
                        {
                            if (queens[qr][dc])
                                return false; // there is another queen on this column
                            dc++;
                        }
                        // check diagonal -> l
                        dc = queenCol - 1;
                        for (int qr = r + 1; qr < n && dc >= 0; qr++) //start from next row
                        {
                            if (queens[qr][dc])
                                return false; // there is another queen on this column
                            dc--;
                        }
                    }
                }
            }
            return true;
        }

        public void Print(bool[][] array)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    string s = array[i][j] ? "Q" : "-";
                    Console.Write(s);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
