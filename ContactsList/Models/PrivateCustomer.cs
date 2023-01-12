using ContactsList.Interfaces;
using ContactsList.Models.AbstractModels;

namespace ContactsList.Models;

internal class PrivateCustomer : Customer, IPrivateCustomer
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public new string Type { get; set; } = "PrivateCustomer";
}
