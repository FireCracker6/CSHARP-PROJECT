namespace ContactsList.Interfaces;

internal interface IKeyAccountManager : IEmployee
{
    List<ICustomer> Customers { get; set; }

}
