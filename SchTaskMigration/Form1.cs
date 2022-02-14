using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Win32.TaskScheduler;
using System.Windows.Forms;
using System.Xml;
using log4net;

namespace SchTaskMigration
{
    public partial class Form1 : Form
    {
        private static ILog m_log = LogManager.GetLogger(typeof(Form1));

        private DataSet m_data = new DataSet();
        private CTaskSettings m_settings = new CTaskSettings();
        private List<CTaskFile> m_xmlFiles = new List<CTaskFile>();
        private frmLoadPath m_loadPath = new frmLoadPath();
        private HostTaskDisplay m_host1 = new HostTaskDisplay();
        private HostTaskDisplay m_host2 = new HostTaskDisplay();

        public Form1()
        {
            InitializeComponent();
            CreateTaskHostDisplays();
        }

        private void CreateTaskHostDisplays()
        {
            m_host1.Dock = DockStyle.Fill;
            m_host1.HostListUpdated += host1_HostListUpdated;
            m_host2.Dock = DockStyle.Fill;
            m_host2.HostListUpdated += host2_HostListUpdated;
            spcTaskHostDisplay.Panel1.Controls.Add(m_host1);
            spcTaskHostDisplay.Panel2.Controls.Add(m_host2);
        }

        private void host1_HostListUpdated(object sender, EventArgs e)
        {
            m_host2.UpdateHosts();
        }

        private void host2_HostListUpdated(object sender, EventArgs e)
        {
            m_host1.UpdateHosts();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void openFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(fbdBrowse.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(fbdBrowse.SelectedPath))
            {
                loadFileList(fbdBrowse.SelectedPath);
            }
        }

        private void loadFromPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(m_loadPath.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(m_loadPath.FolderPath))
            {
                if (Directory.Exists(m_loadPath.FolderPath))
                    loadFileList(m_loadPath.FolderPath);
            }
        }

        private void loadFileList(string folderPath)
        {
            m_xmlFiles.Clear();
            try
            {
                string[] xmlfiles = Directory.GetFiles(folderPath, "*.xml");
                foreach (string file in xmlfiles)
                    m_xmlFiles.Add(new CTaskFile(file));
                lbxTaskFiles.Items.Clear();
                lbxTaskFiles.BeginUpdate();
                foreach (CTaskFile file in m_xmlFiles)
                    lbxTaskFiles.Items.Add(file);
                lbxTaskFiles.EndUpdate();
            }
            catch(Exception ex) 
            {
                m_log.Error("Unable to get task XML files from " + folderPath, ex);
                MessageBox.Show("Unable to retrieve task XML files." + Environment.NewLine + ex.Message, "Can't Open Files");
            }
        }

