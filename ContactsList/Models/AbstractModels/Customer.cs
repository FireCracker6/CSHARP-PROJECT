using ContactsList.Interfaces;

namespace ContactsList.Models.AbstractModels;

internal class Customer : ICustomer
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string Type { get; set; } = null!;

}
