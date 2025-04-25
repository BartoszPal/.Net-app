using WebApplication2.Models;

namespace WebApplication2.Repositories;

public interface IAnimalRepository
{
    IEnumerable<Animal> GetAnimals();
    void AddAnimal();
}