using ContactsList.Interfaces;
using ContactsList.Models.AbstractModels;

namespace ContactsList.Models
{
    internal class CEO : BaseEmployee, ICEO
    {
        public bool BenificalOwner { get; set; } = true;
        public new string Type { get; set; } = "CEO";
    }
}
