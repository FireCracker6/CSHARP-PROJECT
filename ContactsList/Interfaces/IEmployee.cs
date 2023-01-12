namespace ContactsList.Interfaces;

internal interface IEmployee : IBaseEmployee
{
   IBaseEmployee Manager { get; set; } 



}