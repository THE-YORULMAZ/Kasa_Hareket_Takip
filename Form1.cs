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
            const int SC_MINIMIZE = 0xF020; //Minimize form (Before)
            const int SC_RESTORE = 0xF120; //Restore form (Before)
            const int WM_NCHITTEST = 0x0084;//Win32, Mouse Input Notification: Determine what part of the window corresponds to a point, allows to resize the form.
            const int resizeAreaSize = 10;
            #region Form Resize
            // Resize/WM_NCHITTEST values
            const int HTCLIENT = 1; //Represents the client area of the window
            const int HTLEFT = 10;  //Left border of a window, allows resize horizontally to the left
            const int HTRIGHT = 11; //Right border of a window, allows resize horizontally to the right
            const int HTTOP = 12;   //Upper-horizontal border of a window, allows resize vertically up
            const int HTTOPLEFT = 13;//Upper-left corner of a window border, allows resize diagonally to the left
            const int HTTOPRIGHT = 14;//Upper-right corner of a window border, allows resize diagonally to the right
            const int HTBOTTOM = 15; //Lower-horizontal border of a window, allows resize vertically down
            const int HTBOTTOMLEFT = 16;//Lower-left corner of a window border, allows resize diagonally to the left
            const int HTBOTTOMRIGHT = 17;//Lower-right corner of a window border, allows resize diagonally to the right
            ///<Doc> More Information: https://docs.microsoft.com/en-us/windows/win32/inputdev/wm-nchittest </Doc>
            if (m.Msg == WM_NCHITTEST)
            { //If the windows m is WM_NCHITTEST
                base.WndProc(ref m);
                if (this.WindowState == FormWindowState.Normal)//Resize the form if it is in normal state
                {
                    if ((int)m.Result == HTCLIENT)//If the result of the m (mouse pointer) is in the client area of the window
                    {
                        Point screenPoint = new Point(m.LParam.ToInt32()); //Gets screen point coordinates(X and Y coordinate of the pointer)                           
                        Point clientPoint = this.PointToClient(screenPoint); //Computes the location of the screen point into client coordinates                          
                        if (clientPoint.Y <= resizeAreaSize)//If the pointer is at the top of the form (within the resize area- X coordinate)
                        {
                            if (clientPoint.X <= resizeAreaSize) //If the pointer is at the coordinate X=0 or less than the resizing area(X=10) in 
                                m.Result = (IntPtr)HTTOPLEFT; //Resize diagonally to the left
                            else if (clientPoint.X < (this.Size.Width - resizeAreaSize))//If the pointer is at the coordinate X=11 or less than the width of the form(X=Form.Width-resizeArea)
                                m.Result = (IntPtr)HTTOP; //Resize vertically up
                            else //Resize diagonally to the right
                                m.Result = (IntPtr)HTTOPRIGHT;
                        }
                        else if (clientPoint.Y <= (this.Size.Height - resizeAreaSize)) //If the pointer is inside the form at the Y coordinate(discounting the resize area size)
                        {
                            if (clientPoint.X <= resizeAreaSize)//Resize horizontally to the left
                                m.Result = (IntPtr)HTLEFT;
                            else if (clientPoint.X > (this.Width - resizeAreaSize))//Resize horizontally to the right
                                m.Result = (IntPtr)HTRIGHT;
                        }
                        else
                        {
                            if (clientPoint.X <= resizeAreaSize)//Resize diagonally to the left
                                m.Result = (IntPtr)HTBOTTOMLEFT;
                            else if (clientPoint.X < (this.Size.Width - resizeAreaSize)) //Resize vertically down
                                m.Result = (IntPtr)HTBOTTOM;
                            else //Resize diagonally to the right
                                m.Result = (IntPtr)HTBOTTOMRIGHT;
                        }
                    }
                }
                return;
            }
            #endregion
            //Remove border and keep snap window
            if (m.Msg == WM_NCCALCSIZE && m.WParam.ToInt32() == 1)
            {
                return;
            }
            //Keep form size when it is minimized and restored. Since the form is resized because it takes into account the size of the title bar and borders.
            if (m.Msg == WM_SYSCOMMAND)
            {
                /// <see cref="https://docs.microsoft.com/en-us/windows/win32/menurc/wm-syscommand"/>
                /// Quote:
                /// In WM_SYSCOMMAND messages, the four low - order bits of the wParam parameter 
                /// are used internally by the system.To obtain the correct result when testing 
                /// the value of wParam, an application must combine the value 0xFFF0 with the 
                /// wParam value by using the bitwise AND operator.
                int wParam = (m.WParam.ToInt32() & 0xFFF0);
                if (wParam == SC_MINIMIZE)  //Before
                    formsize = this.ClientSize;
                if (wParam == SC_RESTORE)// Restored form(Before)
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
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, (IntPtr)0xf012, (IntPtr)0);
        }
    }
}
