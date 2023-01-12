using ContactsList.Interfaces;
using ContactsList.Models.AbstractModels;

namespace ContactsList.Models;

internal class KeyAccountManager : Employee, IKeyAccountManager
{
    public List<ICustomer> Customers { get; set; } = new List<ICustomer>();
    public new string Type { get; set; } = "KeyAccountManager";
}
