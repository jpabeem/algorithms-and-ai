using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Week_1
{
    public enum Walls
    {
        NO_WALLS = -1,
        ALL_WALLS,
        START_WALL,
        END_WALL,
        END_OF_ROW_WALL,
        FIRST_OF_LAST_ROW_WALL,
        NORTH_WALL,
        SOUTH_WALL,
        RIGHT_WALL,
        LEFT_WALL
    }

    public partial class Form1 : Form
    {
        public Maze Maze { get; private set; }

        public Walls WallOption { get; set; }

        public Graph Graph { get; private set; }

        public const int GRID_SIZE = 20;

        public const int WALK_SPEED = 200;

        public bool MazeWalking { get; private set; }

        public int waitInMs { get; private set; }

        public Form1()
        {
            InitializeComponent();

            this.WallOption = Walls.ALL_WALLS;
            this.waitInMs = 15;
            wallOptionLblValue.Text = "" + this.WallOption;

            groupBox1.Visible = false;
            this.Maze = new Maze(15, 15, outputTxtBox);
            this.Maze.PrintArray();

            chkBoxMzWalking.CheckState = CheckState.Checked;

            List<Tuple<int, int>> allEdges = new List<Tuple<int, int>>();
            for (int i = 0; i < Maze.internalArray.Length; i++)
            {
                List<Tuple<int, int>> edges = Maze.GetEdges(i);
                foreach (Tuple<int, int> edge in edges)
                {
                    this.LogOutput("(" + edge.Item1 + ", " + edge.Item2 + ")");
                    allEdges.Add(edge);
                }
            }

            this.LogOutput("Length: " + allEdges.Count);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClearScreen();
            try
            {
                int width = Int32.Parse(textBox1.Text);
                int height = Int32.Parse(textBox2.Text);
                this.Maze = new Maze(width, height, outputTxtBox);
                LogOutput("New maze initialized, width: " + width + ", height: " + height);
                DrawStart();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
         
        }

        private void outputTxtBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int x = Int32.Parse(textBox1.Text);
            int y = Int32.Parse(textBox2.Text);
            this.Maze.UnionBySize(x, y);
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            int gridSize = 50;
            int tempWidth = 0;
            int x1 = 0;
            int x2 = 0;
            int y1 = 0;
            int y2 = 0;

            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.FromArgb(255, 0, 0, 0));

            // draw maze outlines
            g.DrawRectangle(pen, 0, 0, Maze.width * gridSize, Maze.height * gridSize);

            for (int i = 0; i < Maze.internalArray.Length; i++)
            {
                if (tempWidth < Maze.width - 1)
                {
                    g.DrawLine(pen, x1, y1 + gridSize, x2 + gridSize, y2 + gridSize);
                    x1 += gridSize;
                    x2 += gridSize;
                    tempWidth++;
                }
                else
                {
                    tempWidth = 0;
                    x1 = 0;
                    x2 = 0;
                    y1 += gridSize;
                    y2 += gridSize;
                    g.DrawLine(pen, x1 + gridSize, y1, x2 + gridSize, y2 + gridSize);
                }
            }
        }

        private void LogOutput(string input)
        {
            outputTxtBox.AppendText(Environment.NewLine + input);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int index = Int32.Parse(elementNumTxtBox.Text);
            int row = Maze.GetRow(index); // 23 / 5 = 4
            int column = Maze.GetColumn(index);

            outputTxtBox.AppendText(Environment.NewLine + "Index: " + index + "@row: " + row.ToString() + ", @column: " + column.ToString());
        }

        private void DrawStart()
        {
            for (int element = 0; element < (this.Maze.width * this.Maze.height); element++)
            { 
                int row = Maze.GetRow(element);
                int column = Maze.GetColumn(element);
                int maxRow = Maze.GetRow((Maze.width * Maze.height) - 1);

                // first level
                if (Maze.GetRow(element) == 0)
                {
                    if (column == (Maze.width - 1))
                        this.WallOption = Walls.END_OF_ROW_WALL;
                    else
                        this.WallOption = Walls.NORTH_WALL;
                    DrawLine(element, new Pen(Color.Black));
                }

                // middle level
                if(row != maxRow && row != 0)
                {
                    if (column == 0)
                    {
                        this.WallOption = Walls.LEFT_WALL;
                    }else if (column == (Maze.width - 1))
                    {
                        this.WallOption = Walls.RIGHT_WALL;
                    }
                    else
                    {
                        this.WallOption = Walls.NO_WALLS;
                    }
                    DrawLine(element, new Pen(Color.Black));
                }

                // end level
                if (row == maxRow)
                {
                    if (column == 0)
                    {
                        WallOption = Walls.FIRST_OF_LAST_ROW_WALL;
                    }
                    else
                    {
                        WallOption = Walls.SOUTH_WALL;
                    }
                    DrawLine(element, new Pen(Color.Black));
                }
            }
        }

        private void DrawVertex(Vertex v)
        {
            LogOutput(string.Format("Vertex {0} -->", v.name));
            // get row & column
            int row = Maze.GetRow(Int32.Parse(v.name));
            int column = Maze.GetColumn(Int32.Parse(v.name));

            Graphics gr = panel1.CreateGraphics();
            Pen pen = new Pen(Color.Blue);

            Point leftUpperCorner = new Point(0 + (column * GRID_SIZE), 0 + (row * GRID_SIZE));
            Point rightBottomCorner = new Point(GRID_SIZE + (column * GRID_SIZE), GRID_SIZE + (row * GRID_SIZE));

            Rectangle rec = new Rectangle(leftUpperCorner, new Size(rightBottomCorner));
            int offset = GRID_SIZE / 4;
            int size = GRID_SIZE / 2;
            //gr.DrawRectangle(pen, rec);
            int x = (((0 + (column * GRID_SIZE)) + (GRID_SIZE + (column * GRID_SIZE))) / 2) - offset;
            int y = ((row * GRID_SIZE) + (GRID_SIZE / 2)) - offset;

            Brush b = new SolidBrush(Color.Blue);

            //gr.FillRectangle(b, new Rectangle(3 + (column * GRID_SIZE), 3 + (row * GRID_SIZE), GRID_SIZE - 3, GRID_SIZE - 3));
            gr.FillEllipse(b, x, y, size, size);

            if (MazeWalking)
                Thread.Sleep(WALK_SPEED);
            else
                Thread.Sleep(this.waitInMs);
        }

        private void DrawEdge(Tuple<int, int> edge)
        {
            int element1 = edge.Item1;
            int element2 = edge.Item2;

            if(Maze.GetRow(element1) == Maze.height - 1 && Maze.GetColumn(element1) == Maze.height - 2)
            {
                return;
            }

            if (element1 == (element2 - 1))
            {
                // draw right wall
                this.WallOption = Walls.RIGHT_WALL;
            } else
            {
                this.WallOption = Walls.SOUTH_WALL;
            }

            DrawLine(element1, new Pen(Color.Red));
            Thread.Sleep(this.waitInMs);
        }

        private void DrawLine(int element, Pen p)
        {
            Graphics gr = panel1.CreateGraphics();
            //Pen pen = new Pen(Color.FromArgb(255, 0, 0, 0));
            Pen pen = p;
            //try
            //{
                //if (this.WallOption != 0 && !wallRadioLeft.Checked && !wallRadioRight.Checked && !wallRadioNorth.Checked && !wallRadioSouth.Checked)
                //    throw new InvalidEnumArgumentException();

                int row = Maze.GetRow(element); // 23 / 5 = 4
                int column = Maze.GetColumn(element);

                switch (this.WallOption)
                {
                    case Walls.ALL_WALLS:
                        // NORTH
                        gr.DrawLine(pen, 0 + (column * GRID_SIZE), 0 + (row * GRID_SIZE), GRID_SIZE + (column * GRID_SIZE), 0 + (row * GRID_SIZE));
                        // SOUTH
                        gr.DrawLine(pen, 0 + (column * GRID_SIZE), GRID_SIZE + (row * GRID_SIZE), GRID_SIZE + (column * GRID_SIZE), GRID_SIZE + (row * GRID_SIZE));
                        // LEFT
                        gr.DrawLine(pen, 0 + (column * GRID_SIZE), 0 + (row * GRID_SIZE), 0 + (column * GRID_SIZE), GRID_SIZE + (row * GRID_SIZE));
                        // RIGHT
                        gr.DrawLine(pen, GRID_SIZE + (column * GRID_SIZE), 0 + (row * GRID_SIZE), GRID_SIZE + (column * GRID_SIZE), GRID_SIZE + (row * GRID_SIZE));
                        break;
                    case Walls.START_WALL:
                        // NORTH
                        gr.DrawLine(pen, 0 + (column * GRID_SIZE), 0 + (row * GRID_SIZE), GRID_SIZE + (column * GRID_SIZE), 0 + (row * GRID_SIZE));
                        // SOUTH
                        gr.DrawLine(pen, 0 + (column * GRID_SIZE), GRID_SIZE + (row * GRID_SIZE), GRID_SIZE + (column * GRID_SIZE), GRID_SIZE + (row * GRID_SIZE));
                        // RIGHT
                        gr.DrawLine(pen, GRID_SIZE + (column * GRID_SIZE), 0 + (row * GRID_SIZE), GRID_SIZE + (column * GRID_SIZE), GRID_SIZE + (row * GRID_SIZE));
                        break;
                    case Walls.END_WALL:
                        // NORTH
                        gr.DrawLine(pen, 0 + (column * GRID_SIZE), 0 + (row * GRID_SIZE), GRID_SIZE + (column * GRID_SIZE), 0 + (row * GRID_SIZE));
                        // SOUTH
                        gr.DrawLine(pen, 0 + (column * GRID_SIZE), GRID_SIZE + (row * GRID_SIZE), GRID_SIZE + (column * GRID_SIZE), GRID_SIZE + (row * GRID_SIZE));
                        // LEFT
                        gr.DrawLine(pen, 0 + (column * GRID_SIZE), 0 + (row * GRID_SIZE), 0 + (column * GRID_SIZE), GRID_SIZE + (row * GRID_SIZE));
                        break;
                    case Walls.END_OF_ROW_WALL:
                        // NORTH
                        gr.DrawLine(pen, 0 + (column * GRID_SIZE), 0 + (row * GRID_SIZE), GRID_SIZE + (column * GRID_SIZE), 0 + (row * GRID_SIZE));
                        // RIGHT
                        gr.DrawLine(pen, GRID_SIZE + (column * GRID_SIZE), 0 + (row * GRID_SIZE), GRID_SIZE + (column * GRID_SIZE), GRID_SIZE + (row * GRID_SIZE));
                        break;
                    case Walls.FIRST_OF_LAST_ROW_WALL:
                        // SOUTH
                        gr.DrawLine(pen, 0 + (column * GRID_SIZE), GRID_SIZE + (row * GRID_SIZE), GRID_SIZE + (column * GRID_SIZE), GRID_SIZE + (row * GRID_SIZE));
                        // LEFT
                        gr.DrawLine(pen, 0 + (column * GRID_SIZE), 0 + (row * GRID_SIZE), 0 + (column * GRID_SIZE), GRID_SIZE + (row * GRID_SIZE));
                    break;
                    case Walls.NORTH_WALL:
                        gr.DrawLine(pen, 0 + (column * GRID_SIZE), 0 + (row * GRID_SIZE), GRID_SIZE + (column * GRID_SIZE), 0 + (row * GRID_SIZE));
                        break;
                    case Walls.SOUTH_WALL:
                        gr.DrawLine(pen, 0 + (column * GRID_SIZE), GRID_SIZE + (row * GRID_SIZE), GRID_SIZE + (column * GRID_SIZE), GRID_SIZE + (row * GRID_SIZE));
                        break;
                    case Walls.LEFT_WALL:
                        gr.DrawLine(pen, 0 + (column * GRID_SIZE), 0 + (row * GRID_SIZE), 0 + (column * GRID_SIZE), GRID_SIZE + (row * GRID_SIZE));
                        break;
                    case Walls.RIGHT_WALL:
                        gr.DrawLine(pen, GRID_SIZE + (column * GRID_SIZE), 0 + (row * GRID_SIZE), GRID_SIZE + (column * GRID_SIZE), GRID_SIZE + (row * GRID_SIZE));
                        break;
                }

                //______________________X1__________________________________Y1______________X2_________________________________Y2___________________________         
                //gr.DrawLine(pen, 0 + (column * GRID_SIZE), GRID_SIZE + (row * GRID_SIZE), GRID_SIZE + (column * GRID_SIZE), GRID_SIZE + (row * GRID_SIZE));

                //gr.DrawLine(pen, GRID_SIZE + (column * GRID_SIZE), 0 + (row * GRID_SIZE), GRID_SIZE + (column * GRID_SIZE), GRID_SIZE + (row * GRID_SIZE));

                this.LogOutput("Index: " + element + ", X1: " + (0 + (column * GRID_SIZE)) + ", Y1: " + (GRID_SIZE + (row * GRID_SIZE) + ", X2: " + (GRID_SIZE + (column * GRID_SIZE)) + ", Y2: " + (GRID_SIZE + (row * GRID_SIZE))));
            }

        private void button4_Click(object sender, EventArgs e)
        {
            Graphics gr = panel1.CreateGraphics();

            try
            {
                int index = Int32.Parse(elementNumTxtBox.Text);
                this.DrawLine(index, new Pen(Color.Black));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.FromArgb(255, 0, 0, 0));
            Pen transPen = new Pen(Color.FromArgb(128, 255, 255, 255));
            // draw maze outlines
            //g.DrawRectangle(pen, 0, 0, Maze.width * GRID_SIZE, Maze.height * GRID_SIZE);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox1.Checked)
            {
                this.WallOption = Walls.NO_WALLS;
                groupBox1.Show();
            } else
            {
                this.WallOption = Walls.ALL_WALLS;
                groupBox1.Hide();
            }

            wallOptionLblValue.Text = "" + this.WallOption;
        }

        private void wallRadioNorth_CheckedChanged(object sender, EventArgs e)
        {
            this.WallOption = Walls.NORTH_WALL;
            wallOptionLblValue.Text = "" + this.WallOption;
        }

        private void wallRadioSouth_EnabledChanged(object sender, EventArgs e)
        {
            this.WallOption = Walls.SOUTH_WALL;
            wallOptionLblValue.Text = "" + this.WallOption;
        }

        private void wallRadioRight_CheckedChanged(object sender, EventArgs e)
        {
            this.WallOption = Walls.RIGHT_WALL;
            wallOptionLblValue.Text = "" + this.WallOption;
        }

        private void wallRadioLeft_CheckedChanged(object sender, EventArgs e)
        {
            this.WallOption = Walls.LEFT_WALL;
            wallOptionLblValue.Text = "" + this.WallOption;
        }

        private void wallRadioSouth_CheckedChanged(object sender, EventArgs e)
        {
            this.WallOption = Walls.SOUTH_WALL;
            wallOptionLblValue.Text = "" + this.WallOption;
        }

        private bool isFirstElement(int element)
        {
            return element == 0;
        }

        private bool isLastElement(int element)
        {
            return element == ((this.Maze.width * this.Maze.height) - 1);
        }

        private void drawAllWallsBtn_Click(object sender, EventArgs e)
        {
            drawAllMazeWalls();
        }

        private void drawAllMazeWalls()
        {
            for (int element = 0; element < (this.Maze.width * this.Maze.height); element++)
            {
                if (isFirstElement(element))
                {
                    this.WallOption = Walls.START_WALL;
                }
                else if (isLastElement(element))
                {
                    this.WallOption = Walls.END_WALL;
                }
                DrawLine(element, new Pen(Color.Black));
                this.WallOption = Walls.ALL_WALLS;
                Thread.Sleep(this.waitInMs);
            }
        }

        private void drawExperimentalMaze()
        {
            Random random = new Random();
            for (int element = 0; element < (this.Maze.width * this.Maze.height); element++)
            {
                this.WallOption = (Walls) random.Next(-1, 6);
                DrawLine(element, new Pen(Color.Black));
                Thread.Sleep(this.waitInMs);
            }
        }

        private void ClearScreen()
        {
            panel1.Refresh();
            outputTxtBox.Text = "";
        }

        private void drawExperimentalBtn_Click(object sender, EventArgs e)
        {
            ClearScreen();
            drawExperimentalMaze();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            ClearScreen();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            this.waitInMs = trackBar1.Value;
            setWaitInMsLabel();
        }

        private void setWaitInMsLabel()
        {
            msDelayLbl.Text = this.waitInMs + " ms";
        }

        private void setTrackbar()
        {
            this.trackBar1.Value = this.waitInMs;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            setWaitInMsLabel();
            setTrackbar();
        }

        private void DrawShortestPath(HashSet<Vertex> vertices)
        {
            foreach (Vertex v in vertices)
            {
                // draw center points
                DrawVertex(v);
            }
        }

        private void DrawShortestPath(List<Vertex> vertices)
        {
            Maze.ClearScreen();
            LogOutput(string.Format("Path was found in {0} steps", vertices.Count - 1));

            foreach(Vertex v in vertices)
            {
                // draw center points
                DrawVertex(v);
            }
        }

        private void DrawMazeEdges()
        {
            int edges = 0;
            int fifth = Maze.drawableEdges.Count / 3;
            int wait = this.waitInMs;

            foreach (Tuple<int, int> edge in Maze.drawableEdges)
            {
                if(edges == fifth)
                {
                    this.waitInMs = 0;
                }
                DrawEdge(edge);
                edges++;
            }

            this.waitInMs = wait;
        }

        private void runMazeBtn_Click(object sender, EventArgs e)
        {
            ClearScreen();
            DrawStart();

            BackgroundWorker bg = new BackgroundWorker();
            bg.DoWork += new DoWorkEventHandler(bg_DoWork);
            bg.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bg_RunWorkerCompleted);

            bg.RunWorkerAsync();
        }

        private void drawMazeOutlinesBtn_Click(object sender, EventArgs e)
        {
            DrawStart();
        }

        private void bg_DoWork(object sender, DoWorkEventArgs e)
        {
            Maze.GenerateMaze();
        }

        private void bg_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            DrawMazeEdges();

            Console.WriteLine("[{0}]", string.Join(", ", Maze.drawableEdges));
            Console.WriteLine("Drawable edges count: {0}", Maze.drawableEdges.Count);
            Console.WriteLine("[{0}]", string.Join(", ", Maze.internalArray));
        }

        /*
         * Generate vertices with the string format: "row;column" <-- TODO: change format
         */
        private List<Vertex> GenerateVertices()
        {
            List<Vertex> vertices = new List<Vertex>();
            for (int i = 0; i < Maze.internalArray.Length; i++)
                vertices.Add(new Vertex(i.ToString()));
                //vertices.Add(new Vertex(Maze.GetRow(i) + ";" + Maze.GetColumn(i)));
            return vertices;
        }

        private void btnMakeGraph_Click(object sender, EventArgs e)
        {
            ClearScreen();
            // generate the vertices
            List <Vertex> vertices = GenerateVertices();

            // generate maze
            Maze.GenerateMaze();

            // show edges
            DrawStart();
            DrawMazeEdges();

            // retrieve all possible edges
            var allEdges = Maze.GetAllEdges();

            // retrieve drawable edges (the edges the maze is made out of)
            var tupleEdges = new List<Tuple<int, int>>(Maze.drawableEdges);

            var finalResult = allEdges.Where(i => !tupleEdges.Contains(i)).ToList();

            Console.WriteLine("[{0}]", string.Join(", ", finalResult));

            this.Graph = new Graph(vertices, finalResult, true);

            Graph.Unweighted(0.ToString());
            int mazeEnd = Maze.internalArray.Length - 1;
            Graph.PrintPath(mazeEnd.ToString());

            Graph.GetPath(mazeEnd.ToString());

            var list = Graph.verticesPath;
            DrawShortestPath(list);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            this.MazeWalking = chkBoxMzWalking.Checked;
        }
    }
}
