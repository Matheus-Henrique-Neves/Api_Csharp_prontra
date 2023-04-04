using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_SERVICE_3DSB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
       static MySqlConnection conexao = new MySqlConnection(
                "Server =ESN509VMYSQL;Database=apimysql;User id=aluno;password=Senai1234");

        [HttpPost]
        //metodo para fazer um incert na tb produtos
        //O metodo precisa ser public , pode ter ou o mesmo nome do verbo http,
        // ou seja o post . so deve possuir um parametro.
        // a interfaçe IACTIONRESULT permite dar retornos mais elaborados
        //[FROMBOBY] indica onde virão os parameto nesse caso no body(corpo)
        //as requisição
        public IActionResult cadastrar([FromBody] Produto p)
        {
            
            //a pesquisa
            MySqlCommand comando = new MySqlCommand(
                "INSERT INTO produto VALUES (@id,@nome,@preco,@qtde)",conexao);
            comando.Parameters.AddWithValue("@id", p.id);
            comando.Parameters.AddWithValue("@nome", p.nome);
            comando.Parameters.AddWithValue("@preco", p.preco);
            comando.Parameters.AddWithValue("@qtde", p.quantidade);
            try{
                conexao.Open();
                if (comando.ExecuteNonQuery() !=0)
                {// ela retonara codigo 200(sucesso) e eviara um json com o a mensagem abaixo
                    return Ok(new { resut = "cadastrado" });
                }
                else
                {//retona 204(sem conteudo) caso no consiga cadastrar
                    return NoContent();

                }
            }catch(Exception)
            {
                throw;
            }
            finally
            {
                conexao.Close();

            }




        }

        [HttpPut]
        //metodo para fazer um incert na tb produtos
        //O metodo precisa ser public , pode ter ou o mesmo nome do verbo http,
        // ou seja o post . so deve possuir um parametro.
        // a interfaçe IACTIONRESULT permite dar retornos mais elaborados
        //[FROMBOBY] indica onde virão os parameto nesse caso no body(corpo)
        //as requisição
        public IActionResult atualizar([FromBody] Produto p)
        {

            //a pesquisa
            MySqlCommand comando = new MySqlCommand(
                "UPDATE produto SET nome=@nome,preco=@preco,quantidade=@quantidade,id=@id"+"WHERE id=@id", conexao);
            comando.Parameters.AddWithValue("@id", p.id);
            comando.Parameters.AddWithValue("@nome", p.nome);
            comando.Parameters.AddWithValue("@preco", p.preco);
            comando.Parameters.AddWithValue("@qtde", p.quantidade);
            try
            {
                conexao.Open();
                if (comando.ExecuteNonQuery() != 0)
                {// ela retonara codigo 200(sucesso) e eviara um json com o a mensagem abaixo
                    return Ok(new { resut = "ATUALIZADO" });
                }
                else
                {//retona 204(sem conteudo) caso no consiga cadastrar
                    return NoContent();

                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conexao.Close();

            }

        }
         [HttpGet]
        public List<Produto> Get()
        {
            MySqlCommand comando = new MySqlCommand(
                "SELECT * FROM produto", conexao);
            conexao.Open();
            List<Produto> listaFinal = new List<Produto>();
            MySqlDataReader reader = comando.ExecuteReader();
            while (reader.Read())
            {
                listaFinal.Add(new Produto(reader.GetInt32("id"), reader.GetString("nome"), reader.GetDouble("preco"), reader.GetInt32("quantidade")));
            }
            conexao.Close();
            return listaFinal;
        
        }

        [HttpGet]
        [Route("/api/[controller]/buscar")]
        public List<Produto> Get(int id)
        {
            MySqlCommand comando = new MySqlCommand(
                "SELECT * FROM produto where id = @id ", conexao);
            comando.Parameters.AddWithValue("@id", id);
            conexao.Open();
            List<Produto> listaFinal = new List<Produto>();
            MySqlDataReader reader = comando.ExecuteReader();
            while (reader.Read())
            {
                listaFinal.Add(new Produto(reader.GetInt32("id"), reader.GetString("nome"), reader.GetDouble("preco"), reader.GetInt32("quantidade")));
            }
            conexao.Close();
            return listaFinal;

        }
        [HttpDelete]
           public IActionResult remover(int id)
            {
            MySqlCommand comando = new MySqlCommand(
            "DELETE FROM produto WHERE id= @id", conexao);
            comando.Parameters.AddWithValue("@id", id);
            
                conexao.Open();
                if (comando.ExecuteNonQuery() != 0)
                {// ela retonara codigo 200(sucesso) e eviara um json com o a mensagem abaixo
                conexao.Close();
                return Ok(new { resut = "DELETADO" });
                }
                else
                {//retona 204(sem conteudo) caso no consiga deletar
                conexao.Close();
                return NoContent();

                }
           




            }



    }
}
