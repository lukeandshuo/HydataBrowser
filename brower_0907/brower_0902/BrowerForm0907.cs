using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using HyData;

namespace brower_0902
{
    public partial class BrowerForm : Form
    {
        private CSharpBrowserSettings initSetting;
        public BrowerForm()
        {
            InitializeComponent();

            var bitness = Environment.Is64BitProcess ? "x64" : "x86";
            Text = "CefSharp.WinForms.Example - " + bitness;
            WindowState = FormWindowState.Maximized;

            ResizeBegin += (s, e) => SuspendLayout();
            ResizeEnd += (s, e) => ResumeLayout(true);
        }

        /// <summary>
        /// 根据某个url添加界面
        /// </summary>
        /// <param name="url"></param>
        /// <param name="insertIndex"></param>
        private void AddTab(CSharpBrowserSettings setting, int? insertIndex = null)
        {
            browerTabControl.SuspendLayout();

            var brower = new UserControl1(initSetting)
            {
                Dock = DockStyle.Fill,
            };

            var tabPage = new TabPage(setting.DefaultUrl)
            {
                Dock = DockStyle.Fill
            };

            brower.CreateControl();
            tabPage.Controls.Add(brower);

            if (insertIndex == null)
            {
                browerTabControl.TabPages.Add(tabPage);
            }
            else
            {
                browerTabControl.TabPages.Insert(insertIndex.Value, tabPage);
            }

            browerTabControl.SelectedTab = tabPage;
            browerTabControl.ResumeLayout(true);
        }

        /// <summary>
        /// NewTab按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddTab(initSetting);
        }

        /// <summary>
        /// 关闭Tab按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (browerTabControl.Controls.Count == 0)
            {
                return;
            }

            var currentIndex = browerTabControl.SelectedIndex;
            var tabPage = browerTabControl.Controls[currentIndex];
            var control = GetCurrentTabControl();
            if (control != null)
            {
                control.Dispose();
            }

            browerTabControl.Controls.Remove(tabPage);
            tabPage.Dispose();

            browerTabControl.SelectedIndex = currentIndex - 1;

            if (browerTabControl.Controls.Count == 0)
            {
                ExitApplication();
            }
        }

        /// <summary>
        /// 获得当前页面
        /// </summary>
        /// <returns></returns>
        private UserControl1 GetCurrentTabControl()
        {
            if (browerTabControl.SelectedIndex == -1)
            {
                return null;
            }
            var tabPage = browerTabControl.Controls[browerTabControl.SelectedIndex];
            var control = (UserControl1)tabPage.Controls[0];
            return control;
        }

        /// <summary>
        /// 退出应用
        /// </summary>
        private void ExitApplication()
        {
            Close();
        }

        private void BrowerForm_Load(object sender, EventArgs e)
        {
            initSetting = new CSharpBrowserSettings();
            initSetting.DefaultUrl = System.IO.Directory.GetCurrentDirectory() + "\\TestHtml\\test.html";
            initSetting.CachePath = @"C:\temp\caches";
            AddTab(initSetting);
        }

        public void Call(string msg)
        {
            MessageBox.Show("im c#");
            MessageBox.Show(msg);
            var currentChrome = GetCurrentTabControl();
            string myMsg = string.Format("CallBack('{0}');","message from c#");
            currentChrome.thisChrome.ExecuteScript(myMsg);
        }

        private void menuStrip1_Resize(object sender, EventArgs e)
        {
            //var width = menuStrip1.Width;
            //foreach (ToolStripItem item in menuStrip1.Items)
            //{            
            //        width -= item.Width - item.Margin.Horizontal;              
            //}
        }

        ////禁用窗体的关闭按钮
        //private const int CP_NOCLOSE_BUTTON = 0x200;
        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams myCp = base.CreateParams;
        //        myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
        //        return myCp;
        //    }
        //}
    }
}
