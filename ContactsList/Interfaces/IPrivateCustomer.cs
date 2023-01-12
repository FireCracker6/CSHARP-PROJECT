
namespace ContactsList.Interfaces;

internal interface IPrivateCustomer : ICustomer
{
    string FirstName { get; set; }
    string LastName { get; set; }
}
