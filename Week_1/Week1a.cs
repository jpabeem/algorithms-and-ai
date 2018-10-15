namespace Week_1
{
    using System;

    public class Week1a
    {
        public int[] internalArray;
        public int width;

        public int height;

        public Week1a(int width, int height)
        {
            this.width = width;
            this.height = height;
            int arraySize = width * height;
            this.internalArray = new int[arraySize];
            this.InitializeArray(this.internalArray);
        }

        private void InitializeArray(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                internalArray[i] = -1;
            }
        }

        public int Find(int n)
        {
            int root = this.internalArray[n];
            if (root > -1)
            {
                return Find(n);
            }
            else
            {
                return root;
            }
        }

        private void SetElementRoot(int element, int value)
        {
            this.internalArray[element] = value;
        }

        private void IncreaseElementSize(int element)
        {
            this.internalArray[element] -= 1;
        }

        public void UnionBySize(int a, int b)
        {
            System.Console.WriteLine("A: {0}, B: {1}", a, b);
            int rootOfA = Find(a);
            int rootOfB = Find(b);

            if (Math.Abs(rootOfA) < Math.Abs(rootOfB))
            {
                SetElementRoot(a, b);
                IncreaseElementSize(b);
            }
            else
            {
                SetElementRoot(b, a);
                IncreaseElementSize(a);
            }

        }

        public void GenerateMaze()
        {
            int size = 100; // length of the set
            int[] edges;
            int[] maze;
            while (size > 1)
            {
                // generate random edge (x, y) from edges array
                // remove edge from edges
                int x = 1;
                int y = 2;
                int a = Find(x);
                int b = Find(y);
                if (a != b)
                {
                    UnionBySize(a, b);
                }
                else
                {
                    // add (x, y) to maze
                }

            }
        }

        public void GetAsAscii(int n)
        {
            Console.WriteLine("╒════════╕");
            if (n < 0)
            Console.WriteLine("│  n={0}│", n);
            else{
            Console.WriteLine("│  n={0}│", n);

            }
            Console.WriteLine("│        │");
            Console.WriteLine("╘════════╛");
        }

        public void PrintArray()
        {
            int tempWidth = 0;

            for (int i = 0; i < this.internalArray.Length; i++)
            {
                if (tempWidth < this.width - 1)
                {
                    // System.Console.Write('*');
                    if (this.internalArray[i] <= -1)
                        System.Console.Write(" * ");
                    else
                        System.Console.Write(internalArray[i]);
                    // System.Console.WriteLine("{index {0} : {1}} ", i, internalArray[i]);
                    tempWidth++;
                }
                else
                {
                    System.Console.WriteLine(internalArray[i] + " ");
                    // System.Console.WriteLine("*");
                    tempWidth = 0;
                }
            }
        }
    }
}