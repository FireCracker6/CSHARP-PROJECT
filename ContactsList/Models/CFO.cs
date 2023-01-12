using ContactsList.Interfaces;
using ContactsList.Models.AbstractModels;

namespace ContactsList.Models;

internal class CFO : Employee, ICFO
{
    public new string Type { get; set; } = "CFO";
}
