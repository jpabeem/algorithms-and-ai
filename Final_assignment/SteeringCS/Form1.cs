using SteeringCS.behaviour;
using SteeringCS.entity;
using SteeringCS.util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SteeringCS.util.pathplanning;
using SteeringCS.world;
using SteeringCS.util.goals;

namespace SteeringCS
{
    public enum Pathfinding
    {
        AStar,
        DIJKSTRA
    }

    public partial class Form1 : Form
    {
        public World world;
        private delegate void DrawGraphInPanel(Graphics g);

        public Bitmap buffer;

        public Pathfinding PathFindingAlgorithm { get; set; }

        public IHeuristic Heuristic { get; set; }

        public bool GraphDrawn = false;

        public bool ForceRedraw = false;

        private int TotalFrames = 0;

        private static int LastTick;
        private static int LastFrameRate;
        private static int FrameRate;

        private readonly long TickStart = 0;

        /// <summary>
        /// Show the generated shortest path.
        /// </summary>
        private bool ShowGeneratedPath
        {
            get
            {
                return showPathToolStripMenuItem1.Checked;
            }
            set
            {
                showPathToolStripMenuItem1.Checked = value;
                world.Settings.Set("ShowGeneratedPath", value);
            }
        }

        /// <summary>
        /// Show the visited vertices and edges.
        /// </summary>
        private bool ShowVisited
        {
            get
            {
                return showVisitedToolStripMenuItem1.Checked;
            }
            set
            {
                showVisitedToolStripMenuItem1.Checked = value;
            }
        }

        /// <summary>
        /// Show the target of the pathfinding algorithm.
        /// </summary>
        private bool ShowTarget
        {
            get
            {
                return showTargetToolStripMenuItem.Checked;
            }
            set
            {
                showTargetToolStripMenuItem.Checked = value;
                if (world.Mode == CursorMode.DEBUG_INFO)
                {
                    world.Settings.Set("ShowTarget", value);
                }
            }
        }

        /// <summary>
        /// Enforce the non penetration constraint.
        /// </summary>
        private bool EnforceNonPenetrationConstraint
        {
            get
            {
                return npcToolStripMenuItem.Checked;
            }
            set
            {
                npcToolStripMenuItem.Checked = value;
                world.Settings.Set("EnforceNonPenetrationConstraint", value);
            }
        }

        /// <summary>
        /// Show the spatial grid.
        /// </summary>
        private bool ShowSpatialGrid
        {
            get
            {
                return showSpatialGridToolStripMenuItem.Checked;
            }
            set
            {
                world.Settings.Set("ShowSpatialGrid", value);
                showSpatialGridToolStripMenuItem.Checked = value;
            }
        }

        /// <summary>
        /// Show the adjacent buckets.
        /// </summary>
        private bool ShowAdjacentBuckets
        {
            get
            {
                return showAdjacentBucketsToolStripMenuItem.Checked;
            }
            set
            {
                showAdjacentBucketsToolStripMenuItem.Checked = value;
            }
        }

        /// <summary>
        /// Show the current goal.
        /// </summary>
        private bool ShowGoal
        {
            get
            {
                return showGoalToolStripMenuItem.Checked;
            }
            set
            {
                showGoalToolStripMenuItem.Checked = value;
            }
        }

        private bool AutoArbitration
        {
            get
            {
                return autoArbitrationToolStripMenuItem.Checked;
            }
            set
            {
                autoArbitrationToolStripMenuItem.Checked = value;
            }
        }

        System.Timers.Timer timer;

        public const float timeDelta = 0.8f;

        public Form1()
        {
            InitializeComponent();
            WorldSingleton.Panel = dbPanel1;
            world = WorldSingleton.Instance;

            Settings settingsForm = new Settings(world);

            settingsForm.Show();

            world.drawnPath = new List<Vertex>();
            PathFindingAlgorithm = Pathfinding.AStar;
            Heuristic = new Euclidean();

            timer = new System.Timers.Timer();
            timer.Elapsed += Timer_Elapsed;
            timer.Interval = 20;
            timer.Enabled = true;
            TickStart = Clock.GetCurrentTimeInMillis();
            InitSettings();
        }

