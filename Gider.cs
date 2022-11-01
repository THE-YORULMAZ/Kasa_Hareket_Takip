using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kasa_Hareket_Takip
{
    public partial class Gider : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=THEYORULMAZZ;Initial Catalog=Kasa_Hareket_Takip;Integrated Security=True");
        DataTable dt = new DataTable();

       
        
        public Gider()
        {
            InitializeComponent();
        }

        

        void listele()
        {
            txtfis.Text = "";
            txttarih.Text = "";
            txttip.Text = "";
            txttutar.Text = "";
            rtxtgider.Text = "";
            conn.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("Select * from Gider", conn);
            adtr.Fill(dt);
            dataGridView1.DataSource = dt;
            adtr.Dispose();
            conn.Close();
        }

        void kontrol()
        {
            txtfis.Text = txtfis.Text.Replace("'", "''");
            txttarih.Text = txttarih.Text.Replace("'", "''");
            txttip.Text = txttip.Text.Replace("'", "''");
            txttutar.Text = txttutar.Text.Replace("'", "''");
            rtxtgider.Text = rtxtgider.Text.Replace("'", "''");
        }

        
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Gider_Load(object sender, EventArgs e)
        {
           
            listele();
        }

        private void btnekle_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 2;
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (txtfisgider.Text == "")
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
                    cmd3.CommandText = "DELETE FROM Gider WHERE ID=@NUMARA";
                    cmd3.Parameters.AddWithValue("@NUMARA", dataGridView1.CurrentRow.Cells[0].Value.ToString());
                    cmd3.ExecuteNonQuery();
                    dt.Clear();
                    conn.Close();
                    listele();
                }
            }
           
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }

        private void btniptalg_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
            if (txtfis.Text.Trim() == "") errorProvider1.SetError(txtfis, "BOŞ GEÇİLEMEZ");
            if (txttarih.Text.Trim() == "") errorProvider1.SetError(txttarih, "BOŞ GEÇİLEMEZ");
            if (txttip.Text.Trim() == "") errorProvider1.SetError(txttip, "BOŞ GEÇİLEMEZ");
            if (txttutar.Text.Trim() == "") errorProvider1.SetError(txttutar, "BOŞ GEÇİLEMEZ");
            if (rtxtgider.Text.Trim() == "") errorProvider1.SetError(rtxtgider, "BOŞ GEÇİLEMEZ");

            else
            {
                kontrol();
                conn.Open();
                SqlCommand cmd1 = new SqlCommand();
                cmd1.Connection = conn;
                cmd1.CommandText = "INSERT INTO Gider(Fis_No,Tarih,Gider_Tipi,Gider_Tutarı,Aciklama)VALUES('" + txtfis.Text + "','" + txttarih.Text + "','" + txttip.Text + "','" + txttutar.Text + "','" + rtxtgider.Text + "')";

                cmd1.ExecuteNonQuery();
                dt.Clear();
                conn.Close();
                MessageBox.Show("GELİR KAYIT EDİLDİ", "SİSTEM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
            }
        }

       

        private void btnedit_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
            if (txtfisgider.Text == "")
            {
                MessageBox.Show("LÜTFEN LİSTEDEN BİR KAYIT SEÇİN", "SİSTEM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                kontrol();
                conn.Open();
                SqlCommand cmd2 = new SqlCommand();
                cmd2.Connection = conn;
                cmd2.CommandText = "UPDATE Gider SET Fis_No='" + txtfisgider.Text + "',Tarih='" + txttarihg.Text + "',Gider_Tipi='" + txtgtipg.Text + "',Gider_Tutarı='" + txtgtutg.Text + "',Aciklama='" + rtxtgunc.Text + "'where ID=@NUMARA";
                cmd2.Parameters.AddWithValue("@NUMARA", dataGridView1.CurrentRow.Cells[0].Value.ToString());
                cmd2.ExecuteNonQuery();
                dt.Clear();
                conn.Close();
                MessageBox.Show("GÜNCELLEME İŞLEMİ BAŞARILI", "SİSTEM", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            listele();
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            dt.Clear();
            conn.Open();
            SqlDataAdapter adtr2 = new SqlDataAdapter("Select *  From Gider where Fis_No Like'%" + txtsearch.Text + "%'", conn);
            adtr2.Fill(dt);
            dataGridView1.DataSource = dt;
            adtr2.Dispose();
            conn.Close();
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            dt.Clear();
            conn.Open();
            SqlDataAdapter adtr2 = new SqlDataAdapter("Select *  From Gider where Fis_No Like'%" + txtsearch.Text + "%'", conn);
            adtr2.Fill(dt);
            dataGridView1.DataSource = dt;
            adtr2.Dispose();
            conn.Close();
        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            txtfisgider.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txttarihg.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtgtipg.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtgtutg.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            rtxtgunc.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
        }
    }
}
