using ContactsList.Interfaces;
using ContactsList.Models.AbstractModels;

namespace ContactsList.Models;

internal class BusinessCustomer : Customer, IBusinessCustomer
{
    public string OrganizationNumber { get; set; } = null!;
    public string CompanyName { get; set; } = null!;
    public string ContactPerson { get; set; } = null!;
    public new string Type { get; set; } = "BusinessCustomer";
}
