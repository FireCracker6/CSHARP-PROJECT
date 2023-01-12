using ContactsList.Interfaces;

namespace ContactsList.Models.AbstractModels;

internal class BaseEmployee : IBaseEmployee
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Type { get; set; } = null!;
}
