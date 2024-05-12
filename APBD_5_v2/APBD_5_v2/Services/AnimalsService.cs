using APBD_5_v2.Models;
using APBD_5_v2.Repositories;

namespace APBD_5_v2.Services
{
    public class AnimalsService : IAnimalsService
    {
        private readonly IAnimalsRepository _animalsRepository;
        
        public AnimalsService(IAnimalsRepository animalsRepository)
        {
            _animalsRepository = animalsRepository;
        }
        
        public IEnumerable<Animal> GetAnimals(string orderBy)
        {
            // Business logic, if any
            return _animalsRepository.GetAnimals(orderBy);
        }
        
        public int CreateAnimal(Animal animal)
        {
            // Business logic, if any
            return _animalsRepository.CreateAnimal(animal);
        }

        public Animal? GetAnimal(int idAnimal)
        {
            // Business logic, if any
            return _animalsRepository.GetAnimal(idAnimal);
        }

        public int UpdateAnimal(Animal animal)
        {
            // Business logic, if any
            return _animalsRepository.UpdateAnimal(animal);
        }

        public int DeleteAnimal(int idAnimal)
        {
            // Business logic, if any
            return _animalsRepository.DeleteAnimal(idAnimal);
        }
        
    }
}
