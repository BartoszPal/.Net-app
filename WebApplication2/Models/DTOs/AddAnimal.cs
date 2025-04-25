using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models.DTOs;

public class AddAnimal
{
    [Required]
    [MinLength(5)]
    [MaxLength(200)]
    public string Name { get; set; }
}