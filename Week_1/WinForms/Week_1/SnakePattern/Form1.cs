using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakePattern
{
    public partial class Form1 : Form
    {
        public const int WIDTH = 5;
        public const int HEIGHT = 5;
        public const int SIZE = 50;
        public const int OFFSET = 5;

        public Graph graph { get; private set; }

        public Form1()
        {
            InitializeComponent();
            InitGraph();
            SetUndirectedGraph();
        }

        private void InitGraph()
        {
            graph = new Graph();

            for (int i = 0; i < (WIDTH * HEIGHT); i++)
            {
                graph.AddVertex(i.ToString());
            }
        }

        private void SetUndirectedGraph()
        {
            graph.AddEdge("0", "1", 0);
            graph.AddEdge("0", "5", 0);
            graph.AddEdge("1", "2", 0);
            graph.AddEdge("2", "3", 0);
            graph.AddEdge("3", "4", 0);
            graph.AddEdge("4", "9", 0);
            graph.AddEdge("5", "10", 0);
            graph.AddEdge("6", "2", 0);
            graph.AddEdge("6", "7", 0);
            graph.AddEdge("7", "8", 0);
            graph.AddEdge("8", "2", 0);
            graph.AddEdge("6", "10", 0);


            //graph.AddEdge("1", "2", 0);
            //graph.AddEdge("2", "6", 0);
            //graph.AddEdge("2", "8", 0);
            //graph.AddEdge("2", "3", 0);
            //graph.AddEdge("3", "4", 0);
            //graph.AddEdge("4", "9", 0);
            //graph.AddEdge("5", "10", 0);
            //graph.AddEdge("6", "7", 0);
            //graph.AddEdge("6", "10", 0);
            //graph.AddEdge("7", "12", 0);
            //graph.AddEdge("7", "8", 0);
            //graph.AddEdge("7", "13", 0);

        }

        private int GetRow(int element)
        {
            if (element > (WIDTH * HEIGHT) - 1)
                throw new ArgumentOutOfRangeException();
            return element / HEIGHT; // 23 / 5 = 4
        }

        private int GetColumn(int element)
        {
            if (element > (WIDTH * HEIGHT) - 1)
                throw new ArgumentOutOfRangeException();
            return element % WIDTH;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void DrawVertices(Graphics g, SolidBrush brush)
        {
            int x = 0;
            int y = 0;

            Pen edgePen = new Pen(Color.Red);

            for (int i = 0; i < HEIGHT; i++)
            {
                for (int j = 0; j < WIDTH; j++)
                {
                    int row = GetRow(i);
                    int column = GetColumn(j);
                    Rectangle rect = new Rectangle(x + (column * (SIZE + OFFSET)), y, SIZE, SIZE);
                    g.FillEllipse(brush, rect);
                }
                y += SIZE + OFFSET;
            }
        }

        private void DrawEdge(Graphics g, Pen p, int start, int destination)
        {
            if (start < 0 || destination > WIDTH * HEIGHT)
                return;

            int startRow = GetRow(start);
            int startColumn = GetColumn(start);

            int endRow = GetRow(destination);
            int endColumn = GetColumn(destination);

            Point startPoint = new Point(0, 0);
            Point endPoint = new Point(0, 0);

            if (startRow == endRow && startRow != 0 && startColumn + 1 == endColumn)
            {
                startPoint = new Point((SIZE / 2) + startColumn * (SIZE + OFFSET), SIZE / 2 + (startRow * SIZE + OFFSET));
                endPoint = new Point((SIZE / 2) + endColumn * (SIZE + OFFSET), SIZE / 2 + (endRow * (SIZE + OFFSET)));
            }
            else
            {
                startPoint = new Point((SIZE / 2) + startColumn * (SIZE + OFFSET), SIZE / 2 + (startRow * SIZE));
                endPoint = new Point((SIZE / 2) + endColumn * (SIZE + OFFSET), SIZE / 2 + (endRow * (SIZE + OFFSET)));
            }

            

            g.DrawLine(p, startPoint, endPoint);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode =
       System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            foreach (var vertex in graph.vertexMap)
            {
                var edges = vertex.Value.adj;
                foreach(var edge in edges)
                {
                    DrawEdge(g, new Pen(Color.Red), Int32.Parse(vertex.Value.name), Int32.Parse(edge.Dest.name));
                }
            }

            DrawVertices(g, new SolidBrush(Color.Blue));
        }
    }
}
