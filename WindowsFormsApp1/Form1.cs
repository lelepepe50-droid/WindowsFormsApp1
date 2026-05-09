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
        
        const string DADOS_CONEXAO =
        "Server=localhost;user=root;password=;database=projeto";

        private void Telasvelculos_Load(object sender, EventArgs e)
        {

        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {

        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            string campoNome = txtnome.Text;
            string campoTelefone = cbtelefone.Text;
            string campoAno = dtpano.Text;

            DateTime dateConvertida = DateTime.Parse(campoAno);
                int controlelinhasAfetadas = 0;
            MessageBox.Show(
                $"nome: {campoNome} \n" +
                $"telofone: {campoTelefone}\n" +
                $"ano: {campoAno}\n" +
                $"data Convertida:{dateConvertida}");

            
            using (MySqlConnection conn = new MySqlConnection(DADOS_CONEXAO))
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

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(DADOS_CONEXAO))
            {
                conn.Open();
                string scriptConsultaIndividual = "SETECT * FROM  tb_cadastro WHERE id = 1";
                
                using (MySqlCommand comando = new MySqlCommand(scriptConsultaIndividual, conn))
                {
                   var dadosResultado = comando.ExecuteReader();

                   while (dadosResultado.Read())
                    {
                       // lbidResultado.text = dadosResultado["id"].ToString();
                        //lbNomeResultado.text = dadosResultado["nomeVendendor"].ToString();
                        //lbResultado.text = dadosResultado["telefone"].ToString();
                        //lbDataResultado.text = dadosResultado["ano"].ToString();
                    }
                   conn.Close();
                }
            }
        }

        private void btnConsultarLista_Click(object sender, EventArgs e)
        {
           
            using (MySqlConnection conn = new MySqlConnection(DADOS_CONEXAO))
            {
                conn.Open();

                string campoServico = txtnome.Text;
                string scriptConsultaLista = "";
                if (!string.IsNullOrEmpty(campoServico))
                {
                    scriptConsultaLista = $"SELECT * FROM tb_cadastro WHERE nomeVendendor LIKE '%{campoServico}%'";
                }
                else
                {
                    scriptConsultaLista = "SELECT * FROM tb_cadastro";
                }

                string scriptCosulta = "SELECT * FROM tb_cadastro";

                using (MySqlCommand comando = new MySqlCommand(scriptCosulta, conn))
                {
                    if (!string.IsNullOrEmpty(campoServico))
                    {
                       comando.Parameters.AddWithValue("@nomeVendendor", $"%{campoServico}%");
                    }
                    MySqlDataAdapter resultadoConsultaMySql = new MySqlDataAdapter(comando);

                    DataTable dt = new DataTable();

                    resultadoConsultaMySql.Fill(dt);

                    dgvCarro.DataSource = dt;



                }
               conn.Close();
            }
        }
    }
}
