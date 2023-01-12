using ContactsList.Interfaces;

namespace ContactsList.Models.AbstractModels
{
    internal class Employee : BaseEmployee, IEmployee
    {
       public IBaseEmployee Manager { get; set; } = null!;
    }
}
