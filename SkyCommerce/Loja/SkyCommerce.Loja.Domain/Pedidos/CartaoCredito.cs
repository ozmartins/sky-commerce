using SkyCommerce.Loja.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SkyCommerce.Loja.Domain.Pedidos
{
    internal class CartaoCredito
    {
        private string _numero = string.Empty;
        private string _cvc = string.Empty;

        public string Numero 
        {
            get => TruncateCreditCard();
            set => _numero = value;
        }

        public string Nome { get; set; } = string.Empty;
        public string Mes { get; set; } = string.Empty;
        public string Ano { get; set; } = string.Empty;
        public string CodigoVerificador
        {
            get => _cvc.TruncateSensitiveInformation();
            set => _cvc = value;
        }

        private string TruncateCreditCard()
        {
            var firstDigits = _numero.Substring(0, 6);
            var lastDigits = _numero.Substring(_numero.Length - 4, 4);

            var requiredMask = new string('X', _numero.Length - firstDigits.Length - lastDigits.Length);

            var maskedString = string.Concat(firstDigits, requiredMask, lastDigits);
            var maskedCardNumberWithSpaces = Regex.Replace(maskedString, ".{4}", "$0 ");

            return maskedCardNumberWithSpaces;
        }
    }
}
