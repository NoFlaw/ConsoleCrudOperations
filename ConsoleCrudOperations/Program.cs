using System;

namespace ConsoleCrudOperations
{
    class Program
    {
        private static readonly PersonService _personService = new PersonService();

        static void Main(string[] args)
        {
            bool showMainMenu = true;

            while (showMainMenu)
            {
                showMainMenu = ShowMainMenu();
            }
        }
        private static bool ShowMainMenu()
        {
            Console.Clear();

            ShowCrudOperationsHeader();

            Console.WriteLine("Choose an option from below:\n");

            Console.WriteLine("1.) Add an entry");
            Console.WriteLine("2.) Find an entry");
            Console.WriteLine("3.) Remove an entry");
            Console.WriteLine("4.) Update an entry");
            Console.WriteLine("5.) View all entries");
            Console.WriteLine("6.) Exit");

            Console.Write("\r\nSelect an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    AddEntry();
                    return true;
                case "2":
                    FindEntry();
                    return true;
                case "3":
                    RemoveEntry();
                    return true;
                case "4":
                    UpdateEntry();
                    return true;
                case "5":
                    ViewAllEntries();
                    return true;
                case "6":
                    return false;
                default:
                    return true;
            }
        }

        private static void AddEntry()
        {
            Console.Clear();
            Console.WriteLine("===============");
            Console.WriteLine("-= ADD ENTRY =-");
            Console.WriteLine("===============");

            Console.Write("\r\nFirst Name: ");
            var firstName = Console.ReadLine();

            Console.Write("\r\nLast Name: ");
            var lastName = Console.ReadLine();

            var person = new Person
            {
                FirstName = firstName,
                LastName = lastName
            };

            var success = _personService.Add(person);

            string addMessage = string.Empty;

            Console.WriteLine($"You entered: {person.FirstName} {person.LastName}");

            if (success)
                addMessage = $"Your entry has been added and saved.";
            else
                addMessage = $"Your entry failed to be added and saved.";

            DisplayResult(addMessage);
        }

        private static void ViewAllEntries()
        {
            Console.Clear();
            Console.WriteLine("================");
            Console.WriteLine("-= ALL ENTRIES =-");
            Console.WriteLine("================");

            int counter = 1;

            var personList = _personService.GetAll();

            foreach (var person in personList)
            {
                Console.WriteLine($"{counter}.) ID: {person.Id,-3} FirstName: {person.FirstName,-10} LastName: {person.LastName,-10}");
                counter++;
            }

            string viewAllMessage = $"All existing entries have now been displayed.";

            DisplayResult(viewAllMessage);
        }

        private static void UpdateEntry()
        {
            Console.Clear();
            Console.WriteLine("==================");
            Console.WriteLine("-= UPDATE ENTRY =-");
            Console.WriteLine("==================");

            Console.Write("\r\nPlease enter the ID: ");

            var id = Convert.ToInt32(Console.ReadLine());
            var personToUpdate = _personService.Find(id);

            var previousFirstName = personToUpdate.FirstName;
            var previousLastName = personToUpdate.LastName;

            Console.Write("\r\nPlease enter the new FirstName: ");
            var newFirstName = Console.ReadLine();

            Console.Write("\r\nPlease enter the new LastName: ");
            var newLastName = Console.ReadLine();

            personToUpdate.FirstName = newFirstName;
            personToUpdate.LastName = newLastName;

            string updateMessage = string.Empty;

            var success = _personService.Update(personToUpdate);

            if (success)
            {
                Console.WriteLine($"OldFirstName: {previousFirstName}, NewFirstName: {newFirstName}");
                Console.WriteLine($"OldLastName: {previousLastName}, NewLastName: {newLastName}");
                updateMessage = $"Your entry has been updated and saved.";
                DisplayResult(updateMessage);
            }
            else
            {
                updateMessage = $"Your entry failed to be updated and saved.";
                DisplayResult(updateMessage);
            }
        }

        private static void RemoveEntry()
        {
            Console.Clear();
            Console.WriteLine("==================");
            Console.WriteLine("-= REMOVE ENTRY =-");
            Console.WriteLine("==================");

            Console.Write("\r\nPlease enter the ID: ");

            var id = Convert.ToInt32(Console.ReadLine());
            var person = _personService.Find(id);
            var success = _personService.Remove(person);

            string removeMessage = string.Empty;

            if (success)
            {
                Console.WriteLine($"Removed: ID: {person.Id,-3} FirstName: {person.FirstName,-10} LastName: {person.LastName,-10}");
                removeMessage = $"Your entry has been removed.";
                DisplayResult(removeMessage);
            }
            else
            {
                removeMessage = $"Your entry failed to be removed and saved.";
                DisplayResult(removeMessage);
            }
        }

        private static void FindEntry()
        {
            Console.Clear();
            Console.WriteLine("=================");
            Console.WriteLine("-= FIND ENTRY =-");
            Console.WriteLine("=================");

            Console.Write("\r\nPlease enter the ID: ");

            var id = Convert.ToInt32(Console.ReadLine());
            var person = _personService.Find(id);

            if (person != null)
            {
                Console.WriteLine($"Found: ID: {person.Id,-3} FirstName: {person.FirstName,-10} LastName: {person.LastName,-10}");
                DisplayResult($"Your entry has been found.");
            }
            else
            {
                DisplayResult($"Your entry could not be found using the following ID: {id}.");
            }
        }

        private static void ShowCrudOperationsHeader()
        {
            Console.WriteLine("=====================================");
            Console.WriteLine("------- Simple CRUD Operations ------");
            Console.WriteLine("Create     Read     Update     Delete");
            Console.WriteLine("=====================================\r\n");
        }

        private static void DisplayResult(string message)
        {
            Console.WriteLine($"\r\nStatus: {message}");
            Console.Write("\r\nPlease, press the Enter button to return to Main Menu.");
            Console.ReadLine();
        }
    }
}
