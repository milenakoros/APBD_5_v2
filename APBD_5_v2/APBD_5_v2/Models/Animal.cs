using System.ComponentModel.DataAnnotations;
namespace APBD_5_v2.Models;


public class Animal
{
    public int IdAnimal { get; set; }
    [Required]
    [MaxLength(200)]
    public string Name { get; set; }
    [Required]
    [MaxLength(200)]
    public string Category { get; set; }
    [Required]
    [MaxLength(1000)]
    public string Description { get; set; }
    public string Area { get; set; }
}