        private void CalculateFPS()
        {
            if (System.Environment.TickCount - LastTick >= 1000)
            {
                LastFrameRate = FrameRate;
                FrameRate = 0;
                LastTick = System.Environment.TickCount;
            }
            FrameRate++;

            /* Update the framerate in the game world, this is used in some cases to 
             * calculate a future position or the time it takes to get to a point.
             */
            world.FrameRate = LastFrameRate;

            lblDisplayFPS.Text = string.Format("FPS: {0}", LastFrameRate);
        }

        private void InitSettings()
        {
            world.Mode = CursorMode.SEEK;
            seekToolStripMenuItem1.Checked = true;
            aToolStripMenuItem.Checked = true;
            euclideanToolStripMenuItem.Checked = true;
            showPathToolStripMenuItem1.Checked = true;
            showVisitedToolStripMenuItem1.Checked = true;
            showTargetToolStripMenuItem.Checked = true;
            npcToolStripMenuItem.Checked = true;
            ShowAdjacentBuckets = true;
            ShowGoal = true;
            AutoArbitration = false;
            world.Settings.Set("EnforceNonPenetrationConstraint", true);

            comboBoxBehaviour.DataSource = Enum.GetValues(typeof(Behaviour));
            comboBoxBehaviour.SelectedItem = Behaviour.PURSUIT;

            InitToolStripStatusLabels();
            HandleCursorUpdate();
            HandleAlgorithmUpdate();
            HandleHeuristicUpdate();
        }

        private void InitToolStripStatusLabels()
        {
            toolStripSpatialGridStatus.Text = "Spatial grid: off";
            toolStripLblShowGraph.Text = "Grid: off";
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            world.Update(timeDelta);
            world.HandleNewEntities();

            dbPanel1.Invalidate();
            TotalFrames += 1;
        }

        private void dbPanel1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            //e.Graphics.DrawImageUnscaled(buffer, Point.Empty);

            dbPanel1.BackgroundImage = SteeringCS.Properties.Resources.fishing_background;

            DrawGraph(e.Graphics);

            DrawSpatialGrid(e.Graphics);

            world.Render(e.Graphics);

            HandleFishStatusLabels();

            RefreshFormElements();

            RenderGoals(e.Graphics);

            if (world.DebugEntity != null)
                HandleEntityDebugLabels();

            CalculateFPS();
        }

        private void HandleFishStatusLabels()
        {
            lblBurriedFish.Text = string.Format("Buried fish: {0}", world.BuriedFish);
            lblBurriedFish.Refresh();

            lblHunger.Text = string.Format("Hunger: {0}", world.Fishy.Hunger);
            lblHunger.Refresh();
        }

        private void RefreshFormElements()
        {
            menuStrip1.Refresh();
            statusStrip1.Refresh();
            grpBoxDebug.Refresh();
        }

        private string GetTypeName(Type t)
        {
            if (!t.IsGenericType) return t.Name;
            if (t.IsNested && t.DeclaringType.IsGenericType) throw new NotImplementedException();
            string txt = t.Name.Substring(0, t.Name.IndexOf('`')) + "<";
            int cnt = 0;
            foreach (Type arg in t.GetGenericArguments())
            {
                if (cnt > 0) txt += ", ";
                txt += GetTypeName(arg);
                cnt++;
            }
            return txt + ">";
        }

        private void RenderGoal(Graphics g, Goal goal, Fishy fishy, int offsetX, int offsetY)
        {
            //offsetY += 15;
            var goalString = string.Format("{0}", GetTypeName(goal.GetType()));
            var goalFont = new Font("Arial", 12);
            if (goal.GetType() == typeof(GoalThink))
                goalFont = new Font("Arial", 12, FontStyle.Bold);
            var goalBrush = new SolidBrush(Color.Black);
            var goalFormat = new StringFormat();
            g.DrawString(goalString, goalFont, goalBrush, (float)fishy.Pos.X + offsetX, (float)fishy.Pos.Y + offsetY, goalFormat);
        }

