using ContactsList.Interfaces;
using ContactsList.Models.AbstractModels;

namespace ContactsList.Models;

internal class Assistant : Employee, IAssistant
{
    public new string Type { get; set; } = "Assistant";
}
