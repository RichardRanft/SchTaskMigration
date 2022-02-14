
namespace SchTaskMigration
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadFromPathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.writeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.operationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disableAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enableAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fbdBrowse = new System.Windows.Forms.FolderBrowserDialog();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lbxTaskFiles = new System.Windows.Forms.ListBox();
            this.pnlDataGrids = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpgTaskSched = new System.Windows.Forms.TabPage();
            this.spcTaskHostDisplay = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCopyToB = new System.Windows.Forms.Button();
            this.btnCopyToA = new System.Windows.Forms.Button();
            this.tpgXML = new System.Windows.Forms.TabPage();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpgTaskSched.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spcTaskHostDisplay)).BeginInit();
            this.spcTaskHostDisplay.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tpgXML.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.operationsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1073, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFolderToolStripMenuItem,
            this.loadFromPathToolStripMenuItem,
            this.writeToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openFolderToolStripMenuItem
            // 
            this.openFolderToolStripMenuItem.Name = "openFolderToolStripMenuItem";
            this.openFolderToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.openFolderToolStripMenuItem.Text = "&Open Folder";
            this.openFolderToolStripMenuItem.Click += new System.EventHandler(this.openFolderToolStripMenuItem_Click);
            // 
            // loadFromPathToolStripMenuItem
            // 
            this.loadFromPathToolStripMenuItem.Name = "loadFromPathToolStripMenuItem";
            this.loadFromPathToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.loadFromPathToolStripMenuItem.Text = "Load From Path";
            this.loadFromPathToolStripMenuItem.Click += new System.EventHandler(this.loadFromPathToolStripMenuItem_Click);
            // 
            // writeToolStripMenuItem
            // 
            this.writeToolStripMenuItem.Name = "writeToolStripMenuItem";
            this.writeToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.writeToolStripMenuItem.Text = "Write";
            this.writeToolStripMenuItem.Click += new System.EventHandler(this.writeToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(155, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // operationsToolStripMenuItem
            // 
            this.operationsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.disableAllToolStripMenuItem,
            this.enableAllToolStripMenuItem});
            this.operationsToolStripMenuItem.Name = "operationsToolStripMenuItem";
            this.operationsToolStripMenuItem.Size = new System.Drawing.Size(77, 20);
            this.operationsToolStripMenuItem.Text = "Operations";
            // 
            // disableAllToolStripMenuItem
            // 
            this.disableAllToolStripMenuItem.Name = "disableAllToolStripMenuItem";
            this.disableAllToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.disableAllToolStripMenuItem.Text = "Disable All";
            this.disableAllToolStripMenuItem.Click += new System.EventHandler(this.disableAllToolStripMenuItem_Click);
            // 
            // enableAllToolStripMenuItem
            // 
            this.enableAllToolStripMenuItem.Name = "enableAllToolStripMenuItem";
            this.enableAllToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.enableAllToolStripMenuItem.Text = "Enable All";
            this.enableAllToolStripMenuItem.Click += new System.EventHandler(this.enableAllToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lbxTaskFiles);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pnlDataGrids);
            this.splitContainer1.Size = new System.Drawing.Size(1059, 463);
            this.splitContainer1.SplitterDistance = 231;
            this.splitContainer1.TabIndex = 1;
            // 
            // lbxTaskFiles
            // 
            this.lbxTaskFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbxTaskFiles.FormattingEnabled = true;
            this.lbxTaskFiles.Location = new System.Drawing.Point(0, 0);
            this.lbxTaskFiles.Name = "lbxTaskFiles";
            this.lbxTaskFiles.Size = new System.Drawing.Size(231, 463);
            this.lbxTaskFiles.TabIndex = 0;
            this.lbxTaskFiles.SelectedIndexChanged += new System.EventHandler(this.lbxTaskFiles_SelectedIndexChanged);
            // 
            // pnlDataGrids
            // 
            this.pnlDataGrids.AutoScroll = true;
            this.pnlDataGrids.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDataGrids.Location = new System.Drawing.Point(0, 0);
            this.pnlDataGrids.Name = "pnlDataGrids";
            this.pnlDataGrids.Size = new System.Drawing.Size(824, 463);
            this.pnlDataGrids.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpgTaskSched);
            this.tabControl1.Controls.Add(this.tpgXML);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 24);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1073, 495);
            this.tabControl1.TabIndex = 0;
            // 
            // tpgTaskSched
            // 
            this.tpgTaskSched.Controls.Add(this.spcTaskHostDisplay);
            this.tpgTaskSched.Controls.Add(this.panel1);
            this.tpgTaskSched.Location = new System.Drawing.Point(4, 22);
            this.tpgTaskSched.Name = "tpgTaskSched";
            this.tpgTaskSched.Padding = new System.Windows.Forms.Padding(3);
            this.tpgTaskSched.Size = new System.Drawing.Size(1065, 469);
            this.tpgTaskSched.TabIndex = 1;
            this.tpgTaskSched.Text = "Task Scheduler Interface";
            this.tpgTaskSched.UseVisualStyleBackColor = true;
            // 
            // spcTaskHostDisplay
            // 
            this.spcTaskHostDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spcTaskHostDisplay.Location = new System.Drawing.Point(3, 32);
            this.spcTaskHostDisplay.Name = "spcTaskHostDisplay";
            this.spcTaskHostDisplay.Size = new System.Drawing.Size(1059, 434);
            this.spcTaskHostDisplay.SplitterDistance = 529;
            this.spcTaskHostDisplay.TabIndex = 0;
            this.spcTaskHostDisplay.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.spcTaskHostDisplay_SplitterMoved);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCopyToB);
            this.panel1.Controls.Add(this.btnCopyToA);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1059, 29);
            this.panel1.TabIndex = 1;
            this.panel1.Resize += new System.EventHandler(this.panel1_Resize);
            // 
            // btnCopyToB
            // 
            this.btnCopyToB.Location = new System.Drawing.Point(541, 3);
            this.btnCopyToB.Name = "btnCopyToB";
            this.btnCopyToB.Size = new System.Drawing.Size(75, 23);
            this.btnCopyToB.TabIndex = 1;
            this.btnCopyToB.Text = "Copy >>";
            this.btnCopyToB.UseVisualStyleBackColor = true;
            this.btnCopyToB.Click += new System.EventHandler(this.btnCopyToB_Click);
            // 
            // btnCopyToA
            // 
            this.btnCopyToA.Location = new System.Drawing.Point(442, 3);
            this.btnCopyToA.Name = "btnCopyToA";
            this.btnCopyToA.Size = new System.Drawing.Size(75, 23);
            this.btnCopyToA.TabIndex = 0;
            this.btnCopyToA.Text = "<< Copy";
            this.btnCopyToA.UseVisualStyleBackColor = true;
            this.btnCopyToA.Click += new System.EventHandler(this.btnCopyToA_Click);
            // 
            // tpgXML
            // 
            this.tpgXML.Controls.Add(this.splitContainer1);
            this.tpgXML.Location = new System.Drawing.Point(4, 22);
            this.tpgXML.Name = "tpgXML";
            this.tpgXML.Padding = new System.Windows.Forms.Padding(3);
            this.tpgXML.Size = new System.Drawing.Size(1065, 469);
            this.tpgXML.TabIndex = 0;
            this.tpgXML.Text = "Task XML Tool";
            this.tpgXML.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1073, 519);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(1089, 558);
            this.Name = "Form1";
            this.Text = "Scheduled Task XML Manager";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tpgTaskSched.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spcTaskHostDisplay)).EndInit();
            this.spcTaskHostDisplay.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tpgXML.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog fbdBrowse;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox lbxTaskFiles;
        private System.Windows.Forms.ToolStripMenuItem loadFromPathToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem operationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disableAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enableAllToolStripMenuItem;
        private System.Windows.Forms.Panel pnlDataGrids;
        private System.Windows.Forms.ToolStripMenuItem writeToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpgXML;
        private System.Windows.Forms.TabPage tpgTaskSched;
        private System.Windows.Forms.SplitContainer spcTaskHostDisplay;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCopyToB;
        private System.Windows.Forms.Button btnCopyToA;
    }
}

