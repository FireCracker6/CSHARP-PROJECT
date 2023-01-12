using ContactsList.Services;

var menu = new Menu();
menu.PopulateContactList();

while (true)
{
    Console.Clear();
    menu.WelcomeMenu();
}