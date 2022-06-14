using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroPersonas.Clases
{
    public class Persona
    {
        public int Id { get; set; }

        public String FullName { get; set; }

        public String RowGuid { get; set; }

        public String PersonType { get; set; }

        public int NameStyle { get; set; }

        public String Title { get; set; }

        public String FirstName { get; set; }

        public String MiddleName { get; set; }

        public String LastName { get; set; }

        public String EmailAddress { get; set; }    

        public int IdEmailAddress { get; set; }

        public String PhoneNumber { get; set; }

        public int PhoneNumberType { get; set; }

        public int IdCreditCard { get; set; }

        public String CardType { get; set; }

        public String CardNumber { get; set; }

        public int ExpMonth { get; set; }

        public int ExpYear { get; set; }

        public int EmailPromotion { get; set; }
    }
}
