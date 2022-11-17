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
    public partial class Kasa : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=THEYORULMAZZ\\SQLEXPRESS;Initial Catalog=Kasa_Hareket_Takip;Integrated Security=True");
        DataTable dt = new DataTable();
        public Kasa()
        {
            InitializeComponent();
            listele();
        }

        private void listele()
        {
            conn.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("Select * from Kasa", conn);
            adtr.Fill(dt);
            dataGridView1.DataSource = dt;
            adtr.Dispose();
            conn.Close();
        }
        private void btngunsonu_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd1 = new SqlCommand();
            cmd1.Connection = conn;
            cmd1.CommandText = "INSERT INTO kasa(Tarih,Gelir,Gider,Bakiye)  SELECT GETDATE() , Sum(Gelir.Gelir_Tutarı) , Sum(Gider.Gider_Tutarı) , Sum(Gelir.Gelir_Tutarı)-Sum(Gider.Gider_Tutarı) FROM Gelir, Gider --GROUP BY Gelir.Tarih, Gider.Tarih-- --HAVING (((Gelir.Tarih)=GETDATE()) AND ((Gider.Tarih)=GETDATE()))--";
            cmd1.ExecuteNonQuery();
            dt.Clear();
            conn.Close();
            listele();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("KAYIT SİLİNSİNMİ ?", "SİSTEM", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
            {
                conn.Open();
                SqlCommand cmd3 = new SqlCommand();
                cmd3.Connection = conn;
                cmd3.CommandText = "DELETE FROM Kasa WHERE ID=@NUMARA";
                cmd3.Parameters.AddWithValue("@NUMARA", dataGridView1.CurrentRow.Cells[0].Value.ToString());
                cmd3.ExecuteNonQuery();
                dt.Clear();
                conn.Close();
                listele();
            }
        }
    }
}
