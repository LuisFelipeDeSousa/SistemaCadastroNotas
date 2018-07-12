using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroNotas
{
    public class alunosRepositorio
    {

        private string connectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\105411\Documents\CadastroNotas.mdf;Integrated Security=True;Connect Timeout=30";
        private SqlConnection connection = null;

        public alunosRepositorio()
        {
            connection = new SqlConnection(connectionString);
        }
        public int Inserir(Alunos alunos)
        {
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO alunos (nome, codigoMatricula, nota1, nota2, nota3, frequencia) OUTPUT INSERTED.ID VALUES (@NOME, @CODIGOMATRICULA, @NOTA1, @NOTA2, @NOTA3, @FREQUENCIA)";

            command.Parameters.AddWithValue("@NOME", alunos.Nome);
            command.Parameters.AddWithValue("@CODIGOMATRICULA", alunos.CodigoMatricula);
            command.Parameters.AddWithValue("@NOTA1", alunos.Nota1);
            command.Parameters.AddWithValue("@NOTA2", alunos.Nota2);
            command.Parameters.AddWithValue("@NOTA3", alunos.Nota3);
            command.Parameters.AddWithValue("@FREQUENCIA", alunos.Frequencia);
           
            
            

            int id = Convert.ToInt32(command.ExecuteScalar().ToString());
            connection.Close();
            return id;

        }

        public bool Alterar(Alunos alunos)
        {
            connection.Open();
            SqlCommand comando = new SqlCommand();
            comando.Connection = connection;
            comando.CommandText = @"UPDATE alunos SET 
nome = @NOME, 
codigoMatricula = @CODIGOMATRICULA,
nota1 = @NOTA1,
nota2 = @NOTA2,
nota3 = @NOTA3,
frequencia = @FREQUENCIA";
            comando.Parameters.AddWithValue("@NOME", alunos.Nome);
            comando.Parameters.AddWithValue("@CODIGOMATRICULA", alunos.CodigoMatricula);
            comando.Parameters.AddWithValue("@NOTA1", alunos.Nota1);           
            comando.Parameters.AddWithValue("@NOTA2", alunos.Nota2);
            comando.Parameters.AddWithValue("@NOTA3", alunos.Nota3);

            comando.Parameters.AddWithValue("@ID", alunos.Id);
            int quantidadeAlterada = comando.ExecuteNonQuery();
            connection.Close();
            return quantidadeAlterada == 1;
        }
        public List<Alunos> ObterTodos(string textoParaPesquisar = "%%", string colunaOrdenacao = "nome", string tipoOrdenacao = "ASC")
        {
            textoParaPesquisar = "%" + textoParaPesquisar + "%";
            List<Alunos> herois = new List<Alunos>();
            connection.Open();
            SqlCommand comando = new SqlCommand();
            comando.Connection = connection;

            comando.CommandText = @"SELECT 
id, nome, codigoMatricula, nota1, nota2, nota3, frequencia 
FROM alunos 
WHERE nome LIKE @PESQUISA OR codigoMatricula LIKE @PESQUISA
ORDER BY " + colunaOrdenacao + " " + tipoOrdenacao;
            comando.Parameters.AddWithValue("@PESQUISA", textoParaPesquisar);

            DataTable tabelaEmMemoria = new DataTable();
            tabelaEmMemoria.Load(comando.ExecuteReader());
            for (int i = 0; i < tabelaEmMemoria.Rows.Count; i++)
            {
                Alunos alunos = new Alunos();
                alunos.Id = Convert.ToInt32(tabelaEmMemoria.Rows[i][0].ToString());
                alunos.Nome = tabelaEmMemoria.Rows[i][1].ToString();
                alunos.CodigoMatricula = tabelaEmMemoria.Rows[i][2].ToString();
                alunos.Nota1 = Convert.ToDouble(tabelaEmMemoria.Rows[i][3].ToString());
                alunos.Nota2 = Convert.ToDouble(tabelaEmMemoria.Rows[i][3].ToString());
                alunos.Nota3 = Convert.ToDouble(tabelaEmMemoria.Rows[i][3].ToString());
                alunos.Add(alunos);
            }
            connection.Close();
            return herois;
        }
    }
}
