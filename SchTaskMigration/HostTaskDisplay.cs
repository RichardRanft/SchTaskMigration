using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32.TaskScheduler;
using System.Windows.Forms;
using System.IO;
using log4net;

namespace SchTaskMigration
{
    public partial class HostTaskDisplay : UserControl
    {
        private static ILog m_log = LogManager.GetLogger(typeof(HostTaskDisplay));
        private List<CServerData> m_taskHosts = new List<CServerData>();
        private TaskService m_taskScheduler = new TaskService();
        private CTaskDisplay m_currentTask;
        private AddHost m_addHost = new AddHost();
        private bool m_showOnlyEnabled = false;

        public event EventHandler HostListUpdated;

        protected virtual void OnHostListUpdated(EventArgs e)
        {
            EventHandler handler = HostListUpdated;
            handler?.Invoke(this, e);
        }

        public CTaskDisplay CurrentTask { get => m_currentTask; private set => m_currentTask = value; }

        public string TaskHost
        {
            get { return cbxHosts.SelectedItem == null ? "" : cbxHosts.SelectedItem.ToString(); }
        }

        public TaskService TaskScheduler { get => m_taskScheduler; private set => m_taskScheduler = value; }

        public HostTaskDisplay()
        {
            InitializeComponent();
            loadHostFile();
        }

        public override void Refresh()
        {
            refreshTaskList();
        }

        public void UpdateHosts()
        {
            loadHostFile();
        }

        private void loadHostFile()
        {
            if (File.Exists("taskhosts.txt"))
            {
                try
                {
                    using (StreamReader sr = new StreamReader("taskhosts.txt"))
                    {
                        string line = "";
                        while (!sr.EndOfStream)
                        {
                            line = sr.ReadLine();
                            if (!string.IsNullOrEmpty(line))
                                m_taskHosts.Add(new CServerData(line));
                        }
                    }
                    cbxHosts.BeginUpdate();
                    cbxHosts.Items.Clear();
                    foreach (CServerData host in m_taskHosts)
                        cbxHosts.Items.Add(host);
                    cbxHosts.EndUpdate();
                }
                catch (Exception ex)
                {
                    m_log.Error("Unable to load taskhosts.txt", ex);
                }
            }
        }

