using System;
using System.Collections.Generic;
using System.Linq;

public class PersonDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
}

public class PersonService
{
    private List<PersonDTO> people = new List<PersonDTO>();

    // Add a new person to the list
    public void AddPerson(PersonDTO person)
    {
        people.Add(person);
    }

    // Remove a person from the list by Id
    public bool RemovePerson(int id)
    {
        var person = people.FirstOrDefault(p => p.Id == id);
        if (person != null)
        {
            people.Remove(person);
            return true;
        }
        return false;
    }

    // Get people by a specific age
    public List<PersonDTO> GetPeopleByAge(int age)
    {
        return people.Where(p => p.Age == age).ToList();
    }

    // Get all people
    public List<PersonDTO> GetAllPeople()
    {
        return people.ToList();
    }
}

class Program
{
    static void Main()
    {
        PersonService service = new PersonService();
        
        // Add some people
        service.AddPerson(new PersonDTO { Id = 1, Name = "Alice", Age = 30 });
        service.AddPerson(new PersonDTO { Id = 2, Name = "Bob", Age = 25 });
        service.AddPerson(new PersonDTO { Id = 3, Name = "Charlie", Age = 30 });

        // Remove a person
        service.RemovePerson(2);

        // Get people by age
        var peopleAge30 = service.GetPeopleByAge(30);
        Console.WriteLine("People aged 30:");
        foreach (var person in peopleAge30)
        {
            Console.WriteLine($"{person.Name}, {person.Age}");
        }
		Console.WriteLine();
        // Get all people
        var allPeople = service.GetAllPeople();
        Console.WriteLine("All people:");
        foreach (var person in allPeople)
        {
            Console.WriteLine($"{person.Name}, {person.Age}");
        }
    }
}