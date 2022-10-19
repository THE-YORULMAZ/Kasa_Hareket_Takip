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
        public Form1()
        {
            InitializeComponent();
            control = false;
            memu_width = panelMemu.Width;
        }
        int sayac = 1;
        bool control;
        int memu_width;
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (control)
            {
                panelMemu.Width = panelMemu.Width + 10;
                if (panelMemu.Width >= memu_width)
                {
                    timer1.Stop();
                    control = false;
                    this.Refresh();
                }
            }
            else
            {
                panelMemu.Width = panelMemu.Width - 10;
                if (panelMemu.Width <= 0)
                {
                    timer1.Stop();
                    control = true;
                    this.Refresh();
                }
            }
        }

        private void btnminimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnmaximize_Click(object sender, EventArgs e)
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

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void label1_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }
    }
}