        private void cbxHosts_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (cbxHosts.SelectedItem != null)
                {
                    CServerData dat = (CServerData)cbxHosts.SelectedItem;
                    m_log.Info("Selecting schedule host " + dat.HostName);
                    TaskScheduler = new TaskService(dat.HostName, dat.Username, dat.Domain, dat.Password);
                    refreshTaskList();
                }
            }
            catch(Exception ex)
            {
                m_log.Error("Error connecting to task host " + cbxHosts.SelectedItem.ToString(), ex);
            }
        }

        private void refreshTaskList()
        {
            lbxTasks.BeginUpdate();
            lbxTasks.Items.Clear();
            foreach (Microsoft.Win32.TaskScheduler.Task task in TaskScheduler.AllTasks)
            {
                if (task.ReadOnly)
                    continue;
                if (task.Definition.Principal.UserId == "SYSTEM")
                    continue;
                if (task.Name.StartsWith("Optimize Start Menu Cache Files"))
                    continue;
                if (task.Name.StartsWith("AD RMS Rights Policy"))
                    continue;
                if (task.Definition.Settings.Hidden)
                    continue;
                if (m_showOnlyEnabled)
                {
                    if (task.Definition.Settings.Enabled)
                        lbxTasks.Items.Add(task);
                }
                else
                    lbxTasks.Items.Add(task);
            }
            lbxTasks.EndUpdate();
        }

        private void lbxTasks_SelectedIndexChanged(object sender, EventArgs e)
        {
            CServerData dat = (CServerData)cbxHosts.SelectedItem;
            Microsoft.Win32.TaskScheduler.Task task = TaskScheduler.FindTask(lbxTasks.SelectedItem.ToString());
            CurrentTask = new CTaskDisplay(task, TaskScheduler, dat);
            splitContainer1.Panel2.Controls.Clear();
            splitContainer1.Panel2.Controls.Add(CurrentTask.GetDisplay());
        }

        private void ckbOnlyEnabled_CheckedChanged(object sender, EventArgs e)
        {
            m_showOnlyEnabled = ckbOnlyEnabled.Checked;
            refreshTaskList();
        }

        private void cbxHosts_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    ComboBox box = (ComboBox)sender;
                    tryAddHost(box);
                }
                catch (Exception ex)
                {
                    m_log.Error("Error casting " + sender.ToString() + " to ComboBox control to add host to host list.", ex);
                }
            }
        }

        private void tryAddHost(ComboBox box)
        {
            string newhost = box.Text.Trim();
            foreach(object item in box.Items)
            {
                if(item.ToString() == newhost)
                {
                    MessageBox.Show("Host " + newhost + " is already in the host list", "Item Already Present");
                    return;
                }
                m_addHost.Clear();
                if(m_addHost.DialogResult == DialogResult.OK)
                {
                    string definition = string.Format("{0},{1},{2},{3}", newhost, m_addHost.Domain, m_addHost.Username, m_addHost.Password);
                    CServerData dat = new CServerData(definition);
                    Dictionary<string, CServerData> servers = new Dictionary<string, CServerData>();
                    foreach(object obj in box.Items)
                    {
                        try
                        {
                            CServerData datobj = (CServerData)obj;
                            servers.Add(datobj.HostName, datobj);
                        }
                        catch(Exception ex)
                        {
                            m_log.Error("Unable to cast combobox item back to CServerData objects", ex);
                        }
                    }
                    servers.Add(dat.HostName, dat);
                    string[] keys = servers.Keys.ToArray();
                    Array.Sort(keys);
                    box.BeginUpdate();
                    box.Items.Clear();
                    foreach (string key in keys)
                        box.Items.Add(servers[key]);
                    box.EndUpdate();
                    box.SelectedItem = dat;
                    updateTaskHosts(servers);
                }
            }
        }

        private void updateTaskHosts(Dictionary<string, CServerData> servers)
        {
            string[] keys = servers.Keys.ToArray();
            Array.Sort(keys);
            try
            {
                using (StreamWriter sw = new StreamWriter("taskhosts.txt"))
                {
                    foreach (string key in keys)
                        sw.WriteLine(servers[key].ToDefinition());
                }
                OnHostListUpdated(new EventArgs());
            }
            catch (Exception ex)
            {
                m_log.Error("Unable to write new taskHosts.txt file.", ex);
            }
        }
    }

    public class CTaskDisplay
    {
        private static ILog m_log = LogManager.GetLogger(typeof(CTaskDisplay));

        private Microsoft.Win32.TaskScheduler.Task m_task;

        private TaskService m_service;

        private CServerData m_data;

        public Microsoft.Win32.TaskScheduler.Task Task { get => m_task; set => m_task = value; }

        public TaskService Service { get => m_service; set => m_service = value; }

        public CTaskDisplay() { }

        public CTaskDisplay(Microsoft.Win32.TaskScheduler.Task task, TaskService service, CServerData data)
        {
            m_task = task;
            m_service = service;
            m_data = data;
        }

        public Control GetDisplay()
        {
            Panel root = new Panel();
            root.Dock = DockStyle.Fill;
            root.AutoScroll = true;
            root.BorderStyle = BorderStyle.Fixed3D;
            int curTop = 4;
            Label lblName = new Label();
            lblName.Text = m_task.Name;
            lblName.Font = new Font(Label.DefaultFont, FontStyle.Bold);
            lblName.Width = 750;
            lblName.Top = curTop + 2;
            lblName.Left = 4;
            root.Controls.Add(lblName);
            curTop += lblName.Height + 2;
            lblName = new Label();
            lblName.Text = "Status : ";
            lblName.Width = 60;
            lblName.Top = curTop;
            lblName.Left = 4;
            root.Controls.Add(lblName);
            Label lblDesc = new Label();
            lblDesc.Name = "lblStatus";
            lblDesc.Width = 100;
            lblDesc.AutoSize = true;
            lblDesc.Top = curTop;
            lblDesc.Left = lblName.Left + lblName.Width + 4;
            lblDesc.Text = string.IsNullOrEmpty(m_task.State.ToString()) ? "\"\"" : m_task.State.ToString();
            root.Controls.Add(lblDesc);
            curTop += lblName.Height + 2;
            CheckBox ckbEnabled = new CheckBox();
            ckbEnabled.AutoSize = true;
            ckbEnabled.Name = "ckbEnabled";
            ckbEnabled.Text = "Enabled";
            ckbEnabled.Checked = m_task.Enabled;
            ckbEnabled.Left = 4;
            ckbEnabled.Top = curTop;
            ckbEnabled.CheckedChanged += CkbEnabled_CheckedChanged;
            root.Controls.Add(ckbEnabled);
            curTop += ckbEnabled.Height + 2;
            int stateBtnLeft = lblDesc.Left;
            Button btnStateChange = new Button();
            btnStateChange.Name = "taskStartBtn";
            btnStateChange.Text = "Start";
            btnStateChange.Left = stateBtnLeft;
            btnStateChange.Top = curTop;
            btnStateChange.Enabled = (m_task.State != TaskState.Running && m_task.State != TaskState.Queued && m_task.Enabled);
            btnStateChange.Click += BtnStateChange_Click;
            root.Controls.Add(btnStateChange);
            stateBtnLeft += btnStateChange.Width + 4;
            btnStateChange = new Button();
            btnStateChange.Name = "taskStopBtn";
            btnStateChange.Text = "Stop";
            btnStateChange.Left = stateBtnLeft;
            btnStateChange.Top = curTop;
            btnStateChange.Enabled = (m_task.State == TaskState.Running || m_task.State == TaskState.Queued) && m_task.Enabled;
            btnStateChange.Click += BtnStateChange_Click;
            root.Controls.Add(btnStateChange);
            curTop += btnStateChange.Height + 2;
            lblName = new Label();
            lblName.Text = "Description :";
            lblName.Top = curTop;
            lblName.Left = 4;
            lblName.Width = 60;
            root.Controls.Add(lblName);
            lblDesc = new Label();
            lblDesc.Width = 600;
            lblDesc.AutoSize = true;
            lblDesc.Top = curTop;
            lblDesc.Left = lblName.Left + lblName.Width + 4;
            lblDesc.Text = string.IsNullOrEmpty(m_task.Definition.RegistrationInfo.Description) ? "\"\"" : m_task.Definition.RegistrationInfo.Description;
            root.Controls.Add(lblDesc);
            curTop += lblName.Height + 2;
            var actions = m_task.Definition.Actions.ToList();
            for (int i = 0; i < actions.Count; ++i)
            {
                ExecAction action = null;
                try
                {
                    action = (ExecAction)actions[i];
                }
                catch { continue; }
                lblName = new Label();
                lblName.Text = "Action :";
                lblName.Top = curTop;
                lblName.Left = 4;
                lblName.Width = 60;
                root.Controls.Add(lblName);
                lblDesc = new Label();
                lblDesc.Width = 600;
                lblDesc.AutoSize = true;
                lblDesc.Top = curTop;
                lblDesc.Left = lblName.Left + lblName.Width + 4;
                lblDesc.Text = string.IsNullOrEmpty(action.Path) ? "\"\"" : action.Path;
                root.Controls.Add(lblDesc);
                curTop += lblName.Height + 2;
                lblDesc = new Label();
                lblDesc.Width = 600;
                lblDesc.AutoSize = true;
                lblDesc.Top = curTop;
                lblDesc.Left = lblName.Left + lblName.Width + 4;
                lblDesc.Text = string.IsNullOrEmpty(action.Arguments) ? "\"\"" : action.Arguments;
                root.Controls.Add(lblDesc);
                curTop += lblName.Height + 2;
            }
            lblName = new Label();
            lblName.Text = "Last Run :";
            lblName.Width = 60;
            lblName.Top = curTop;
            lblName.Left = 4;
            root.Controls.Add(lblName);
            lblDesc = new Label();
            lblDesc.Width = 600;
            lblDesc.AutoSize = true;
            lblDesc.Top = curTop;
            lblDesc.Left = lblName.Left + lblName.Width + 4;
            lblDesc.Text = m_task.LastRunTime != null ? string.Format("{0} : result {1}", m_task.LastRunTime.ToString(), m_task.LastTaskResult) : "Unknown";
            root.Controls.Add(lblDesc);
            curTop += lblName.Height + 2;
            lblName = new Label();
            lblName.Text = "Triggers :";
            lblName.Font = new Font(Label.DefaultFont, FontStyle.Bold);
            lblName.Width = 750;
            lblName.Top = curTop + 2;
            lblName.Left = 4;
            root.Controls.Add(lblName);
            curTop += lblName.Height + 2;
            foreach(Trigger trigger in m_task.Definition.Triggers)
            {
                lblName = new Label();
                lblName.Text = "Type :";
                lblName.Top = curTop;
                lblName.Left = 4;
                lblName.Width = 60;
                root.Controls.Add(lblName);
                lblDesc = new Label();
                lblDesc.Width = 600;
                lblDesc.AutoSize = true;
                lblDesc.Top = curTop;
                lblDesc.Left = lblName.Left + lblName.Width + 4;
                lblDesc.Text = trigger.TriggerType.ToString();
                root.Controls.Add(lblDesc);
                curTop += lblName.Height + 2;
                lblName = new Label();
                lblName.Text = "Start :";
                lblName.Top = curTop;
                lblName.Left = 4;
                lblName.Width = 60;
                root.Controls.Add(lblName);
                lblDesc = new Label();
                lblDesc.Width = 600;
                lblDesc.AutoSize = true;
                lblDesc.Top = curTop;
                lblDesc.Left = lblName.Left + lblName.Width + 4;
                lblDesc.Text = trigger.StartBoundary.ToString();
                root.Controls.Add(lblDesc);
                curTop += lblName.Height + 2;
                lblName = new Label();
                lblName.Text = "End :";
                lblName.Top = curTop;
                lblName.Left = 4;
                lblName.Width = 60;
                root.Controls.Add(lblName);
                lblDesc = new Label();
                lblDesc.Width = 600;
                lblDesc.AutoSize = true;
                lblDesc.Top = curTop;
                lblDesc.Left = lblName.Left + lblName.Width + 4;
                lblDesc.Text = trigger.EndBoundary.ToString();
                root.Controls.Add(lblDesc);
                curTop += lblName.Height + 2;
                lblName = new Label();
                lblName.Text = "Repeat :";
                lblName.Top = curTop;
                lblName.Left = 4;
                lblName.Width = 60;
                root.Controls.Add(lblName);
                lblDesc = new Label();
                lblDesc.Width = 600;
                lblDesc.AutoSize = true;
                lblDesc.Top = curTop;
                lblDesc.Left = lblName.Left + lblName.Width + 4;
                lblDesc.Text = trigger.ToString();
                root.Controls.Add(lblDesc);
                curTop += lblName.Height + 2;
            }
            return root;
        }

        private void BtnStateChange_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if(btn.Name == "taskStartBtn")
            {
                try
                {
                    Microsoft.Win32.TaskScheduler.Task t = m_task.Run();
                    m_log.InfoFormat("{0} - {1}", t.Name, t.State);
                    btn.Enabled = false;
                    m_task = t;
                    foreach (Control c in btn.Parent.Controls)
                    {
                        if (c.Name == "taskStopBtn")
                            c.Enabled = true;
                        if (c.Name == "lblStatus")
                            c.Text = m_task.State.ToString();
                    }
                }
                catch(Exception ex)
                {
                    m_log.Error("Error starting task " + m_task.Name + " on " + m_task.TaskService.TargetServer, ex);
                }
            }
            if (btn.Name == "taskStopBtn")
            {
                try
                {
                    m_task.Stop();
                    Microsoft.Win32.TaskScheduler.Task t = m_task.TaskService.GetTask(m_task.Path);
                    m_log.InfoFormat("{0} - {1}", t.Name, t.State);
                    btn.Enabled = false;
                    m_task = t;
                    foreach (Control c in btn.Parent.Controls)
                    {
                        if (c.Name == "taskStartBtn")
                            c.Enabled = true;
                        if (c.Name == "lblStatus")
                            c.Text = m_task.State.ToString();
                    }
                }
                catch(Exception ex)
                {
                    m_log.Error("Error stopping task " + m_task.Name + " on " + m_task.TaskService.TargetServer, ex);
                }
            }
        }

        private void CkbEnabled_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox ckb = (CheckBox)sender;
                if (ckb.Name == "ckbEnabled")
                {                    
                    m_task.Definition.Settings.Enabled = ckb.Checked;
                    m_task.Enabled = ckb.Checked;
                    m_log.Info(m_task.State.ToString());
                    if (m_task.Definition.Settings.RunOnlyIfLoggedOn)
                    {
                        string user = string.Format("{0}\\{1}", m_data.Domain, m_data.Username);
                        m_task = m_service.RootFolder.RegisterTaskDefinition(m_task.Path, m_task.Definition, TaskCreation.CreateOrUpdate, user, m_data.Password);
                    }
                    else
                        m_task.RegisterChanges();
                    foreach (Control c in ckb.Parent.Controls)
                    {
                        if (c.Name == "taskStartBtn")
                        {
                            c.Enabled = (m_task.State != TaskState.Running && m_task.State != TaskState.Queued && m_task.Enabled);
                        }
                        if (c.Name == "taskStopBtn")
                        {
                            c.Enabled = (m_task.State == TaskState.Running || m_task.State == TaskState.Queued) && m_task.Enabled;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                m_log.Error("Error updating task settings.", ex);
            }
        }
    }
}