        private void lbxTaskFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(lbxTaskFiles.SelectedItem != null)
            {
                try
                {
                    CTaskFile file = (CTaskFile)lbxTaskFiles.SelectedItem;
                    XmlReader reader = XmlReader.Create(new StringReader(File.ReadAllText(file.Path)));
                    if (reader.Read())
                    {
                        m_data.Clear();
                        m_data.ReadXml(reader);
                        m_settings = new CTaskSettings(m_data);
                        pnlDataGrids.Controls.Clear();
                        pnlDataGrids.Controls.Add(m_settings.GetSettingsDisplay());
                    }
                }
                catch (Exception ex)
                {
                    string msg = "Unable to read schedule task definition file " + lbxTaskFiles.SelectedItem.ToString();
                    m_log.Error(msg, ex);
                    MessageBox.Show(msg, "Operation Failure");
                }
            }
        }

        private void writeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CTaskFile file = (CTaskFile)lbxTaskFiles.SelectedItem;
            try
            {
                m_data.WriteXml(file.Path);
            }
            catch (Exception ex)
            {
                string msg = "Unable to disable all schedule task definition files.";
                m_log.Error(msg, ex);
                MessageBox.Show(msg, "Operation Failure");
            }
        }

        private void disableAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lbxTaskFiles.Items.Count < 1)
                return;
            bool success = true;
            for (int i = 0; i < lbxTaskFiles.Items.Count; ++i)
            {
                try
                {
                    CTaskFile file = (CTaskFile)lbxTaskFiles.Items[i];
                    m_log.Info("Reading " + file.Path);
                    XmlReader reader = XmlReader.Create(new StringReader(File.ReadAllText(file.Path)));
                    if (reader.Read())
                    {
                        m_data.Clear();
                        m_data.ReadXml(reader);
                        m_data.Tables["Settings"].Rows[0]["Enabled"] = "false";
                        m_log.Info("Writing " + file.Path);
                        m_data.WriteXml(file.Path);
                    }
                    else
                    {
                        success = false;
                        m_log.Warn("Failed to read " + file.Path);
                    }
                }
                catch (Exception ex)
                {
                    success = false;
                    string msg = "Unable to disable scheduled task definition file";
                    m_log.Error(msg, ex);
                }
            }
            if(!success)
                MessageBox.Show("One or more scheduled tasks were not successfully disabled.", "Operation Failure");
        }

        private void enableAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lbxTaskFiles.Items.Count < 1)
                return;
            bool success = true;
            for (int i = 0; i < lbxTaskFiles.Items.Count; ++i)
            {
                try
                {
                    CTaskFile file = (CTaskFile)lbxTaskFiles.Items[i];
                    m_log.Info("Reading " + file.Path);
                    XmlReader reader = XmlReader.Create(new StringReader(File.ReadAllText(file.Path)));
                    if (reader.Read())
                    {
                        m_data.Clear();
                        m_data.ReadXml(reader);
                        m_data.Tables["Settings"].Rows[0]["Enabled"] = "true";
                        m_log.Info("Writing " + file.Path);
                        m_data.WriteXml(file.Path);
                    }
                    else
                    {
                        success = false;
                        m_log.Warn("Failed to read " + file.Path);
                    }
                }
                catch (Exception ex)
                {
                    success = false;
                    string msg = "Unable to enable scheduled task definition file";
                    m_log.Error(msg, ex);
                    MessageBox.Show(msg, "Operation Failure");
                }
            }
            if (!success)
                MessageBox.Show("One or more scheduled tasks were not successfully enabled. ", "Operation Failure");
        }

        private void spcTaskHostDisplay_SplitterMoved(object sender, SplitterEventArgs e)
        {
            btnCopyToA.Left = spcTaskHostDisplay.SplitterDistance - btnCopyToA.Width - 5;
            btnCopyToB.Left = spcTaskHostDisplay.SplitterDistance + 7;
        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            btnCopyToA.Left = spcTaskHostDisplay.SplitterDistance - btnCopyToA.Width - 5;
            btnCopyToB.Left = spcTaskHostDisplay.SplitterDistance + 7;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnCopyToA.Left = spcTaskHostDisplay.SplitterDistance - btnCopyToA.Width - 5;
            btnCopyToB.Left = spcTaskHostDisplay.SplitterDistance + 7;
        }

        private void btnCopyToB_Click(object sender, EventArgs e)
        {
            string msg = "";
            if (spcTaskHostDisplay.Panel1.Controls[0] != null && spcTaskHostDisplay.Panel2.Controls[0] != null)
            {
                try
                {
                    HostTaskDisplay srcTask = (HostTaskDisplay)spcTaskHostDisplay.Panel1.Controls[0];
                    HostTaskDisplay tgtTask = (HostTaskDisplay)spcTaskHostDisplay.Panel2.Controls[0];
                    if (string.IsNullOrEmpty(srcTask.TaskHost) || string.IsNullOrEmpty(tgtTask.TaskHost))
                    {
                        MessageBox.Show("Must select a task host URL for both source and target.", "Select Task Hosts");
                        return;
                    }
                    // errmsg, just in case.
                    msg = string.Format("Error copying task from {0} to {1}", srcTask.TaskHost, tgtTask.TaskHost);
                    Microsoft.Win32.TaskScheduler.Task task = srcTask.CurrentTask.Task;
                    TaskService tgtService = null;
                    if (tgtTask.CurrentTask == null)
                        tgtService = new TaskService(tgtTask.TaskHost, "Eng_Autobuild", "AGI", "Aug0105");
                    else
                        tgtService = tgtTask.CurrentTask.Service;
                    TaskDefinition newTask = tgtService.NewTask();
                    newTask.XmlText = task.Definition.XmlText;
                    newTask.Settings.Enabled = false;
                    tgtService.RootFolder.RegisterTaskDefinition(srcTask.CurrentTask.Task.Path, newTask, TaskCreation.CreateOrUpdate, "AGI\\Eng_Autobuild", "Aug0105");
                    tgtTask.Refresh();
                }
                catch (Exception ex)
                {
                    m_log.Error(msg, ex);
                }
            }
            else
            {
                MessageBox.Show("One or both task displays are null.", "Cannot Copy Task");
            }
        }

        private void btnCopyToA_Click(object sender, EventArgs e)
        {
            if(spcTaskHostDisplay.Panel1.Controls[0] != null && spcTaskHostDisplay.Panel2.Controls[0] != null)
            {
                string msg = "";
                try
                {
                    HostTaskDisplay srcTask = (HostTaskDisplay)spcTaskHostDisplay.Panel2.Controls[0];
                    HostTaskDisplay tgtTask = (HostTaskDisplay)spcTaskHostDisplay.Panel1.Controls[0];
                    if(string.IsNullOrEmpty(srcTask.TaskHost) || string.IsNullOrEmpty(tgtTask.TaskHost))
                    {
                        MessageBox.Show("Must select a task host URL for both source and target.", "Select Task Hosts");
                        return;
                    }
                    // errmsg, just in case.
                    msg = string.Format("Error copying task from {0} to {1}", srcTask.TaskHost, tgtTask.TaskHost);
                    Microsoft.Win32.TaskScheduler.Task task = srcTask.CurrentTask.Task;
                    TaskService tgtService = null;
                    if (tgtTask.CurrentTask == null)
                        tgtService = new TaskService(tgtTask.TaskHost, "Eng_Autobuild", "AGI", "Aug0105");
                    else
                        tgtService = tgtTask.CurrentTask.Service;
                    TaskDefinition newTask = tgtService.NewTask();
                    newTask.XmlText = task.Definition.XmlText;
                    newTask.Settings.Enabled = false;
                    tgtService.RootFolder.RegisterTaskDefinition(srcTask.CurrentTask.Task.Path, newTask, TaskCreation.CreateOrUpdate, "AGI\\Eng_Autobuild", "Aug0105");
                    tgtTask.Refresh();
                }
                catch (Exception ex)
                {
                    m_log.Error(msg, ex);
                }
            }
            else
            {
                MessageBox.Show("One or both task displays are null.", "Cannot Copy Task");
            }
        }
    }

    public class CTaskFile : Object
    {
        private static ILog m_log = LogManager.GetLogger(typeof(CTaskFile));

        private string m_path = "";

        public string Path { get => m_path; set => m_path = value; }

        public CTaskFile() { }

        public CTaskFile(string path)
        {
            m_path = path;
        }

        public override string ToString()
        {
            try
            {
                return System.IO.Path.GetFileName(m_path);
            }
            catch(Exception ex)
            {
                m_log.Error("Error retrieving filename for " + m_path, ex);
            }
            return "";
        }
    }

    public class CTaskSettings
    {
        private static ILog m_log = LogManager.GetLogger(typeof(CTaskSettings));

        private DataTable m_settingsTable = new DataTable();
        private DataTable m_actionsTable = new DataTable();
        private Dictionary<string, string> m_settings = new Dictionary<string, string>();
        private Dictionary<string, string> m_actions = new Dictionary<string, string>();
        private Dictionary<TextBox, string> m_settingsValMap = new Dictionary<TextBox, string>();
        private Dictionary<TextBox, string> m_actionssValMap = new Dictionary<TextBox, string>();
        private int m_tbxOffset = 160;


        public CTaskSettings() { }

        public CTaskSettings(DataSet set)
        {
            try
            {
                if (set.Tables.Contains("Settings"))
                {
                    m_settingsTable = set.Tables["Settings"];
                    Label tmp = new Label();
                    tmp.AutoSize = true;
                    foreach (DataColumn col in set.Tables["Settings"].Columns)
                    {
                        m_settings.Add(col.ColumnName, set.Tables["Settings"].Rows[0][col.ColumnName].ToString());
                        tmp.Text = col.ColumnName;
                        tmp.Update();
                        if (tmp.Width + 4 > m_tbxOffset)
                            m_tbxOffset = tmp.Width + 4;
                    }
                }
                if (set.Tables.Contains("Exec"))
                {
                    m_actionsTable = set.Tables["Exec"];
                    Label tmp = new Label();
                    tmp.AutoSize = true;
                    foreach (DataColumn col in set.Tables["Exec"].Columns)
                    {
                        m_actions.Add(col.ColumnName, set.Tables["Exec"].Rows[0][col.ColumnName].ToString());
                        tmp.Text = col.ColumnName;
                        tmp.Update();
                        if (tmp.Width + 4 > m_tbxOffset)
                            m_tbxOffset = tmp.Width + 4;
                    }
                }
            }
            catch (Exception ex)
            {
                m_log.Error("Error constructing CTaskSettings object from table", ex);
            }
        }

        public CTaskSettings(DataTable tbl)
        {
            try
            {
                m_settingsTable = tbl;
                Label tmp = new Label();
                tmp.AutoSize = true;
                foreach (DataColumn col in tbl.Columns)
                {
                    m_settings.Add(col.ColumnName, tbl.Rows[0][col.ColumnName].ToString());
                    tmp.Text = col.ColumnName;
                    tmp.Update();
                    if (tmp.Width + 4 > m_tbxOffset)
                        m_tbxOffset = tmp.Width + 4;
                }
            }
            catch(Exception ex)
            {
                m_log.Error("Error constructing CTaskSettings object from table", ex);
            }
        }

        internal Control GetSettingsDisplay()
        {
            m_settingsValMap = new Dictionary<TextBox, string>();
            m_actionssValMap = new Dictionary<TextBox, string>();
            Panel container = new Panel();
            container.BorderStyle = BorderStyle.Fixed3D;
            container.AutoScroll = true;
            int curTop = 4;
            foreach(string key in m_settings.Keys)
            {
                Label lbl = new Label();
                lbl.AutoSize = true;
                lbl.Text = key;
                lbl.Left = 4;
                lbl.Top = curTop;
                container.Controls.Add(lbl);
                TextBox tbx = new TextBox();
                tbx.Text = m_settings[key];
                tbx.Left = lbl.Left + m_tbxOffset;
                tbx.Top = curTop;
                m_settingsValMap.Add(tbx, lbl.Text);
                tbx.TextChanged += tbx_txtChanged;
                container.Controls.Add(tbx);
                curTop += tbx.Height + 4;
            }
            Panel actionContainer = new Panel();
            actionContainer.BorderStyle = BorderStyle.Fixed3D;
            curTop = 4;
            int tbxHeight = 0;
            foreach (string key in m_actions.Keys)
            {
                Label lbl = new Label();
                lbl.AutoSize = true;
                lbl.Text = key;
                lbl.Left = 4;
                lbl.Top = curTop;
                actionContainer.Controls.Add(lbl);
                TextBox tbx = new TextBox();
                tbx.Width = 600;
                tbx.Text = m_actions[key];
                tbx.Left = lbl.Left + m_tbxOffset;
                tbx.Top = curTop;
                tbxHeight = tbx.Height;
                m_actionssValMap.Add(tbx, lbl.Text);
                tbx.TextChanged += tbx_txtChanged;
                actionContainer.Controls.Add(tbx);
                curTop += tbx.Height + 4;
            }
            if (m_actionssValMap.Keys.Count > 0)
            {
                actionContainer.AutoScroll = true;
                actionContainer.Height = (m_actionssValMap.Keys.Count * (tbxHeight + 4)) + 4;
                actionContainer.Dock = DockStyle.Bottom;
                container.Controls.Add(actionContainer);
            }
            container.Dock = DockStyle.Fill;
            return container;
        }

        public bool ImportActions(DataTable tbl)
        {
            try
            {
                m_actionsTable = tbl;
                Label tmp = new Label();
                tmp.AutoSize = true;
                foreach (DataColumn col in tbl.Columns)
                {
                    m_actions.Add(col.ColumnName, tbl.Rows[0][col.ColumnName].ToString());
                    tmp.Text = col.ColumnName;
                    tmp.Update();
                    if (tmp.Width + 4 > m_tbxOffset)
                        m_tbxOffset = tmp.Width + 4;
                }
                return true;
            }
            catch(Exception ex)
            {
                m_log.Error("Error importing actions", ex);
            }
            return false;
        }

        private void tbx_txtChanged(object sender, EventArgs e)
        {
            TextBox tbx = (TextBox)sender;
            if(m_settingsValMap.ContainsKey(tbx))
                m_settingsTable.Rows[0][m_settingsValMap[tbx]] = tbx.Text;
            else if(m_actionssValMap.ContainsKey(tbx))
                m_actionsTable.Rows[0][m_actionssValMap[tbx]] = tbx.Text;
        }
    }

    public class CServerData : Object
    {
        private static ILog m_log = LogManager.GetLogger(typeof(CServerData));

        public string HostName = "";
        public string Domain = "";
        public string Username = "";
        public string Password = "";

        public CServerData() { }

        public CServerData(string def)
        {
            if(string.IsNullOrEmpty(def))
            {
                m_log.Error("Host definition string is null or empty");
                return;
            }
            string[] parts = def.Split(',');
            if(parts.Length == 4)
            {
                HostName = parts[0].Trim();
                Domain = parts[1].Trim();
                Username = parts[2].Trim();
                Password = parts[3].Trim();
            }
            else
            {
                m_log.Error("Host definition incorrect : should be (host address),(domain),(username),(password)");
                return;
            }
        }

        public override string ToString()
        {
            return HostName;
        }

        public string ToDefinition()
        {
            return string.Format("{0},{1},{2},{3}", HostName, Domain, Username, Password);
        }
    }
}
