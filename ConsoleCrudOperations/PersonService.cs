using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleCrudOperations
{
    public class PersonService
    {
        private readonly Random _random;

        private static readonly List<Person> _personList = new List<Person>()
        {
            new Person
            {
                Id = 1,
                FirstName = "Brenton",
                LastName = "Bates"                
            },
            new Person
            {
                Id = 2,
                FirstName = "Jalisse",
                LastName = "Adams"
            },
            new Person
            {
                Id = 3,
                FirstName = "Joe",
                LastName = "Fuentes"
            }
        };

        public PersonService()
        {
            _random = new Random();
        }

        public bool Add(Person person)
        {
            if (person == null)
                return false;

            if (person.Id <= 0)
                person.Id = _random.Next(_personList.Count() + 1, 1000);

            _personList.Add(person);

            return true;
        }

        public List<Person> GetAll()
        {
            return _personList;
        }

        public Person Find(int id)
        {
            if (id <= 0)
                return null;

            var person = _personList.FirstOrDefault(x => x.Id == id);

            if (person == null)
                return null;

            return person;
        }

        public bool Update(Person person)
        {
            if (person == null)
                return false;

            var personToUpdate = _personList.FirstOrDefault(x => x.Id == person.Id);

            if (personToUpdate == null)
                return false;

            personToUpdate.FirstName = person.FirstName;
            personToUpdate.LastName = person.LastName;            

            return true;
        }

        public bool Remove(Person person)
        {
            if (person == null)
                return false;

            if (person.Id <= 0)
                return false;

            var personToRemove = _personList.FirstOrDefault(x => x.Id == person.Id);

            if (personToRemove == null)
                return false;

            return _personList.Remove(personToRemove);
        }
    }
}
