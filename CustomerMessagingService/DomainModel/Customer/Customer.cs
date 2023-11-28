using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Customer
{
    public class Customer
    {
        /// <summary>
        /// Primary Key of a Customer
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of a Customer
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Email address of a customer
        /// </summary>
        public string Email { get; set; }

    }
}
