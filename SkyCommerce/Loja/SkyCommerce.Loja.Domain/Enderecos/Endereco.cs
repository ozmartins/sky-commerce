using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyCommerce.Loja.Domain.Enderecos
{
    internal class Endereco
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Cep { get; set; }
        public string Referencia { get; set; }
        public string NomeEndereco { get; set; }
        public TipoEndereco TipoEndereco { get; set; }
    }
}
