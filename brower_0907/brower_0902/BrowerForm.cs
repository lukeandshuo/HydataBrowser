using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace brower_0902
{
    public partial class BrowerForm : Form
    {
        private const string DefaultUrlForAddedTabs = "https://www.baidu.com";
        // Default to a small increment:
        private const double ZoomIncrement = 0.10;

        public BrowerForm()
        {
            InitializeComponent();

            var bitness = Environment.Is64BitProcess ? "x64" : "x86";
            Text = "CefSharp.WinForms.Example - " + bitness;
            WindowState = FormWindowState.Maximized;
            //AddTab(CefExample.DefaultUrl);

            //Only perform layout when control has completly finished resizing
            ResizeBegin += (s, e) => SuspendLayout();
            ResizeEnd += (s, e) => ResumeLayout(true);
        }

        /// <summary>
        /// 根据某个url添加界面
        /// </summary>
        /// <param name="url"></param>
        /// <param name="insertIndex"></param>
        private void AddTab(string url, int? insertIndex = null)
        {
            browerTabControl.SuspendLayout();

            var brower = new UserControl1(url)
            {
                Dock=DockStyle.Fill,
            };

            var tabPage = new TabPage(url)
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
            AddTab(DefaultUrlForAddedTabs);
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
    }
}
