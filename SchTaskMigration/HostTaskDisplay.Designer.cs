
namespace SchTaskMigration
{
    partial class HostTaskDisplay
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.ckbOnlyEnabled = new System.Windows.Forms.CheckBox();
            this.cbxHosts = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlInformation = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lbxTasks = new System.Windows.Forms.ListBox();
            this.panel1.SuspendLayout();
            this.pnlInformation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ckbOnlyEnabled);
            this.panel1.Controls.Add(this.cbxHosts);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(571, 29);
            this.panel1.TabIndex = 0;
            // 
            // ckbOnlyEnabled
            // 
            this.ckbOnlyEnabled.AutoSize = true;
            this.ckbOnlyEnabled.Location = new System.Drawing.Point(289, 5);
            this.ckbOnlyEnabled.Name = "ckbOnlyEnabled";
            this.ckbOnlyEnabled.Size = new System.Drawing.Size(151, 17);
            this.ckbOnlyEnabled.TabIndex = 2;
            this.ckbOnlyEnabled.Text = "Show Only Enabled Tasks";
            this.ckbOnlyEnabled.UseVisualStyleBackColor = true;
            this.ckbOnlyEnabled.CheckedChanged += new System.EventHandler(this.ckbOnlyEnabled_CheckedChanged);
            // 
            // cbxHosts
            // 
            this.cbxHosts.FormattingEnabled = true;
            this.cbxHosts.Location = new System.Drawing.Point(45, 3);
            this.cbxHosts.Name = "cbxHosts";
            this.cbxHosts.Size = new System.Drawing.Size(238, 21);
            this.cbxHosts.TabIndex = 1;
            this.cbxHosts.SelectionChangeCommitted += new System.EventHandler(this.cbxHosts_SelectionChangeCommitted);
            this.cbxHosts.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.cbxHosts_PreviewKeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Host: ";
            // 
            // pnlInformation
            // 
            this.pnlInformation.Controls.Add(this.splitContainer1);
            this.pnlInformation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInformation.Location = new System.Drawing.Point(0, 29);
            this.pnlInformation.Name = "pnlInformation";
            this.pnlInformation.Size = new System.Drawing.Size(571, 338);
            this.pnlInformation.TabIndex = 1;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lbxTasks);
            this.splitContainer1.Size = new System.Drawing.Size(571, 338);
            this.splitContainer1.SplitterDistance = 190;
            this.splitContainer1.TabIndex = 0;
            // 
            // lbxTasks
            // 
            this.lbxTasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbxTasks.FormattingEnabled = true;
            this.lbxTasks.Location = new System.Drawing.Point(0, 0);
            this.lbxTasks.Name = "lbxTasks";
            this.lbxTasks.Size = new System.Drawing.Size(190, 338);
            this.lbxTasks.Sorted = true;
            this.lbxTasks.TabIndex = 0;
            this.lbxTasks.SelectedIndexChanged += new System.EventHandler(this.lbxTasks_SelectedIndexChanged);
            // 
            // HostTaskDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.pnlInformation);
            this.Controls.Add(this.panel1);
            this.Name = "HostTaskDisplay";
            this.Size = new System.Drawing.Size(571, 367);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlInformation.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cbxHosts;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlInformation;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox lbxTasks;
        private System.Windows.Forms.CheckBox ckbOnlyEnabled;
    }
}