        private void RenderSubgoals(Graphics g, Goal goal, Fishy fishy, int offsetX, int offsetY)
        {
            offsetY += 15;
            // render main goal first => then subgoals

            if (goal is CompositeGoal composite)
            {
                RenderGoal(g, goal, fishy, offsetX, offsetY);

                offsetX += 30;

                lock (composite.SubGoals)
                {
                    for (int i = 0; i < composite.SubGoals.Count; i++)
                    {
                        var c = composite.SubGoals.ElementAt(i);
                        offsetY += 15;
                        RenderSubgoals(g, c, fishy, offsetX, offsetY);
                    }
                }
            }
            else // if a goal is an atomic goal, we've hit the base case for recursion
            {
                RenderGoal(g, goal, fishy, offsetX, offsetY);
            }
        }
        private void RenderGoals(Graphics graphics)
        {
            if (ShowGoal)
            {
                var fishy = world.Fishy;

                int offsetX = 30;
                int offsetY = 0;

                RenderSubgoals(graphics, fishy.Brain, fishy, offsetX, offsetY);
            }
        }

        private void HandleBucketIntersection(Tuple<int, int> rowAndcol, Rectangle rect)
        {
            var grid = world.WorldGrid;
            // check collision with obstacles
            foreach (var obstacle in world.obstacles)
            {
                if (EntityHelper.Intersects(rect, obstacle))
                    grid.AddToBucket(new Point(rowAndcol.Item1, rowAndcol.Item2), obstacle);
            }
        }

        private void DrawSpatialGrid(Graphics graphics)
        {
            if (ShowSpatialGrid)
            {
                var grid = world.WorldGrid;
                Pen pen = new Pen(Color.Black);
                int x = 0, y = 0;


                for (int row = 0; row < grid.RowCount; row++)
                {
                    for (int column = 0; column < grid.ColumnCount; column++)
                    {
                        x = 4 + column * grid.CellSize;
                        y = 4 + row * grid.CellSize;

                        var bucket = grid.GetBucket(row, column);
                        bucket.Reset();

                        Point drawingPoint = new Point(x, y);
                        Size drawingSize = new Size(new Point(grid.CellSize, grid.CellSize));

                        Rectangle rect = new Rectangle(drawingPoint, drawingSize);
                        graphics.DrawRectangle(pen, rect);

                        HandleBucketIntersection(new Tuple<int, int>(row, column), rect);

                        // draw the number of entities within each bucket
                        if (bucket.Entities.Count > 0)
                        {
                            string drawString = string.Format("{0}", bucket.Entities.Count);
                            System.Drawing.Font drawFont = new System.Drawing.Font("Arial", 12);
                            System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
                            System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();
                            graphics.DrawString(drawString, drawFont, drawBrush, x + grid.CellSize / 2 - 4, y + grid.CellSize / 2 - 4, drawFormat);
                        }

                        // draw the adjacent buckets
                        if (world.Mode == CursorMode.DEBUG_INFO && world.DebugEntity != null && ShowAdjacentBuckets)
                        {
                            if (world.DebugEntity.GetType() != typeof(Vehicle))
                                continue;

                            var debugBuckets = world.WorldGrid.GetAllAdjacentBuckets(world.DebugEntity);

                            int debugColumns = 0, debugRows = 0;
                            Rectangle debugRect;
                            Pen debugPen = new Pen(Color.Red);
                            Point debugDrawingPoint = new Point(4, 4);

                            // string drawing preperation variables
                            string debugString = string.Format("X");
                            System.Drawing.Font debugFont = new System.Drawing.Font("Arial", 12, FontStyle.Bold);
                            System.Drawing.SolidBrush debugBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
                            System.Drawing.StringFormat debugFormat = new System.Drawing.StringFormat();

                            foreach (var debugBucket in debugBuckets)
                            {
                                if (bucket != null)
                                {
                                    // draw Rectangle
                                    debugColumns = 4 + debugBucket.Column * grid.CellSize;
                                    debugRows = 4 + debugBucket.Row * grid.CellSize;

                                    debugDrawingPoint.X = debugRows;
                                    debugDrawingPoint.Y = debugColumns;

                                    graphics.DrawString(debugString, debugFont, debugBrush, debugRows + grid.CellSize / 2, debugColumns + grid.CellSize / 2, debugFormat);

                                    debugRect = new Rectangle(debugDrawingPoint, drawingSize);
                                    //graphics.DrawRectangle(debugPen, debugRect);
                                }
                            }
                        }
                    }
                }
            }

        }

