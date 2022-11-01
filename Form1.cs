using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kasa_Hareket_Takip
{
    public partial class Form1 : Form
    {
        bool sidebarExpand;
        private int bordersize = 2;
        private Size formsize;
        public Form1()
        {
            InitializeComponent();
            loadform(new AnaSayfa());
            this.Padding = new Padding(bordersize);
            this.BackColor = Color.FromArgb(69, 69, 69);
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd,int Msg, IntPtr wParam, IntPtr lParam);

        public void loadform(object Form)
        {
            if (this.mainpanel.Controls.Count > 0)
                this.mainpanel.Controls.RemoveAt(0);
            Form f = Form as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.mainpanel.Controls.Add(f);
            this.mainpanel.Tag = f;
            f.Show();
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

        
        private void button5_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                formsize = this.ClientSize;
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                this.Size = formsize;
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
            loadform(new Iletisim());
        }

        private void button8_Click(object sender, EventArgs e)
        {
            loadform(new Kasa());
        }

        //private bool dragging = false;
        //private Point dragCursorPoint;
        //private Point dragFormPoint;

        private void Panel5_MouseDown(object sender, MouseEventArgs e)
        {
            //dragging = true;
            //dragCursorPoint = Cursor.Position;
            //dragFormPoint = this.Location;

            ReleaseCapture();
            SendMessage(this.Handle, 0x112, (IntPtr)0xf012, (IntPtr)0);
        }

        private void panel5_MouseMove(object sender, MouseEventArgs e)
        {
            //if (dragging)
            //{
            //    Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
            //    this.Location = Point.Add(dragFormPoint, new Size(dif));
            //}
        }

        private void panel5_MouseUp(object sender, MouseEventArgs e)
        {
            //dragging = false;
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {

            //dragging = true;
            //dragCursorPoint = Cursor.Position;
            //dragFormPoint = this.Location;
        }

        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            //if (dragging)
            //{
            //    Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
            //    this.Location = Point.Add(dragFormPoint, new Size(dif));
            //}
        }

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            //dragging = false;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            //dragging = true;
            //dragCursorPoint = Cursor.Position;
            //dragFormPoint = this.Location;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            //if (dragging)
            //{
            //    Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
            //    this.Location = Point.Add(dragFormPoint, new Size(dif));
            //}
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            //dragging = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            loadform(new AnaSayfa());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            loadform(new Gelir());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            loadform(new Gider());
        }

        private void button9_Click(object sender, EventArgs e)
        {
            loadform(new Hakkinda());
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_NCCALCSIZE = 0X0083;
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_MINIMIZE = 0xF020; 
            const int SC_RESTORE = 0xF120; 
            const int WM_NCHITTEST = 0x0084;
            const int resizeAreaSize = 10;
            #region Form Resize
            
            const int HTCLIENT = 1; 
            const int HTLEFT = 10;  
            const int HTRIGHT = 11; 
            const int HTTOP = 12;   
            const int HTTOPLEFT = 13;
            const int HTTOPRIGHT = 14;
            const int HTBOTTOM = 15; 
            const int HTBOTTOMLEFT = 16;
            const int HTBOTTOMRIGHT = 17;
            
            if (m.Msg == WM_NCHITTEST)
            { 
                base.WndProc(ref m);
                if (this.WindowState == FormWindowState.Normal)
                {
                    if ((int)m.Result == HTCLIENT)
                    {
                        Point screenPoint = new Point(m.LParam.ToInt32());
                        Point clientPoint = this.PointToClient(screenPoint);
                        if (clientPoint.Y <= resizeAreaSize)
                        {
                            if (clientPoint.X <= resizeAreaSize)
                                m.Result = (IntPtr)HTTOPLEFT; 
                            else if (clientPoint.X < (this.Size.Width - resizeAreaSize))
                                m.Result = (IntPtr)HTTOP; 
                            else 
                                m.Result = (IntPtr)HTTOPRIGHT;
                        }
                        else if (clientPoint.Y <= (this.Size.Height - resizeAreaSize))
                        {
                            if (clientPoint.X <= resizeAreaSize)
                                m.Result = (IntPtr)HTLEFT;
                            else if (clientPoint.X > (this.Width - resizeAreaSize))
                                m.Result = (IntPtr)HTRIGHT;
                        }
                        else
                        {
                            if (clientPoint.X <= resizeAreaSize)
                                m.Result = (IntPtr)HTBOTTOMLEFT;
                            else if (clientPoint.X < (this.Size.Width - resizeAreaSize))
                                m.Result = (IntPtr)HTBOTTOM;
                            else 
                                m.Result = (IntPtr)HTBOTTOMRIGHT;
                        }
                    }
                }
                return;
            }
            #endregion
            
            if (m.Msg == WM_NCCALCSIZE && m.WParam.ToInt32() == 1)
            {
                return;
            }
            
            if (m.Msg == WM_SYSCOMMAND)
            {
                
                int wParam = (m.WParam.ToInt32() & 0xFFF0);
                if (wParam == SC_MINIMIZE)
                    formsize = this.ClientSize;
                if (wParam == SC_RESTORE)
                    this.Size = formsize;
            }
            base.WndProc(ref m);
            if (m.Msg==WM_NCCALCSIZE && m.WParam.ToInt64()==1)
            {
                return;
            }
            base.WndProc(ref m);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            AdjustForm();
        }

        private void AdjustForm()
        {
            switch (this.WindowState)
            {
                case FormWindowState.Normal:
                    if(this.Padding.Top!=bordersize)
                    this.Padding = new Padding(bordersize);
                    break;
                case FormWindowState.Maximized:
                    this.Padding=new Padding(8,8,8,0);
                    break;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            formsize = this.ClientSize;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
         
        }
    }
}
