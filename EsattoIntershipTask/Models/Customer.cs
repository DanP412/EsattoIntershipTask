using System;

namespace EsattoIntershipTask.Models
{
    public class Customer
    {
        public DateTime CreationDate { get; }

        public string Name { get; set; }

        public int VatId{ get; set; }

        public Adress Adress { get; set; }

        public Customer()
        {
            CreationDate = DateTime.Now;
        }
    }
  
}
