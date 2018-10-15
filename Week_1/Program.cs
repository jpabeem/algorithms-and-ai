using System;
// using Week_1;

namespace Week_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Week1a week1a = new Week1a(5, 5);

            // union by size

            week1a.UnionBySize(3, 2);
            // week1a.UnionBySize(3, 6);
            // week1a.UnionBySize(5, 1);
            // week1a.UnionBySize(3, 5);
            week1a.PrintArray();
        }
    }
}
