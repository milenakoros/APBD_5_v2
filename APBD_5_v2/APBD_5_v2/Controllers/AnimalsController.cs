using APBD_5_v2.Models;
using Microsoft.AspNetCore.Mvc;
using APBD_5_v2.Services;

namespace APBD_5_v2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private readonly IAnimalsService _animalsService;
        
        public AnimalsController(IAnimalsService animalsService)
        {
            _animalsService = animalsService;
        }
        
        [HttpGet]
        public IActionResult GetAnimals([FromQuery] string orderBy = "name")
        {
            // Check if orderBy parameter is valid, if not, default to "name"
            if (!IsValidOrderByParameter(orderBy))
            {
                orderBy = "name";
            }

            // Retrieve sorted animals from the service
            var animals = _animalsService.GetAnimals(orderBy);

            return Ok(animals);
        }
        private bool IsValidOrderByParameter(string orderBy)
        {
            var validOrderByParameters = new List<string> { "name", "description", "category", "area" };
            return validOrderByParameters.Contains(orderBy.ToLower());
        }
        
        
        [HttpGet("{id:int}")]
        public IActionResult GetAnimal(int id)
        {
            var animal = _animalsService.GetAnimal(id);

            if (animal == null)
            {
                return NotFound("Animal not found");
            }
            
            return Ok(animal);
        }
        
        [HttpPost]
        public IActionResult CreateAnimal(Animal animal)
        {
            var affectedCount = _animalsService.CreateAnimal(animal);
            return StatusCode(StatusCodes.Status201Created);
        }
        
        [HttpPut("{id:int}")]
        public IActionResult UpdateAnimal(int id, Animal animal)
        {
            var affectedCount = _animalsService.UpdateAnimal(animal);
            return NoContent();
        }
        
        [HttpDelete("{id:int}")]
        public IActionResult DeleteAnimal(int id)
        {
            var affectedCount = _animalsService.DeleteAnimal(id);
            return NoContent();
        }
    }
}