using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Globalization;

namespace Kasa_Hareket_Takip
{
    public partial class Gelir : Form
    {

        SqlConnection conn = new SqlConnection("Data Source=THEYORULMAZZ\\SQLEXPRESS;Initial Catalog=Kasa_Hareket_Takip;Integrated Security=True");
        DataTable dt = new DataTable();
        
        public Gelir()
        {
            InitializeComponent();
        }

        void listele()
        {
            txtfis.Text = "";           
            txttip.Text = "";
            txttutar.Text = "";
            rtxtgelir.Text = "";
            conn.Open();
            SqlDataAdapter adtr=new SqlDataAdapter("Select * from Gelir",conn);
            adtr.Fill(dt);
            dataGridView1.DataSource= dt;
            adtr.Dispose();
            conn.Close();
        }

        void kontrol()
        {
            txtfis.Text = txtfis.Text.Replace("'", "''");
            txttip.Text = txttip.Text.Replace("'", "''");
            txttutar.Text = txttutar.Text.Replace("'", "''");
            rtxtgelir.Text = rtxtgelir.Text.Replace("'", "''");
        }

        
       
        private void Gelir_Load(object sender, EventArgs e)
        {
            
            listele();
        }

       
        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnekle_Click(object sender, EventArgs e)
        {
           tabControl1.SelectedIndex = 1;

        }
        
        

        private void btnguncelle_Click(object sender, EventArgs e)
        {
           
            if (txtfisg.Text == "")
            {
                MessageBox.Show("LÜTFEN LİSTEDEN BİR KAYIT SEÇİN", "SİSTEM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                tabControl1.SelectedIndex = 2;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }

        private void btniptalg_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            
            if (txtfis.Text.Trim() == "") errorProvider1.SetError(txtfis, "BOŞ GEÇİLEMEZ");
            if (txttip.Text.Trim() == "") errorProvider1.SetError(txttip, "BOŞ GEÇİLEMEZ");
            if (txttutar.Text.Trim() == "") errorProvider1.SetError(txttutar, "BOŞ GEÇİLEMEZ");
            if (rtxtgelir.Text.Trim() == "") errorProvider1.SetError(rtxtgelir, "BOŞ GEÇİLEMEZ");

            else
            {
                kontrol();
                conn.Open();
                SqlCommand cmd1 = new SqlCommand();
                cmd1.Connection = conn;
                cmd1.CommandText = "INSERT INTO Gelir(Fis_No,Tarih,Gelir_Tipi,Gelir_Tutarı,Aciklama)VALUES('" + txtfis.Text + "','" + dateTimePicker1.Value.ToString("d-M-yyyy") + "','" + txttip.Text + "','" + txttutar.Text + "','" + rtxtgelir.Text + "')";

                cmd1.ExecuteNonQuery();
                dt.Clear();
                conn.Close();
                MessageBox.Show("GELİR KAYIT EDİLDİ", "SİSTEM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tabControl1.SelectedIndex = 0;
                listele();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //DateTime dt = DateTime.ParseExact(dataGridView1.CurrentCell.Value.ToString(), "d-M-yyyy", CultureInfo.InvariantCulture);
            //dateTimePicker2.Value = dt;
            dateTimePicker2.Text = dataGridView1.Rows[e.RowIndex].Cells["Tarih"].Value.ToString();
            txtfisg.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();   
            txtgtipg.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtgtutg.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            rtxtgunc.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString(); 
        }

        private void btnedit_Click(object sender, EventArgs e)
        {
                kontrol();
                conn.Open();
                SqlCommand cmd2 = new SqlCommand();
                cmd2.Connection = conn;
                cmd2.CommandText = "UPDATE Gelir SET Fis_No='" + txtfisg.Text + "',Tarih='" + dateTimePicker1.Value.ToString("d-M-yyyy") + "',Gelir_Tipi='" + txtgtipg.Text + "',Gelir_Tutarı='" + txtgtutg.Text + "',Aciklama='" + rtxtgunc.Text + "'where ID=@NUMARA";
                cmd2.Parameters.AddWithValue("@NUMARA", dataGridView1.CurrentRow.Cells[0].Value.ToString());
                cmd2.ExecuteNonQuery();
                dt.Clear();
                conn.Close();
                MessageBox.Show("GÜNCELLEME İŞLEMİ BAŞARILI", "SİSTEM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tabControl1.SelectedIndex = 0;
                listele();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (txtfisg.Text=="")
            {
                MessageBox.Show("LÜTFEN LİSTEDEN BİR KAYIT SEÇİN", "SİSTEM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (MessageBox.Show("KAYIT SİLİNSİNMİ ?", "SİSTEM", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                {
                    conn.Open();
                    SqlCommand cmd3 = new SqlCommand();
                    cmd3.Connection = conn;
                    cmd3.CommandText = "DELETE FROM Gelir WHERE ID=@NUMARA";
                    cmd3.Parameters.AddWithValue("@NUMARA", dataGridView1.CurrentRow.Cells[0].Value.ToString());
                    cmd3.ExecuteNonQuery();
                    dt.Clear();
                    conn.Close();
                    listele();
                }
            }
            
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            dt.Clear();
            conn.Open();
            SqlDataAdapter adtr2 = new SqlDataAdapter("Select *  From Gelir where Fis_No Like'%" + txtsearch.Text + "%'", conn);
            adtr2.Fill(dt);
            dataGridView1.DataSource = dt;
            adtr2.Dispose();
            conn.Close();
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            dt.Clear();
            conn.Open();
            SqlDataAdapter adtr2 = new SqlDataAdapter("Select *  From Gelir where Fis_No Like'%" + txtsearch.Text + "%'", conn);
            adtr2.Fill(dt);
            dataGridView1.DataSource = dt;
            adtr2.Dispose();
            conn.Close();
        }

        private void txttarih_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void txttip_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void btnglsn_Click(object sender, EventArgs e)
        {

        }
    }
}
