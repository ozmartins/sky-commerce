using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyCommerce.Loja.Domain.Avaliacoes
{
    public class Avaliacao
    {
        public string Usuario { get; set; }
        public DateTime DataAvaliacao { get; set; } = DateTime.Now;
        public string Comentario { get; set; }
        public int Nota{ get; set; }
        public string ProdutoUrl { get; set; }
        public string Titulo { get; set; }
        public string Imagem { get; set; }
    }
}
