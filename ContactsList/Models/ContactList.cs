using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactsList.Interfaces;
using ContactsList.Models.AbstractModels;

namespace ContactsList.Models
{
    internal class ContactList
    {
        public List<BaseEmployee> Employees { get; set; } = new List<BaseEmployee>();
        public List<Customer> Customers { get; set; } = new();
    }
}
