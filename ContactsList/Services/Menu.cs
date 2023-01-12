using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Security.Cryptography.X509Certificates;
using ContactsList.Interfaces;
using ContactsList.Models;
using ContactsList.Models.AbstractModels;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace ContactsList.Services;

internal class Menu
{
    private List<dynamic> contactList = new List<dynamic>();
    private readonly FileService file = new FileService();
    
    public void WelcomeMenu()
    {
        Console.Clear();
        Console.WriteLine("\n-----------------------------------");
        Console.WriteLine("Välkommen till Admin Applikationen");
        Console.WriteLine("-----------------------------------");
        Console.WriteLine("");
        Console.WriteLine("1. Lägg till en ny Kund");
        Console.WriteLine("2. Lägg till en ny Anställd");
        Console.WriteLine("3. Visa alla kunder");
        Console.WriteLine("4. Visa alla anställda");
        Console.WriteLine("5. Ta bort en kund");
        Console.WriteLine("6. Ta bort en anställd");
        Console.WriteLine("7. Sök efter en anställd, kund, eller företagskund\n");
        Console.WriteLine("Ange ett av alternativen ovan: ");
  
        var option = Console.ReadLine();

        switch(option)
        {
            case "1": OptionOne(); break;
            case "2": OptionTwo(); break;
            case "3": OptionThree(); break;
            case "4": OptionFour(); break;
            case "5": OptionFive(); break;
            case "6": OptionSix(); break;
            case "7": OptionSeven(); break;
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
            case "3": CreateAssistant(); break;
            case "4": CreateKeyAccountManager(); break;
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

    private void CreateAssistant()
    {
        Console.Clear();
        Console.WriteLine("Lägg till en ny Assistent: ");

        Assistant assistant = new Assistant();
        Console.Write("Ange Förnamn: ");
        assistant.FirstName = Console.ReadLine() ?? "";
        Console.Write("Ange Efternamn: ");
        assistant.LastName = Console.ReadLine() ?? "";
        Console.Write("Ange E-postadress: ");
        assistant.Email = Console.ReadLine() ?? "";

        contactList.Add(assistant);
    }

    private void CreateKeyAccountManager()
    {
        Console.Clear();
        Console.WriteLine("Lägg till en ny Key Account Manager: ");

        KeyAccountManager ka = new KeyAccountManager();
        Console.Write("Ange Förnamn: ");
        ka.FirstName = Console.ReadLine() ?? "";
        Console.Write("Ange Efternamn: ");
        ka.LastName = Console.ReadLine() ?? "";
        Console.Write("Ange E-postadress: ");
        ka.Email = Console.ReadLine() ?? "";

        // Hur får man fram lista med kunder??

        contactList.Add(ka);
        
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
                    Console.WriteLine($" Förnamn: {_privateCustomer!.FirstName}, Efternamn:  {_privateCustomer!.LastName} E-postadress: {_privateCustomer.Email}");
                    Console.WriteLine("");
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
                    Console.WriteLine("VD: ");
                    Console.WriteLine("---------------");
                    Console.WriteLine($"Förnamn: {_ceo!.FirstName}, Efternamn: {_ceo!.LastName}, Verklig huvudman?  {_ceo!.BenificalOwner}");
                    Console.WriteLine("");
                    break;

                case "CFO":
                    var _cfo = contact as CFO;
                    Console.WriteLine("EkonomiChef: ");
                    Console.WriteLine("---------------");
                    Console.WriteLine($"Förnamn: {_cfo!.FirstName}, Efternamn: {_cfo.LastName}, E-postadress: {_cfo.Email}");
                    Console.WriteLine("");
                    break;

                case "Assistant":
                    var _assistant = contact as Assistant;
                    Console.WriteLine("Assistent(er): ");
                    Console.WriteLine("---------------");
                    Console.WriteLine($"Förnamn: {_assistant!.FirstName} Efternamn: {_assistant.LastName} E-post: {_assistant.Email}");
                    Console.WriteLine("");
                    break;
                case "KeyAccountManager":
                    var _ka = contact as KeyAccountManager;
                    Console.WriteLine("Key Account Manager: ");
                    Console.WriteLine("---------------");
                    Console.WriteLine($"Förnamn: {_ka!.FirstName} Efternamn: {_ka.LastName} E-post: {_ka.Email}");
                    Console.WriteLine("");
                    break;
            }
        }

        Console.ReadKey();


    }

