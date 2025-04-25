using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WebApplication2.Models;
using WebApplication2.Models.DTOs;

namespace WebApplication2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnimalController : ControllerBase
{
    private readonly IConfiguration _configuration;
    
    public AnimalController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet]
    public IActionResult GetAnimals()
    {
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        connection.Open();
        using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "SELECT * FROM Animal;";

        var reader = command.ExecuteReader(); // kursor
        var animlas = new List<Animal>();
        int idAnimalOrdinal = reader.GetOrdinal("IdAnimal");
        int nameOrdinal = reader.GetOrdinal("Name");

        while (reader.Read())
        {  
            animlas.Add(new Animal()
            {
                IdAnimal = reader.GetInt32(idAnimalOrdinal),
                Name = reader.GetString(nameOrdinal)
            });
        }
        
        return Ok(animlas);
    }

    [HttpPost]
    public IActionResult AddAnimal(AddAnimal animal)
    {
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        connection.Open();
        using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "INSERT INTO Animal VALUES(@animalName, '','','')";
        command.Parameters.AddWithValue("@animalName", animal.Name);

        command.ExecuteNonQuery();

        return Ok("Iserted");
    }
}