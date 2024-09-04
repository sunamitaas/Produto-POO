using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trabalhoPoo.Models
{
    internal class Produto
    {
        public int id_produto { get; set; }
        public string name_produto { get; set; }
        public string descricao_produto { get; set; }
        public decimal precovenda_produto { get; set; }
        public Categoria id_categoria_fk { get; set; }

    }
}
