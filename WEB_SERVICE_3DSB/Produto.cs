using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_SERVICE_3DSB
{
    public class Produto
    {
        //atributos
        public int id { get; set; }
        public string nome { get; set; }
        public double preco { get; set; }
        public int quantidade { get; set; }

        public Produto(int id, string nome, double preco,int quantidade)
        {
            this.id = id;
            this.nome = nome;
            this.preco = preco;
            this.quantidade = quantidade;
        }
        public Produto()
        {
        
        
        
        }








    }
}
