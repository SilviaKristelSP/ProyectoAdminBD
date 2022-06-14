using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroPersonas.Clases
{
    internal class CreditCard
    {
        public int Id { get; set; }

        public int BusinessEntityID { get; set; }

        public string CardType { get; set; }

        public string CardNumber { get; set; }

        public int ExpMonth { get; set; }

        public int ExpYear { get; set; }

    }
}
