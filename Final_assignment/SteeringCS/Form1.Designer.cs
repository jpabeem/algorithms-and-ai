namespace SteeringCS
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cursorModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.seekToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.debugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pathfindingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dijkstraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showPathToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.showVisitedToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.showTargetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.heuristicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.euclideanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manhattanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.flockingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.npcToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spatialPartitioningToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showSpatialGridToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showAdjacentBucketsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.goalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showGoalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoArbitrationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblDisplayFPS = new System.Windows.Forms.Label();
            this.btnSpawnAgent = new System.Windows.Forms.Button();
            this.comboBoxBehaviour = new System.Windows.Forms.ComboBox();
            this.dbPanel1 = new SteeringCS.DBPanel();
            this.lblBurriedFish = new System.Windows.Forms.Label();
            this.grpBoxDebug = new System.Windows.Forms.GroupBox();
            this.lblDebugAdjacentEntities = new System.Windows.Forms.Label();
            this.lblDebugAngle = new System.Windows.Forms.Label();
            this.lblDebugType = new System.Windows.Forms.Label();
            this.lblDebugVelocity = new System.Windows.Forms.Label();
            this.lblSelectEntity = new System.Windows.Forms.Label();
            this.lblDebugBehaviour = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblDebugPosition = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripLblShowGraph = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripSpatialGridStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusPathfinding = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusHeuristic = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripLblCursorMode = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblHunger = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.dbPanel1.SuspendLayout();
            this.grpBoxDebug.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1184, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cursorModeToolStripMenuItem,
            this.pathfindingToolStripMenuItem,
            this.heuristicToolStripMenuItem,
            this.flockingToolStripMenuItem,
            this.spatialPartitioningToolStripMenuItem,
            this.goalToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.DropDownClosed += new System.EventHandler(this.settingsToolStripMenuItem_DropDownClosed);
            this.settingsToolStripMenuItem.DropDownOpened += new System.EventHandler(this.settingsToolStripMenuItem_DropDownOpened);
            // 
            // cursorModeToolStripMenuItem
            // 
            this.cursorModeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.seekToolStripMenuItem1,
            this.debugToolStripMenuItem});
            this.cursorModeToolStripMenuItem.Name = "cursorModeToolStripMenuItem";
            this.cursorModeToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.cursorModeToolStripMenuItem.Text = "Cursor mode";
            this.cursorModeToolStripMenuItem.Click += new System.EventHandler(this.cursorModeToolStripMenuItem_Click);
            // 
            // seekToolStripMenuItem1
            // 
            this.seekToolStripMenuItem1.Name = "seekToolStripMenuItem1";
            this.seekToolStripMenuItem1.Size = new System.Drawing.Size(109, 22);
            this.seekToolStripMenuItem1.Text = "Seek";
            this.seekToolStripMenuItem1.Click += new System.EventHandler(this.seekToolStripMenuItem1_Click);
            // 
            // debugToolStripMenuItem
            // 
            this.debugToolStripMenuItem.Name = "debugToolStripMenuItem";
            this.debugToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.debugToolStripMenuItem.Text = "Debug";
            this.debugToolStripMenuItem.Click += new System.EventHandler(this.debugToolStripMenuItem_Click);
            // 
            // pathfindingToolStripMenuItem
            // 
            this.pathfindingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aToolStripMenuItem,
            this.dijkstraToolStripMenuItem,
            this.showPathToolStripMenuItem1,
            this.showVisitedToolStripMenuItem1,
            this.showTargetToolStripMenuItem});
            this.pathfindingToolStripMenuItem.Name = "pathfindingToolStripMenuItem";
            this.pathfindingToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.pathfindingToolStripMenuItem.Text = "Pathfinding";
            // 
            // aToolStripMenuItem
            // 
            this.aToolStripMenuItem.Name = "aToolStripMenuItem";
            this.aToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.aToolStripMenuItem.Text = "A*";
            this.aToolStripMenuItem.Click += new System.EventHandler(this.aToolStripMenuItem_Click);
            // 
            // dijkstraToolStripMenuItem
            // 
            this.dijkstraToolStripMenuItem.Name = "dijkstraToolStripMenuItem";
            this.dijkstraToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.dijkstraToolStripMenuItem.Text = "Dijkstra";
            this.dijkstraToolStripMenuItem.Click += new System.EventHandler(this.dijkstraToolStripMenuItem_Click);
            // 
            // showPathToolStripMenuItem1
            // 
            this.showPathToolStripMenuItem1.Name = "showPathToolStripMenuItem1";
            this.showPathToolStripMenuItem1.Size = new System.Drawing.Size(140, 22);
            this.showPathToolStripMenuItem1.Text = "Show path";
            this.showPathToolStripMenuItem1.Click += new System.EventHandler(this.showPathToolStripMenuItem1_Click);
            // 
            // showVisitedToolStripMenuItem1
            // 
            this.showVisitedToolStripMenuItem1.Name = "showVisitedToolStripMenuItem1";
            this.showVisitedToolStripMenuItem1.Size = new System.Drawing.Size(140, 22);
            this.showVisitedToolStripMenuItem1.Text = "Show visited";
            this.showVisitedToolStripMenuItem1.Click += new System.EventHandler(this.showVisitedToolStripMenuItem1_Click);
            // 
            // showTargetToolStripMenuItem
            // 
            this.showTargetToolStripMenuItem.Name = "showTargetToolStripMenuItem";
            this.showTargetToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.showTargetToolStripMenuItem.Text = "Show target";
            this.showTargetToolStripMenuItem.Click += new System.EventHandler(this.showTargetToolStripMenuItem_Click_1);
            // 
            // heuristicToolStripMenuItem
            // 
            this.heuristicToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.euclideanToolStripMenuItem,
            this.manhattanToolStripMenuItem});
            this.heuristicToolStripMenuItem.Name = "heuristicToolStripMenuItem";
            this.heuristicToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.heuristicToolStripMenuItem.Text = "Heuristic";
            // 
            // euclideanToolStripMenuItem
            // 
            this.euclideanToolStripMenuItem.Name = "euclideanToolStripMenuItem";
            this.euclideanToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.euclideanToolStripMenuItem.Text = "Euclidean";
            this.euclideanToolStripMenuItem.Click += new System.EventHandler(this.euclideanToolStripMenuItem_Click);
            // 
            // manhattanToolStripMenuItem
            // 
            this.manhattanToolStripMenuItem.Name = "manhattanToolStripMenuItem";
            this.manhattanToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.manhattanToolStripMenuItem.Text = "Manhattan";
            this.manhattanToolStripMenuItem.Click += new System.EventHandler(this.manhattanToolStripMenuItem_Click);
            // 
            // flockingToolStripMenuItem
            // 
            this.flockingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.npcToolStripMenuItem});
            this.flockingToolStripMenuItem.Name = "flockingToolStripMenuItem";
            this.flockingToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.flockingToolStripMenuItem.Text = "Flocking";
            // 
            // npcToolStripMenuItem
            // 
            this.npcToolStripMenuItem.Name = "npcToolStripMenuItem";
            this.npcToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.npcToolStripMenuItem.Text = "Non Penetration Constraint";
            this.npcToolStripMenuItem.Click += new System.EventHandler(this.npcToolStripMenuItem_Click);
            // 
            // spatialPartitioningToolStripMenuItem
            // 
            this.spatialPartitioningToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showSpatialGridToolStripMenuItem,
            this.showAdjacentBucketsToolStripMenuItem});
            this.spatialPartitioningToolStripMenuItem.Name = "spatialPartitioningToolStripMenuItem";
            this.spatialPartitioningToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.spatialPartitioningToolStripMenuItem.Text = "Spatial partitioning";
            // 
            // showSpatialGridToolStripMenuItem
            // 
            this.showSpatialGridToolStripMenuItem.Name = "showSpatialGridToolStripMenuItem";
            this.showSpatialGridToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.showSpatialGridToolStripMenuItem.Text = "Show grid";
            this.showSpatialGridToolStripMenuItem.Click += new System.EventHandler(this.showGridToolStripMenuItem_Click);
            // 
            // showAdjacentBucketsToolStripMenuItem
            // 
            this.showAdjacentBucketsToolStripMenuItem.Name = "showAdjacentBucketsToolStripMenuItem";
            this.showAdjacentBucketsToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.showAdjacentBucketsToolStripMenuItem.Text = "Show adjacent buckets";
            this.showAdjacentBucketsToolStripMenuItem.Click += new System.EventHandler(this.showAdjacentBucketsToolStripMenuItem_Click_1);
            // 
            // goalToolStripMenuItem
            // 
            this.goalToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showGoalToolStripMenuItem,
            this.autoArbitrationToolStripMenuItem});
            this.goalToolStripMenuItem.Name = "goalToolStripMenuItem";
            this.goalToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.goalToolStripMenuItem.Text = "Goal driven behaviour";
            // 
            // showGoalToolStripMenuItem
            // 
            this.showGoalToolStripMenuItem.Name = "showGoalToolStripMenuItem";
            this.showGoalToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.showGoalToolStripMenuItem.Text = "Show goal";
            this.showGoalToolStripMenuItem.Click += new System.EventHandler(this.showGoalToolStripMenuItem_Click);
            // 
            // autoArbitrationToolStripMenuItem
            // 
            this.autoArbitrationToolStripMenuItem.Name = "autoArbitrationToolStripMenuItem";
            this.autoArbitrationToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.autoArbitrationToolStripMenuItem.Text = "Auto arbitration";
            this.autoArbitrationToolStripMenuItem.Click += new System.EventHandler(this.autoArbitrationToolStripMenuItem_Click);
            // 
            // lblDisplayFPS
            // 
            this.lblDisplayFPS.AutoSize = true;
            this.lblDisplayFPS.BackColor = System.Drawing.Color.White;
            this.lblDisplayFPS.Location = new System.Drawing.Point(1113, 8);
            this.lblDisplayFPS.Name = "lblDisplayFPS";
            this.lblDisplayFPS.Size = new System.Drawing.Size(59, 13);
            this.lblDisplayFPS.TabIndex = 3;
            this.lblDisplayFPS.Text = "FPS: <fps>";
            // 
            // btnSpawnAgent
            // 
            this.btnSpawnAgent.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSpawnAgent.Location = new System.Drawing.Point(879, 4);
            this.btnSpawnAgent.Name = "btnSpawnAgent";
            this.btnSpawnAgent.Size = new System.Drawing.Size(101, 21);
            this.btnSpawnAgent.TabIndex = 4;
            this.btnSpawnAgent.Text = "Spawn Agent";
            this.btnSpawnAgent.UseVisualStyleBackColor = true;
            this.btnSpawnAgent.Click += new System.EventHandler(this.btnSpawnAgent_Click);
            // 
            // comboBoxBehaviour
            // 
            this.comboBoxBehaviour.FormattingEnabled = true;
            this.comboBoxBehaviour.Location = new System.Drawing.Point(986, 5);
            this.comboBoxBehaviour.Name = "comboBoxBehaviour";
            this.comboBoxBehaviour.Size = new System.Drawing.Size(121, 21);
            this.comboBoxBehaviour.TabIndex = 5;
            // 
            // dbPanel1
            // 
            this.dbPanel1.BackColor = System.Drawing.Color.White;
            this.dbPanel1.Controls.Add(this.lblHunger);
            this.dbPanel1.Controls.Add(this.lblBurriedFish);
            this.dbPanel1.Controls.Add(this.grpBoxDebug);
            this.dbPanel1.Controls.Add(this.statusStrip1);
            this.dbPanel1.Cursor = System.Windows.Forms.Cursors.Cross;
            this.dbPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dbPanel1.Location = new System.Drawing.Point(0, 24);
            this.dbPanel1.Name = "dbPanel1";
            this.dbPanel1.Size = new System.Drawing.Size(1184, 737);
            this.dbPanel1.TabIndex = 0;
            this.dbPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.dbPanel1_Paint);
            this.dbPanel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dbPanel1_MouseClick);
            // 
            // lblBurriedFish
            // 
            this.lblBurriedFish.AutoSize = true;
            this.lblBurriedFish.BackColor = System.Drawing.Color.White;
            this.lblBurriedFish.Location = new System.Drawing.Point(12, 11);
            this.lblBurriedFish.Name = "lblBurriedFish";
            this.lblBurriedFish.Size = new System.Drawing.Size(68, 13);
            this.lblBurriedFish.TabIndex = 3;
            this.lblBurriedFish.Text = "Buried fish: 0";
            // 
            // grpBoxDebug
            // 
            this.grpBoxDebug.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(181)))), ((int)(((byte)(223)))), ((int)(((byte)(250)))));
            this.grpBoxDebug.Controls.Add(this.lblDebugAdjacentEntities);
            this.grpBoxDebug.Controls.Add(this.lblDebugAngle);
            this.grpBoxDebug.Controls.Add(this.lblDebugType);
            this.grpBoxDebug.Controls.Add(this.lblDebugVelocity);
            this.grpBoxDebug.Controls.Add(this.lblSelectEntity);
            this.grpBoxDebug.Controls.Add(this.lblDebugBehaviour);
            this.grpBoxDebug.Controls.Add(this.label2);
            this.grpBoxDebug.Controls.Add(this.lblDebugPosition);
            this.grpBoxDebug.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpBoxDebug.ForeColor = System.Drawing.SystemColors.Desktop;
            this.grpBoxDebug.Location = new System.Drawing.Point(972, 27);
            this.grpBoxDebug.Name = "grpBoxDebug";
            this.grpBoxDebug.Size = new System.Drawing.Size(200, 105);
            this.grpBoxDebug.TabIndex = 2;
            this.grpBoxDebug.TabStop = false;
            this.grpBoxDebug.Text = "Debug info:";
            // 
            // lblDebugAdjacentEntities
            // 
            this.lblDebugAdjacentEntities.AutoSize = true;
            this.lblDebugAdjacentEntities.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDebugAdjacentEntities.Location = new System.Drawing.Point(5, 80);
            this.lblDebugAdjacentEntities.Name = "lblDebugAdjacentEntities";
            this.lblDebugAdjacentEntities.Size = new System.Drawing.Size(130, 13);
            this.lblDebugAdjacentEntities.TabIndex = 7;
            this.lblDebugAdjacentEntities.Text = "Adjacent entities: <count>";
            // 
            // lblDebugAngle
            // 
            this.lblDebugAngle.AutoSize = true;
            this.lblDebugAngle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDebugAngle.Location = new System.Drawing.Point(6, 67);
            this.lblDebugAngle.Name = "lblDebugAngle";
            this.lblDebugAngle.Size = new System.Drawing.Size(78, 13);
            this.lblDebugAngle.TabIndex = 6;
            this.lblDebugAngle.Text = "Angle: <angle>";
            // 
            // lblDebugType
            // 
            this.lblDebugType.AutoSize = true;
            this.lblDebugType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDebugType.Location = new System.Drawing.Point(6, 29);
            this.lblDebugType.Name = "lblDebugType";
            this.lblDebugType.Size = new System.Drawing.Size(69, 13);
            this.lblDebugType.TabIndex = 5;
            this.lblDebugType.Text = "Type: <type>";
            // 
            // lblDebugVelocity
            // 
            this.lblDebugVelocity.AutoSize = true;
            this.lblDebugVelocity.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDebugVelocity.Location = new System.Drawing.Point(6, 54);
            this.lblDebugVelocity.Name = "lblDebugVelocity";
            this.lblDebugVelocity.Size = new System.Drawing.Size(98, 13);
            this.lblDebugVelocity.TabIndex = 4;
            this.lblDebugVelocity.Text = "Velocity: <velocity>";
            // 
            // lblSelectEntity
            // 
            this.lblSelectEntity.AutoSize = true;
            this.lblSelectEntity.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectEntity.Location = new System.Drawing.Point(20, 29);
            this.lblSelectEntity.Name = "lblSelectEntity";
            this.lblSelectEntity.Size = new System.Drawing.Size(160, 25);
            this.lblSelectEntity.TabIndex = 3;
            this.lblSelectEntity.Text = "Select an entity";
            // 
            // lblDebugBehaviour
            // 
            this.lblDebugBehaviour.AutoSize = true;
            this.lblDebugBehaviour.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDebugBehaviour.Location = new System.Drawing.Point(6, 42);
            this.lblDebugBehaviour.Name = "lblDebugBehaviour";
            this.lblDebugBehaviour.Size = new System.Drawing.Size(120, 13);
            this.lblDebugBehaviour.TabIndex = 2;
            this.lblDebugBehaviour.Text = "Behaviour: <behaviour>";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 16);
            this.label2.TabIndex = 1;
            // 
            // lblDebugPosition
            // 
            this.lblDebugPosition.AutoSize = true;
            this.lblDebugPosition.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDebugPosition.Location = new System.Drawing.Point(6, 16);
            this.lblDebugPosition.Name = "lblDebugPosition";
            this.lblDebugPosition.Size = new System.Drawing.Size(78, 13);
            this.lblDebugPosition.TabIndex = 0;
            this.lblDebugPosition.Text = "Position: ( x, y )";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLblShowGraph,
            this.toolStripSpatialGridStatus,
            this.toolStripStatusPathfinding,
            this.toolStripStatusHeuristic,
            this.toolStripLblCursorMode});
            this.statusStrip1.Location = new System.Drawing.Point(0, 715);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip1.Size = new System.Drawing.Size(1184, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripLblShowGraph
            // 
            this.toolStripLblShowGraph.Name = "toolStripLblShowGraph";
            this.toolStripLblShowGraph.Size = new System.Drawing.Size(92, 17);
            this.toolStripLblShowGraph.Text = "Graph: <status>";
            // 
            // toolStripSpatialGridStatus
            // 
            this.toolStripSpatialGridStatus.Name = "toolStripSpatialGridStatus";
            this.toolStripSpatialGridStatus.Size = new System.Drawing.Size(119, 17);
            this.toolStripSpatialGridStatus.Text = "Spatial grid: <status>";
            // 
            // toolStripStatusPathfinding
            // 
            this.toolStripStatusPathfinding.Name = "toolStripStatusPathfinding";
            this.toolStripStatusPathfinding.Size = new System.Drawing.Size(135, 17);
            this.toolStripStatusPathfinding.Text = "Algorithm: <algorithm>";
            // 
            // toolStripStatusHeuristic
            // 
            this.toolStripStatusHeuristic.Name = "toolStripStatusHeuristic";
            this.toolStripStatusHeuristic.Size = new System.Drawing.Size(121, 17);
            this.toolStripStatusHeuristic.Text = "Heuristic: <heuristic>";
            // 
            // toolStripLblCursorMode
            // 
            this.toolStripLblCursorMode.Name = "toolStripLblCursorMode";
            this.toolStripLblCursorMode.Size = new System.Drawing.Size(67, 17);
            this.toolStripLblCursorMode.Text = "Mode: seek";
            // 
            // lblHunger
            // 
            this.lblHunger.AutoSize = true;
            this.lblHunger.BackColor = System.Drawing.Color.White;
            this.lblHunger.Location = new System.Drawing.Point(12, 27);
            this.lblHunger.Name = "lblHunger";
            this.lblHunger.Size = new System.Drawing.Size(54, 13);
            this.lblHunger.TabIndex = 4;
            this.lblHunger.Text = "Hunger: 0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 761);
            this.Controls.Add(this.comboBoxBehaviour);
            this.Controls.Add(this.btnSpawnAgent);
            this.Controls.Add(this.lblDisplayFPS);
            this.Controls.Add(this.dbPanel1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Algorithms & AI final assignment s1091463";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResizeEnd += new System.EventHandler(this.Form1_ResizeEnd);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.dbPanel1.ResumeLayout(false);
            this.dbPanel1.PerformLayout();
            this.grpBoxDebug.ResumeLayout(false);
            this.grpBoxDebug.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DBPanel dbPanel1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripLblShowGraph;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cursorModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem seekToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem debugToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripLblCursorMode;
        private System.Windows.Forms.GroupBox grpBoxDebug;
        private System.Windows.Forms.Label lblDebugBehaviour;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblDebugPosition;
        private System.Windows.Forms.Label lblSelectEntity;
        private System.Windows.Forms.Label lblDebugVelocity;
        private System.Windows.Forms.Label lblDebugType;
        private System.Windows.Forms.Label lblDebugAngle;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.Windows.Forms.ToolStripMenuItem pathfindingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dijkstraToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusPathfinding;
        private System.Windows.Forms.ToolStripMenuItem heuristicToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem euclideanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manhattanToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusHeuristic;
        private System.Windows.Forms.ToolStripMenuItem showPathToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem showVisitedToolStripMenuItem1;
        private System.Windows.Forms.Label lblDisplayFPS;
        private System.Windows.Forms.ToolStripMenuItem showTargetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem flockingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem npcToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem spatialPartitioningToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showSpatialGridToolStripMenuItem;
        private System.Windows.Forms.Button btnSpawnAgent;
        private System.Windows.Forms.ComboBox comboBoxBehaviour;
        private System.Windows.Forms.ToolStripStatusLabel toolStripSpatialGridStatus;
        private System.Windows.Forms.ToolStripMenuItem showAdjacentBucketsToolStripMenuItem;
        private System.Windows.Forms.Label lblDebugAdjacentEntities;
        private System.Windows.Forms.ToolStripMenuItem goalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showGoalToolStripMenuItem;
        private System.Windows.Forms.Label lblBurriedFish;
        private System.Windows.Forms.ToolStripMenuItem autoArbitrationToolStripMenuItem;
        private System.Windows.Forms.Label lblHunger;
    }
}

