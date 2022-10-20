using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kasa_Hareket_Takip
{
    public partial class Form1 : Form
    {
        bool sidebarExpand;
        public Form1()
        {
            InitializeComponent();
        }

        private void sidebarTimer_Tick(object sender, EventArgs e)
        {
            if (sidebarExpand)
            {
                sidebar.Width -= 10;
                if (sidebar.Width==sidebar.MinimumSize.Width)
                {
                    sidebarExpand = false;
                    sidebarTimer.Stop();
                }
            }
            else
            {
                sidebar.Width += 10;
                if (sidebar.Width==sidebar.MaximumSize.Width)
                {
                    sidebarExpand = true;
                    sidebarTimer.Stop();
                }
            }
        }

        private void menuButton_Click(object sender, EventArgs e)
        {
            sidebarTimer.Start();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        int sayac = 1;
        private void button5_Click(object sender, EventArgs e)
        {
            if (sayac == 2)
            {
                this.WindowState = FormWindowState.Maximized;
                sayac = 1;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                sayac++;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void sidebar_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            MessageBox.Show("BU UYGULAMA ORİJİNAL OLARAK ÖZCAN YORULMAZ TARAFINDAN Z TECHNOLOGY MARKASI ALTINDA YAPILMIŞTIR TÜM HAKLARI SAKLIDIR.2022", "Z TECHNOLOGY ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            MessageBox.Show("İLETİŞİM : theyorulmazz42@gmail.com adresinden bize ulaşabilirsiniz", "Z TECHNOLOGY", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        private void panel5_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void panel5_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void panel5_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {

            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }
    }
}