        private void HandleNewVehicle()
        {
            if (world.Settings.Get("HandleNewVehicle"))
            {
                world.Settings.Set("HandleNewVehicle", false);

                // turn on entity debug labels
                HandleDebugBox(visible: true);
                ToggleEntityDebugLabels(enabled: true);
                HandleEntityDebugLabels();
            }
        }

        private void DrawGraph(Graphics g)
        {
            // draw the graph including edges
            if (world.Settings.Get("GraphEnabled"))
            {
                foreach (var keyValuePair in world.WorldGraph.vertexMap)
                {
                    Vertex vertex = keyValuePair.Value;
                    var edges = vertex.adj;
                    string[] split = vertex.name.Split(';');

                    int leftCorner = Int32.Parse(split[0]) - 1; // x
                    int rightCorner = Int32.Parse(split[1]) - 1; // y
                    foreach (var edge in edges)
                    {
                        string[] edgeSplit = edge.Dest.name.Split(';');
                        g.DrawLine(new Pen(Color.Green),
                            new Point(Int32.Parse(split[0]), Int32.Parse(split[1])),
                            new Point(Int32.Parse(edgeSplit[0]), Int32.Parse(edgeSplit[1]))
                        );
                    }

                    g.DrawEllipse(new Pen(Color.Blue), new Rectangle(leftCorner, rightCorner, 2, 2));

                }
            }

            // draw the generated path
            if (world.drawnPath.Count > 0 && ShowGeneratedPath)
            {
                for (int vrtx = 0; vrtx < world.drawnPath.Count; vrtx++)
                {
                    var vertex = world.drawnPath[vrtx];
                    var edges = vertex.adj;
                    string[] split = vertex.name.Split(';');

                    int leftCorner = Int32.Parse(split[0]) - 2; // x
                    int rightCorner = Int32.Parse(split[1]) - 2; // y

                    if (vrtx + 1 < world.drawnPath.Count)
                    {
                        var nextVertex = world.drawnPath[vrtx + 1];

                        // draw the edge to the next vertex
                        string[] edgeSplit = nextVertex.name.Split(';');
                        g.DrawLine(new Pen(Color.Black, 3),
                            new Point(Int32.Parse(split[0]), Int32.Parse(split[1])),
                            new Point(Int32.Parse(edgeSplit[0]), Int32.Parse(edgeSplit[1]))
                        );
                    }

                    g.DrawEllipse(new Pen(Color.Red), new Rectangle(leftCorner, rightCorner, 4, 4));
                }
            }

            /*
             * Draw the visited vertices to check if the 
             * pathfinding algorithms are efficient.
             */
            if (world.WorldGraph.VisitedVertices.Count > 0 && ShowVisited)
            {
                for (int vrtx = 0; vrtx < world.WorldGraph.VisitedVertices.Count; vrtx++)
                {
                    var vertex = world.WorldGraph.VisitedVertices[vrtx];
                    var edges = vertex.adj;
                    string[] split = vertex.name.Split(';');

                    int leftCorner = Int32.Parse(split[0]) - 2; // x
                    int rightCorner = Int32.Parse(split[1]) - 2; // y

                    if (vrtx + 1 < world.WorldGraph.VisitedVertices.Count)
                    {
                        var nextVertex = world.WorldGraph.VisitedVertices[vrtx + 1];

                        // draw the edge to the next vertex
                        string[] edgeSplit = nextVertex.name.Split(';');
                        g.DrawLine(new Pen(Color.Gray),
                            new Point(Int32.Parse(split[0]), Int32.Parse(split[1])),
                            new Point(Int32.Parse(edgeSplit[0]), Int32.Parse(edgeSplit[1]))
                        );
                    }



                    g.DrawEllipse(new Pen(Color.CadetBlue), new Rectangle(leftCorner, rightCorner, 4, 4));
                }
            }

            if (ShowTarget)
            {
                // draw the current target for the path following steering behaviour
                int leftC = (int)world.CurrentTargetPathFollowing.X - 12;
                int rightC = (int)world.CurrentTargetPathFollowing.Y - 12;
                g.FillEllipse(new SolidBrush(Color.Black), new Rectangle(leftC, rightC, 25, 25));
            }

        }

