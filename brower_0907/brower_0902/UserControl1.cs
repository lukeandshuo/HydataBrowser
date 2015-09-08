using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using HyData;

namespace brower_0902
{
    public partial class UserControl1 : UserControl
    {
        public ChromeWebBrowser thisChrome = null;
        public UserControl1()
        {
            InitializeComponent();
        }

        public UserControl1(CSharpBrowserSettings settings)
        {

            InitializeComponent();
            thisChrome = new ChromeWebBrowser();
            thisChrome.Initialize(settings);
            panel1.Controls.Add(thisChrome);
            thisChrome.Validate();
            thisChrome.Dock = DockStyle.Fill;
  
        }
        /// <summary>
        /// Back按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackButton_Click(object sender, EventArgs e)
        {
            if (thisChrome != null)
            {
                if (thisChrome.canGoBack())
                {
                    thisChrome.GoBack();
                }
            }
        }

        /// <summary>
        /// 前进按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ForwardButton_Click(object sender, EventArgs e)
        {
            if (thisChrome != null)
            {
                if (thisChrome.canGoForward())
                {
                    thisChrome.GoForward();
                }
            }
        }

        /// <summary>
        /// 刷新按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefreshButton_Click(object sender, EventArgs e)
        {
            if (thisChrome != null)
            {
                thisChrome.Reload();
            }
        }
        /// <summary>
        /// GO按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GoButton_Click(object sender, EventArgs e)
        {
            if (URLTextBox.Text != string.Empty)
            {
                thisChrome.OpenUrl(URLTextBox.Text);
            }
            GoButton.Text = "Go";
        }

        /// <summary>
        /// 回车键事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void URLTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GoButton_Click(sender, e);
            }
        }

        /// <summary>
        /// ToolStrip布局
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStrip1_Layout(object sender, LayoutEventArgs e)
        {
            HandleToolStripLayout();
        }

        private void HandleToolStripLayout()
        {
            var width = toolStrip1.Width;
            foreach (ToolStripItem item in toolStrip1.Items)
            {
                if (item != URLTextBox)
                {
                    width -= item.Width - item.Margin.Horizontal;
                }
            }
            URLTextBox.Width = Math.Max(0, width - URLTextBox.Margin.Horizontal - 18);
        }

        /// <summary>
        /// Copy点击事件（右键菜单）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Copy");
        }

        /// <summary>
        /// Select点击事件（右键菜单）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("SelectToolStripMenuItem");
        }

        /// <summary>
        /// Paste点击事件（右键菜单）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("paste");
        }

        /// <summary>
        /// All点击事件（右键菜单）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("all");
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                panel1.ContextMenuStrip = contextMenuStrip1;
            }
        }          
    }
}
