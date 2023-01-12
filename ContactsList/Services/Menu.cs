using System;
using ContactsList.Interfaces;
using ContactsList.Models;
using ContactsList.Models.AbstractModels;
using Newtonsoft.Json;

namespace ContactsList.Services;

internal class Menu
{
    private List<dynamic> contactList = new List<dynamic>();
    private readonly FileService file = new FileService();
    
    public void WelcomeMenu()
    {
        Console.Clear();    
        Console.WriteLine("Välkommen till Admin Applikationen");
        Console.WriteLine("1. Lägg till en ny Kund");
        Console.WriteLine("2. Lägg till en ny Anställd");
        Console.WriteLine("3. Visa alla kunder");
        Console.WriteLine("4. Visa alla anställda");
        Console.WriteLine("Ange ett av alternativen ovan: ");
        var option = Console.ReadLine();

        switch(option)
        {
            case "1": OptionOne(); break;
            case "2": OptionTwo(); break;
            case "3": OptionThree(); break;
            case "4": OptionFour(); break;
        }
    }

    private void OptionOne()
    {
        Console.Clear();
        Console.WriteLine("Lägg till en ny kund:");
        Console.WriteLine("1. Företagskund");
        Console.WriteLine("2. Privatekund");
        Console.Write("Välj vad för typ av kund du vill skapa: ");
        var option = Console.ReadLine();

        switch (option)
        {
            case "1": CreateBusinessCustomer(); break;
            case "2": CreatePrivateCustomer(); break;
        }

        file.Save(JsonConvert.SerializeObject(contactList));
    }

    private void CreateBusinessCustomer()
    {
        Console.Clear();
        Console.WriteLine("Lägg till en ny företagskund.");

        BusinessCustomer customer = new BusinessCustomer();
        Console.Write("Ange Organisationsnummer: ");
        customer.OrganizationNumber = Console.ReadLine() ?? "";
        Console.Write("Ange Företagsnamn: ");
        customer.CompanyName = Console.ReadLine() ?? "";
        Console.Write("Ange Företagsadress: ");
        customer.Address = Console.ReadLine() ?? "";
        Console.Write("Ange Telefonnummer: ");
        customer.Phone = Console.ReadLine() ?? "";
        Console.Write("Ange E-postadress: ");
        customer.Email = Console.ReadLine() ?? "";
        Console.Write("Ange Kontaktperson: ");
        customer.ContactPerson = Console.ReadLine() ?? "";

        contactList.Add(customer);
    }
    private void CreatePrivateCustomer()
    {
        Console.Clear();
        Console.WriteLine("Lägg till en ny privatkund.");

        PrivateCustomer customer = new PrivateCustomer();
        Console.Write("Ange Förnamn: ");
        customer.FirstName = Console.ReadLine() ?? "";
        Console.Write("Ange Efternamn: ");
        customer.LastName = Console.ReadLine() ?? "";
        Console.Write("Ange Adress: ");
        customer.Address = Console.ReadLine() ?? "";
        Console.Write("Ange Telefonnummer: ");
        customer.Phone = Console.ReadLine() ?? "";
        Console.Write("Ange E-postadress: ");
        customer.Email = Console.ReadLine() ?? "";

        contactList.Add(customer);
    }
    private void OptionTwo()
    {
        Console.Clear();
        Console.WriteLine("Lägg till en ny anställd:");
        Console.WriteLine("1. VD");
        Console.WriteLine("2. EkonomiChef");
        Console.WriteLine("3. Assistent");
        Console.WriteLine("4. Key Account Manager");
        Console.Write("Välj vad för typ av anställd du vill skapa: ");
        var option = Console.ReadLine();

        switch (option)
        {
            case "1": CreateCEO(); break;
            case "2": CreateCFO(); break;
            // case "3": CreateAssistant(); break;
            // case "4": CreateKeyAccountManager(); break;
        }
        file.Save(JsonConvert.SerializeObject(contactList));

    }

    private void CreateCEO()
    {
        Console.Clear();
        Console.WriteLine("Lägg till en ny VD.");

        CEO employee = new CEO();
        Console.Write("Ange Förnamn: ");
        employee.FirstName = Console.ReadLine() ?? "";
        Console.Write("Ange Efternamn: ");
        employee.LastName = Console.ReadLine() ?? "";
        Console.Write("Är personen en verklig huvudman? (y/n)");
        var answer = Console.ReadLine();
        employee.BenificalOwner = (answer?.ToLower() == "y");


        contactList.Add(employee);

    }
    private void CreateCFO()
    {
        Console.Clear();
        Console.WriteLine("Lägg till en ny EkonomiChef.");

        CFO employee = new CFO();

        Console.Write("Ange Förnamn: ");
        employee.FirstName = Console.ReadLine() ?? "";
        Console.Write("Ange Efternamn: ");
        employee.LastName = Console.ReadLine() ?? "";
        Console.Write("Ange e-postadressen på den anställdes chef: ");
        var email = Console.ReadLine();

       
        var manager = contactList.FirstOrDefault(x => x.Email == email);
        if (manager != null) 
            employee.Manager= manager;

        contactList.Add(employee);

    }

    private void OptionThree()
    {
        foreach(var contact in contactList)
        {
            switch (contact.Type)
            {
                case "BusinessCustomer":
                    var _businessCustomer = contact as BusinessCustomer;
                    Console.WriteLine("Företagskund: ");
                    Console.WriteLine("--------------");
                    Console.WriteLine($"Organisationsnummer: {_businessCustomer!.OrganizationNumber}");
                    Console.WriteLine($"Företagnamn: {_businessCustomer!.CompanyName}");
                    Console.WriteLine($"Kontaktperson: {_businessCustomer!.ContactPerson}");
                    Console.WriteLine("");
                    break;

                case "PrivateCustomer":
                    var _privateCustomer = contact as PrivateCustomer;
                    Console.WriteLine("Privatkund: ");
                    Console.WriteLine("--------------");
                    Console.WriteLine($"{_privateCustomer!.LastName}");
                    break;
            }            
        }

        Console.ReadKey();
    }

 
    private void OptionFour()
    {
        foreach (var contact in contactList)
        {
            switch (contact.Type)
            {
                case "CEO":
                    var _ceo = contact as CEO;
                    Console.WriteLine($"{_ceo!.BenificalOwner}");
                    break;

                case "CFO":
                    var _cfo = contact as CFO;
                    Console.WriteLine($"{_cfo!.FirstName}");
                    break;
            }
        }

        Console.ReadKey();
    }

    public void PopulateContactList()
    {
        file.FilePath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\contactlist.json";

        var result = file.Read();
        if (!string.IsNullOrEmpty(result))
        {
            dynamic contactList = JsonConvert.DeserializeObject<dynamic>(result)!;
        }
            
    }
}