        /// <summary>
        /// Handle the cursor click within the panel, based on the current cursor mode.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dbPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (world.Mode == CursorMode.SEEK)
            {
                // right click = pathfinding
                if (e.Button == MouseButtons.Right)
                {
                    world.CursorTarget.UpdatePosition(e.X, e.Y);

                    Vertex start = EntityHelper.GetClosestVertex(new Point((int)world.Fishy.Pos.X, (int)world.Fishy.Pos.Y), world.WorldGraph.vertexMap);

                    Vertex target = EntityHelper.GetClosestVertex(new Point(e.X, e.Y), world.WorldGraph.vertexMap);

                    if (PathFindingAlgorithm == Pathfinding.AStar)
                        world.WorldGraph.AStarSearch(start.name, target.name);
                    else
                        world.WorldGraph.Dijkstra(start.name);

                    var vectorPath = world.WorldGraph.GetVectorPath(target.name);
                    SBPath sbPath = new SBPath(20, vectorPath);

                    world.SteeringPath = sbPath;

                    world.Target.Pos = new Vector2D(e.X, e.Y);
                    world.Fishy.Brain.RemoveAllSubgoals();
                    world.Fishy.Brain.AddGoalFollowPath(sbPath);

                    world.drawnPath = world.WorldGraph.GetPath(target.name);

                    world.WorldGraph.PrintPath(target.name);

                    world.Target.Pos = new Vector2D(e.X, e.Y);
                }
                else if (e.Button == MouseButtons.Left) // handle seek
                {
                    world.Target.Pos = new Vector2D(e.X, e.Y);
                    world.Fishy.Brain.RemoveAllSubgoals();
                    world.Fishy.Brain.AddGoalMovetoPosition(new Vector2D(e.X, e.Y));
                }

            }
            else if (world.Mode == CursorMode.DEBUG_INFO)
            {
                world.CursorTarget.UpdatePosition(e.X, e.Y);

                // check collision on cursor
                var collisions = new List<Tuple<bool, double, BaseGameEntity>>();
                var allEntities = new List<BaseGameEntity>(world.entities);

                // cast the obstacles to BaseGameEntity type & add them to the list of entities;
                world.obstacles.ForEach(x => allEntities.Add((BaseGameEntity)x));

                foreach (var entity in allEntities)
                {
                    var collisionTuple = GetCollision(entity);
                    // if there is collision (Item1 is a bool)
                    if (collisionTuple.Item1)
                    {
                        collisions.Add(collisionTuple);
                    }
                }

                if (collisions.Count > 0)
                {
                    /* Sort all collision entities by collision amount (decending) and get 
                     * the entity with the highest amount of collision.
                     */
                    var mostCollisionTuple = collisions.OrderByDescending(item => item.Item2).First();

                    if (mostCollisionTuple.Item3 is Obstacle obstacle)
                    {
                        if (obstacle.Type == ObstacleType.DEFAULT)
                        {
                            world.DebugEntity = mostCollisionTuple.Item3;
                        }
                    }
                    else
                    {
                        world.DebugEntity = mostCollisionTuple.Item3;
                    }
                }
                else
                {
                    world.DebugEntity = null;


                    AdjustGroupboxSize();

                }

                HandleDebugBox(visible: true);
            }
        }

        /// <summary>
        /// Calculate the collision of the cursor with a moving entity.
        /// </summary>
        /// <param name="me"></param>
        /// <returns>Tuple consisting of: bool, double and the moving entity.</returns>
        private Tuple<bool, double, BaseGameEntity> GetCollision(BaseGameEntity entity)
        {
            Tuple<bool, double, BaseGameEntity> collObj;

            // calculate the distance between the cursor and entity
            double calculation = EntityHelper.Distance(world.CursorTarget.Pos, entity.Pos);

            // check if there is collision & init tuples
            if (calculation <= world.CursorTarget.Radius + entity.Scale)
                collObj = new Tuple<bool, double, BaseGameEntity>(true, calculation, entity);
            else
                collObj = new Tuple<bool, double, BaseGameEntity>(false, 0, null);

            return collObj;
        }

        /// <summary>
        /// Generic method to toggle booleans within the menu strip.
        /// </summary>
        /// <param name="boolean"></param>
        /// <param name="label"></param>
        private void HandleBooleanInMenuStrip(bool boolean, ToolStripStatusLabel label = null)
        {
            if (boolean)
            {
                boolean = false;
                if (label != null)
                    label.Visible = false;
            }
            else
            {
                boolean = true;
                if (label != null)
                    label.Visible = true;
            }
        }

        /// <summary>
        /// Pause/Resume the game state.
        /// </summary>
        private void HandleGamePause()
        {
            if (world.Settings.Get("GamePaused"))
            {
                timer.Start();
                world.Settings.Set("GamePaused", false);
                this.Text = "Algorithms & AI final assignment s1091463";
            }
            else
            {
                timer.Stop();
                world.Settings.Set("GamePaused", true);
                this.Text = "Algorithms & AI final assignment s1091463 *PAUSED*";
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.B:
                    world.Settings.Set("ToggleObstacleBoundingBox", !world.Settings.Get("ToggleObstacleBoundingBox"));
                    break;
                case Keys.G:
                    world.Settings.Set("GraphEnabled", !world.Settings.Get("GraphEnabled"));
                    toolStripLblShowGraph.Text = "Graph: ";
                    toolStripLblShowGraph.Text += world.Settings.Get("GraphEnabled") ? "on" : "off";
                    break;

                case Keys.P:
                    HandleGamePause();
                    break;

                case Keys.S:
                    ShowSpatialGrid = !ShowSpatialGrid;
                    toolStripSpatialGridStatus.Text = "Spatial grid: ";
                    toolStripSpatialGridStatus.Text += ShowSpatialGrid ? "on" : "off";
                    break;

                case Keys.T:
                    ShowGoal = !ShowGoal;
                    break;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Helper method which toggles the visibility of the debug labels.
        /// </summary>
        /// <param name="enabled"></param>
        private void ToggleEntityDebugLabels(bool enabled)
        {
            if (enabled)
            {
                lblDebugPosition.Visible = true;
                lblDebugType.Visible = true;

                // if we're debugging a moving entity
                if (world.DebugEntity is MovingEntity)
                {
                    lblDebugBehaviour.Visible = true;
                    lblDebugVelocity.Visible = true;
                    lblDebugAngle.Visible = true;

                    lblDebugAdjacentEntities.Visible = ShowSpatialGrid;
                }
                else if (world.DebugEntity is Obstacle)
                {
                    lblDebugBehaviour.Visible = true;
                    lblDebugVelocity.Visible = false;
                    lblDebugAngle.Visible = false;
                    lblDebugAdjacentEntities.Visible = false;
                }
            }
            else
            {
                lblDebugPosition.Visible = false;
                lblDebugType.Visible = false;
                lblDebugBehaviour.Visible = false;
                lblDebugVelocity.Visible = false;
                lblDebugAngle.Visible = false;
                lblDebugAdjacentEntities.Visible = false;
            }
        }


        private void AdjustGroupboxSize()
        {
            var entity = world.DebugEntity;
            if (entity is Vehicle)
            {
                if (ShowSpatialGrid)
                    grpBoxDebug.Height = 95;
                else
                    grpBoxDebug.Height = 88;
            }
            else
            {
                grpBoxDebug.Height = 60;
            }
        }

        /// <summary>
        /// Handle the content of the debug labels.
        /// </summary>
        private void HandleEntityDebugLabels()
        {
            AdjustGroupboxSize();
            HandleDebugBox(true);
            HandleNewVehicle();

            var entity = world.DebugEntity;
            lblDebugPosition.Text = "Position: " + entity.Pos;
            lblDebugType.Text = string.Format("Type: {0}", entity.GetType().Name);

            if (entity is Vehicle)
            {
                Vehicle vehicle = (Vehicle)entity;

                string behaviour = Enum.GetName(typeof(Behaviour), vehicle.Behaviour);
                lblDebugBehaviour.Text = "Behaviour: " + System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(behaviour.ToLower());

                lblDebugVelocity.Text = string.Format("Velocity: {0}, (Length: {1})", vehicle.Velocity, Math.Truncate(vehicle.Velocity.Length() * 100) / 100);
                lblDebugAngle.Text = "Angle: " + vehicle.Angle + string.Format(" ({0})", vehicle.Direction.ToString());

                lblDebugAdjacentEntities.Text = string.Format("Adjacent entities: {0}", world.WorldGrid.GetAllAdjacentEntities(vehicle).Count);
            }
            else if (entity is Obstacle o)
            {
                lblDebugBehaviour.Text = string.Format("Obstacle type: {0}", o.Type.ToString());
            }
            // refresh to reflect new changes
            grpBoxDebug.Refresh();
        }

        /// <summary>
        /// Toggle the visibility of the debug groupbox.
        /// </summary>
        /// <param name="visible"></param>
        private void HandleDebugBox(bool visible)
        {
            if (visible)
                grpBoxDebug.Visible = true;
            else
                grpBoxDebug.Visible = false;

            if (world.DebugEntity != null)
            {
                lblSelectEntity.Visible = false;
                ToggleEntityDebugLabels(enabled: true);
            }
            else
            {
                lblSelectEntity.Visible = true;
                ToggleEntityDebugLabels(enabled: false);
            }
        }

        /// <summary>
        /// Handle the text of the 'Algorithm' label.
        /// </summary>
        private void HandleAlgorithmUpdate()
        {
            toolStripStatusPathfinding.Text = "Algorithm: ";
            switch (PathFindingAlgorithm)
            {
                case Pathfinding.AStar:
                    toolStripStatusHeuristic.Visible = true;
                    toolStripStatusPathfinding.Text += "A*";
                    break;
                case Pathfinding.DIJKSTRA:
                    toolStripStatusHeuristic.Visible = false;
                    toolStripStatusPathfinding.Text += "Dijkstra";
                    break;
            }
        }

        /// <summary>
        /// Handle the text of the 'Heuristic' label.
        /// </summary>
        private void HandleHeuristicUpdate()
        {
            toolStripStatusHeuristic.Text = "Heuristic: ";
            if (Heuristic is Euclidean)
            {
                toolStripStatusHeuristic.Text += "Euclidean";
                world.WorldGraph.SetHeuristic(new Euclidean());
            }
            else if (Heuristic is Manhattan)
            {
                toolStripStatusHeuristic.Text += "Manhattan";
                world.WorldGraph.SetHeuristic(new Manhattan());
            }
        }

        /// <summary>
        /// Change the cursor mode within the application.
        /// </summary>
        private void HandleCursorUpdate()
        {
            toolStripLblCursorMode.Text = "Mode: ";
            switch (world.Mode)
            {
                case CursorMode.SEEK:
                    world.DebugEntity = null;
                    ShowTarget = false;

                    world.drawnPath.Clear();
                    world.WorldGraph.VisitedVertices.Clear();
                    world.SteeringPath = null;
                    toolStripLblCursorMode.Text += "seek";

                    HandleDebugBox(visible: false);
                    dbPanel1.Cursor = System.Windows.Forms.Cursors.Arrow;
                    break;
                case CursorMode.DEBUG_INFO:
                    toolStripLblCursorMode.Text += "debug";
                    HandleDebugBox(visible: true);

                    dbPanel1.Cursor = System.Windows.Forms.Cursors.Cross;
                    break;
            }
        }

        private void seekToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            world.Mode = CursorMode.SEEK;
            if (debugToolStripMenuItem.Checked)
                debugToolStripMenuItem.Checked = false;
            seekToolStripMenuItem1.Checked = true;
            HandleCursorUpdate();
        }

        private void debugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            world.Mode = CursorMode.DEBUG_INFO;
            if (seekToolStripMenuItem1.Checked)
                seekToolStripMenuItem1.Checked = false;
            debugToolStripMenuItem.Checked = true;
            HandleCursorUpdate();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.Width > world.Width || this.Height > world.Height)
                world.Settings.Set("ForceRedraw", true);

            world.Width = this.Width;
            world.Height = this.Height;
            grpBoxDebug.Location = new Point(this.Width - 230, 27);
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            if (world.Settings.Get("ForceRedraw"))
            {
                world.GenerateGraph();
                world.WorldGrid.ResetGrid();
                world.Settings.Set("ForceRedraw", false);
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            dbPanel1.Invoke(new DrawGraphInPanel(DrawGraph), dbPanel1.CreateGraphics());
            world.Settings.Set("GraphDrawn", true);
        }

        private void cursorModeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void aToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PathFindingAlgorithm = Pathfinding.AStar;
            if (dijkstraToolStripMenuItem.Checked)
                dijkstraToolStripMenuItem.Checked = false;
            aToolStripMenuItem.Checked = true;
            HandleAlgorithmUpdate();
        }

        private void dijkstraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PathFindingAlgorithm = Pathfinding.DIJKSTRA;
            if (aToolStripMenuItem.Checked)
                aToolStripMenuItem.Checked = false;
            dijkstraToolStripMenuItem.Checked = true;
            HandleAlgorithmUpdate();
        }

        private void euclideanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Heuristic = new Euclidean();
            if (manhattanToolStripMenuItem.Checked)
                manhattanToolStripMenuItem.Checked = false;
            euclideanToolStripMenuItem.Checked = true;
            HandleHeuristicUpdate();
        }

        private void manhattanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Heuristic = new Manhattan();
            if (euclideanToolStripMenuItem.Checked)
                euclideanToolStripMenuItem.Checked = false;
            manhattanToolStripMenuItem.Checked = true;
            HandleHeuristicUpdate();
        }

        private void showPathToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            showPathToolStripMenuItem1.Checked = !showPathToolStripMenuItem1.Checked;
            world.Settings.Set("ShowGeneratedPath", showPathToolStripMenuItem1.Checked);
        }

        private void showVisitedToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            showVisitedToolStripMenuItem1.Checked = !showVisitedToolStripMenuItem1.Checked;
        }

        private void settingsToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            HandleGamePause();
        }


        private void settingsToolStripMenuItem_DropDownClosed(object sender, EventArgs e)
        {
            HandleGamePause();
        }

        private void showTargetToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            showTargetToolStripMenuItem.Checked = !showTargetToolStripMenuItem.Checked;
            world.Settings.Set("ShowTarget", showTargetToolStripMenuItem.Checked);
        }

        private void npcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            npcToolStripMenuItem.Checked = !npcToolStripMenuItem.Checked;
            world.Settings.Set("EnforceNonPenetrationConstraint", npcToolStripMenuItem.Checked);
        }

        private void showGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showSpatialGridToolStripMenuItem.Checked = !showSpatialGridToolStripMenuItem.Checked;
            world.Settings.Set("ShowSpatialGrid", showSpatialGridToolStripMenuItem.Checked);
        }

        /// <summary>
        /// Spawn a new agent with a given behaviour within the Game World.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSpawnAgent_Click(object sender, EventArgs e)
        {
            // don't allow default behaviour (null)
            if (comboBoxBehaviour.SelectedValue.Equals(Behaviour.DEFAULT))
            {
                return;
            }
            Enum.TryParse<Behaviour>(comboBoxBehaviour.SelectedValue.ToString(), out Behaviour behaviour);
            world.PopulateDuringRuntime(behaviour);
            world.Settings.Set("HandleNewVehicle", true);

        }

        private void showAdjacentBucketsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            showAdjacentBucketsToolStripMenuItem.Checked = !showAdjacentBucketsToolStripMenuItem.Checked;
            world.Settings.Set("ShowAdjacentBuckets", showAdjacentBucketsToolStripMenuItem.Checked);
        }

        private void showGoalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showGoalToolStripMenuItem.Checked = !showGoalToolStripMenuItem.Checked;
        }

        private void autoArbitrationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            autoArbitrationToolStripMenuItem.Checked = !autoArbitrationToolStripMenuItem.Checked;
            world.Settings.Set("AutoArbitrate", autoArbitrationToolStripMenuItem.Checked);
        }
    }
}
