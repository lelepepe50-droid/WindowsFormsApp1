using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySqlConnector;

namespace WindowsFormsApp1
{
    public partial class Telasvelculos : Form
    {
        public Telasvelculos()
        {
            InitializeComponent();
        }

        private void Telasvelculos_Load(object sender, EventArgs e)
        {

        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {

        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            string campoNome = txtnome.Text;
            int controlelinhasAfetadas = 0;

            string dadosConexao =
             "Server=localhost;user=root;password=;database=projeto";
            using (MySqlConnection conn = new MySqlConnection(dadosConexao) )
            {
                conn.Open();
                string scriptInsert = "Insert INTO tb_projetos (nomeVededor) VALUE (@nomeVededor)";

                using (MySqlCommand comando = new MySqlCommand(scriptInsert,conn))
                {
                    comando.Parameters.AddWithValue("@nomeVededor", campoNome);
                    controlelinhasAfetadas = comando.ExecuteNonQuery();
                }
                conn.Close();
            }
             //MysqlConnection

            if (controlelinhasAfetadas > 0)
            {
                MessageBox.Show("Dados salvos com Sucesso!");
            }
            else
            {
                MessageBox.Show("Ops. Algo deu errado!!!");
            }
        }
    }
}
