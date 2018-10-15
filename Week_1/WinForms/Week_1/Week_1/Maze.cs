using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Week_1
{
    public class Maze
    {
        public int[] internalArray { get; private set; }
        public int width { get; private set; }
        public int height { get; private set; }

        public List<Tuple<int, int>> drawableEdges { get; private set; }

        protected RichTextBox richTxtBox;


        public Maze(int width, int height, RichTextBox richTxtBox)
        {
            this.width = width;
            this.height = height;
            this.richTxtBox = richTxtBox;

            int arraySize = width * height;
            this.internalArray = new int[arraySize];
            this.InitializeArray(this.internalArray);
            this.drawableEdges = new List<Tuple<int, int>>();
        }

        public void ResetArrays()
        {
            InitializeArray(this.internalArray);
            drawableEdges.Clear();
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
                return Find(root);
            }
            else
            {
                return n;
            }
        }

        private void SetElementRoot(int element, int value)
        {
            this.internalArray[element] = value;
        }

        private void IncreaseElementSize(int element)
        {
            this.internalArray[element] = this.internalArray[element] - 1;
        }

        private void assertIsItem(int x)
        {
            if (x < 0 || x >= internalArray.Length)
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        private void assertIsRoot(int root)
        {
            assertIsItem(root);
            if(internalArray[root] >= 0)
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public void Union(int root1, int root2)
        {
            assertIsRoot(root1);
            assertIsRoot(root2);

            if (root1 == root2)
            {
                throw new ArgumentException();
            }

            if (internalArray[root2] < internalArray[root1])
                internalArray[root1] = root2;
            else
            {
                if (internalArray[root1] == internalArray[root2])
                    internalArray[root1]--;
                internalArray[root2] = root1;
            }

        }

        public void UnionBySize(int a, int b)
        {
            System.Console.WriteLine("A: {0}, B: {1}", a, b);
            int rootOfA = Find(a);
            int rootOfB = Find(b);

            int valueA = this.internalArray[rootOfA];
            int valueB = this.internalArray[rootOfB];

            if (Math.Abs(valueA) < Math.Abs(valueB))
            {
                SetElementRoot(a, b);
                IncreaseElementSize(rootOfB);
            }
            else
            {
                SetElementRoot(b, a);
                IncreaseElementSize(rootOfA);
            }
            PrintArray();
        }

        public int SetsLeft()
        {
            int setsLeft = 0;
            foreach(int number in this.internalArray)
            {
                if(number == -1 || number < -1)
                {
                    setsLeft++;
                }
            }

            return setsLeft;
        }

        public int GetRow(int element)
        {
            if (element > this.internalArray.Length - 1)
                throw new ArgumentOutOfRangeException();
            return element / this.height; // 23 / 5 = 4
        }

        public int GetColumn(int element)
        {
            if (element > this.internalArray.Length - 1)
                throw new ArgumentOutOfRangeException();
            return element % this.width;
        }

        //public BFS

        public Tuple<int, int> GetCoords(int element)
        {
            return Tuple.Create(GetRow(element), GetColumn(element));
        }

        public List<Tuple<int, int>> GetEdges(int element)
        {
            List<Tuple<int, int>> edges = new List<Tuple<int, int>>();

            int row = GetRow(element);
            int column = GetColumn(element);

            if (column + 1 < this.width)
                edges.Add(Tuple.Create(element, element + 1));
            if (row + 1 < this.height)
                edges.Add(Tuple.Create(element, element + this.width));

            return edges;
        }

        public List<Tuple<int, int>> GetAllEdges()
        {
            List<Tuple<int, int>> edges = new List<Tuple<int, int>>();

            for (int i = 0; i < this.internalArray.Length; i++)
            {
                List<Tuple<int, int>> indexEdges = GetEdges(i);
                foreach (Tuple<int, int> edge in indexEdges)
                {
                    edges.Add(edge);
                }
            }

            return edges;
        }

        public void GenerateMaze()
        {
            ResetArrays();
            int size = SetsLeft();
            Random random = new Random();
            List<Tuple<int, int>> edges = GetAllEdges();
            List<Tuple<int, int>> mazeEdges = new List<Tuple<int, int>>();

            while (size > 1)
            {
                // generate random edge (x, y) from edges array
                // remove edge from edges
                int randomEdgeIndex = random.Next(edges.Count);
                Tuple<int, int> tempEdge = edges[randomEdgeIndex];
                edges.Remove(tempEdge);

                int x = tempEdge.Item1;
                int y = tempEdge.Item2;
                int u = Find(x);
                int v = Find(y);
                if (u != v)
                {
                    //Union(u, v);
                    UnionBySize(u, v);
                }
                else
                {
                    mazeEdges.Add(tempEdge);
                }
                size = SetsLeft();
            }

            // add edges to the 'master' list
            drawableEdges.AddRange(mazeEdges);
            drawableEdges.AddRange(edges);
        }

        public void ClearScreen()
        {
            this.richTxtBox.Text = "";
        }

        public void PrintArray()
        {
            //this.ClearScreen();
            //int tempWidth = 0;

            //for (int i = 0; i < this.internalArray.Length; i++)
            //{
            //    if (tempWidth < this.width - 1)
            //    {
            //        System.Console.Write('*');
            //        if (this.internalArray[i] <= -1)
            //        {
            //            this.richTxtBox.AppendText(internalArray[i].ToString() + " ");
            //        }
            //        else
            //        {
            //            this.richTxtBox.AppendText(internalArray[i].ToString() + 1 + " ");
            //        }
            //        System.Console.Write(internalArray[i]);
            //        System.Console.WriteLine("{index {0} : {1}} ", i, internalArray[i]);
            //        tempWidth++;
            //    }
            //    else
            //    {
            //        if (this.internalArray[i] <= -1)
            //        {
            //            this.richTxtBox.Text += internalArray[i].ToString() + Environment.NewLine;

            //            this.richTxtBox.AppendText(internalArray[i].ToString() + " ");
            //        }
            //        else
            //        {
            //            this.richTxtBox.Text += (internalArray[i].ToString() + 1) + Environment.NewLine;
            //        }
            //        richTxtBox.AppendText(" *");
            //        System.Console.WriteLine(internalArray[i] + " ");
            //        System.Console.WriteLine("*");
            //        tempWidth = 0;
            //    }
            //}
            //richTxtBox.AppendText((string.Join(",", this.internalArray)));
        }
    }
}