    private void OptionFive()
    {

        Console.WriteLine("All customers are listed below: ");
        foreach (var contact in contactList)
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
                    Console.WriteLine($" Förnamn: {_privateCustomer!.FirstName}, Efternamn:  {_privateCustomer!.LastName} E-postadress: {_privateCustomer.Email}");
                    Console.WriteLine("");
                    break;
            }
        }


        Console.WriteLine("To remove a customer please enter a first name: ");
        var removeCustomer = Console.ReadLine() ?? "";

        var index = contactList.FindIndex(x => x.FirstName == removeCustomer);
        if (removeCustomer != "")
        {
            if (index >= 0)
            {
                Console.WriteLine($"Are you sure you want to delete{removeCustomer} ? (Y/N)");
                Console.WriteLine("");
                if (Console.ReadKey().Key == ConsoleKey.Y)
                {
                    contactList.RemoveAt(index);
                    file.Save(JsonConvert.SerializeObject(contactList));
                    Console.WriteLine($"\n{removeCustomer} was successfully removed from the list!");
                    Console.ReadKey();
                }
            }
            else
                Console.WriteLine("No such person in the list");
        }
        else
            Console.WriteLine("could not find that person! please try again.");
        Console.ReadKey();

    }

    private void OptionSix()
    {

        foreach (var contact in contactList)
        {

            Console.WriteLine("All current employees are listed below: ");
            switch (contact.Type)
            {
                case "CEO":
                    var _ceo = contact as CEO;
                    Console.WriteLine("VD: ");
                    Console.WriteLine("---------------");
                    Console.WriteLine($"Förnamn: {_ceo!.FirstName}, Efternamn: {_ceo!.LastName}, Verklig huvudman?  {_ceo!.BenificalOwner}");
                    Console.WriteLine("");


                    break;

                case "CFO":
                    var _cfo = contact as CFO;
                    Console.WriteLine("EkonomiChef: ");
                    Console.WriteLine("---------------");
                    Console.WriteLine($"Förnamn: {_cfo!.FirstName}, Efternamn: {_cfo.LastName}, E-postadress: {_cfo.Email}");
                    Console.WriteLine("");
                    break;

                case "Assistant":
                    var _assistant = contact as Assistant;
                    Console.WriteLine("Assistent(er): ");
                    Console.WriteLine("---------------");
                    Console.WriteLine($"Förnamn: {_assistant!.FirstName} Efternamn: {_assistant.LastName} E-post: {_assistant.Email}");
                    Console.WriteLine("");
                    break;
                case "KeyAccountManager":
                    var _ka = contact as KeyAccountManager;
                    Console.WriteLine("Key Account Manager: ");
                    Console.WriteLine("---------------");
                    Console.WriteLine($"Förnamn: {_ka!.FirstName} Efternamn: {_ka.LastName} E-post: {_ka.Email}");
                    Console.WriteLine("");
                    break;

                  
            }


        }


        Console.ReadKey();
        Console.WriteLine("To remove an employee please enter a first name: ");
       
        var removePerson = Console.ReadLine() ?? "";

            var index = contactList.FindIndex(x => x.FirstName == removePerson);
        if (removePerson != "")
        {
            if (index >= 0)
            {
                Console.WriteLine($"Are you sure you want to delete{removePerson} ? (Y/N)\n");
                Console.WriteLine("");
                if (Console.ReadKey().Key == ConsoleKey.Y) {
                contactList.RemoveAt(index);
                file.Save(JsonConvert.SerializeObject(contactList));
                Console.WriteLine($"{removePerson} was successfully removed from the list!");
                    Console.ReadKey();
                }
            }
            else
                Console.WriteLine("No such person in the list");
        }
        else
            Console.WriteLine("could not find that person! please try again.");
        Console.ReadKey();
    }

    private void OptionSeven()
    {
        Console.WriteLine("Sök efter en anställd eller en kund: ");
        Console.WriteLine("Sök på förnamn, eller företagsnamn: ");
        var seekPerson = Console.ReadLine() ?? "";

        if (seekPerson != "" || seekPerson != null) { 
            foreach (var contact in contactList)
            {

                switch (contact.Type)
                {
                    case "CEO":
                        CEO _ceo = contactList.FirstOrDefault(x => x.FirstName.ToLower() == seekPerson.ToLower())!;
                        Console.WriteLine($"VD: Förnamn: {_ceo.FirstName}, Efternamn: {_ceo.LastName}");
                        Console.ReadKey();
                        break;
                    case "CFO":
                        CFO _cfo = contactList.FirstOrDefault(x => x.FirstName.ToLower() == seekPerson.ToLower())!;
                        Console.WriteLine($"Ekonomichef: Förnamn: {_cfo.FirstName}, Efternamn: {_cfo.LastName}");
                        Console.ReadKey();
                        break;
                    case "Assistant":
                        Assistant _assistant = contactList.FirstOrDefault(x => x.FirstName.ToLower() == seekPerson.ToLower())!;
                        Console.WriteLine($"Assistent: Förnamn: {_assistant.FirstName}, Efternamn: {_assistant.LastName}");
                        Console.ReadKey();
                        break;
                    case "KeyAccountManager":
                        KeyAccountManager _ka = contactList.FirstOrDefault(x => x.FirstName.ToLower() == seekPerson.ToLower())!;
                        Console.WriteLine($"Key Account Manager: Förnamn: {_ka.FirstName}, Efternamn: {_ka.LastName}");
                        Console.ReadKey();
                        break;
                    //case "BusinessCustomer":
                    //    BusinessCustomer _bc = contactList.FirstOrDefault(x => x.CompanyName.ToLower() == seekPerson.ToLower())!;
                    //    Console.WriteLine($"Företagsnamn: {_bc.CompanyName}, Kontaktperson: {_bc.ContactPerson}");
                    //    Console.ReadKey();
                    //    break;
                    case "PrivateCustomer":
                        PrivateCustomer _pc = contactList.FirstOrDefault(x => x.FirstName.ToLower() == seekPerson.ToLower())!;
                        Console.WriteLine($"Privatkund: Förnamn: {_pc.FirstName}, Efternamn: {_pc.LastName}");
                        Console.ReadKey();
                        break;
                }
            }
            Console.ReadKey();
        }
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
