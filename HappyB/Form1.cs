using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HappyB
{
    public partial class Form1 : Form
    {
        string date = DateTime.Now.AddDays(+1).ToString("dd.MM.yyyy");

        public Form1()
        {
            InitializeComponent();
        }

        #region Обработка формы
        private void Form1_Deactivate(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = false;
                notifyIcon1.Visible = true;
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;           
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
                this.ShowInTaskbar = true;
                notifyIcon1.Visible = false;
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            WindowState = FormWindowState.Minimized;
            e.Cancel = true;           
        }

        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {
            List<string> actionsShow = new List<string>();

            string[] actions = File.ReadAllLines("hb.txt", Encoding.GetEncoding(1251));
            foreach (string str in actions)
            {
                string[] actionStr = str.Split(';');
                string nameActions = actionStr[0];
                string dateActions = actionStr[1];
                if (dateActions.Remove(5) == date.Remove(5))
                    actionsShow.Add(str);
            }
            if (actionsShow.Count != 0)
            {
                string reliseStr = "";
                foreach (string str in actionsShow)
                {
                    string[] actionStr = str.Split(';');
                    string nameActions = actionStr[0];
                    string dateActions = actionStr[1];

                    reliseStr += nameActions;
                }
                lblResult.Text = "Завтра мероприятие у \n" + reliseStr + "\n";
            }
        }
    }
}
