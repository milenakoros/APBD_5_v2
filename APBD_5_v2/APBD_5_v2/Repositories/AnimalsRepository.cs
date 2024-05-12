using System.Data.SqlClient;
using APBD_5_v2.Models;

namespace APBD_5_v2.Repositories;

public class AnimalsRepository : IAnimalsRepository
{
    private IConfiguration _configuration;

    public AnimalsRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IEnumerable<Animal> GetAnimals(string orderBy = "name")
    { 
        string[] allowedColumns = { "name", "description", "category", "area" };
        if (!allowedColumns.Contains(orderBy.ToLower()))
        {
            orderBy = "name";
        }

        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        con.Open();
    
        using var cmd = new SqlCommand();
        cmd.Connection = con;
    
        cmd.CommandText = $"SELECT Id, Name, Category, Description, Area FROM Animal ORDER BY {orderBy}";

        var dr = cmd.ExecuteReader();
        var animals = new List<Animal>();
        while (dr.Read())
        {
            var an = new Animal
            {
                IdAnimal = (int)dr["Id"],
                Name = dr["Name"].ToString(),
                Category = dr["Category"].ToString(),
                Description = dr["Description"].ToString(),
                Area = dr["Area"].ToString(),
            };
            animals.Add(an);
        }
        return animals;
    }

public Animal GetAnimal(int idAnimal)
{
    using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
    con.Open();
    
    using var cmd = new SqlCommand();
    cmd.Connection = con;
    cmd.CommandText = "SELECT Id, Name, Category, Description, Area FROM Animal WHERE Id = @IdAnimal";
    cmd.Parameters.AddWithValue("@IdAnimal", idAnimal);
    
    var dr = cmd.ExecuteReader();
    
    if (!dr.Read()) return null;
    
    var animal = new Animal()
    {
        IdAnimal = (int)dr["Id"],
        Name = dr["Name"].ToString(),
        Category = dr["Category"].ToString(),
        Description = dr["Description"].ToString(),
        Area = dr["Area"].ToString(),
    };
    
    return animal;
}

public int CreateAnimal(Animal animal)
{
    using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
    con.Open();

    using var cmd = new SqlCommand();
    cmd.Connection = con;
    cmd.CommandText = "INSERT INTO Animal(Name, Category, Description, Area) VALUES(@Name, @Category, @Description, @Area)";
    cmd.Parameters.AddWithValue("@Name", animal.Name);
    cmd.Parameters.AddWithValue("@Category", animal.Category);
    cmd.Parameters.AddWithValue("@Description", animal.Description);
    cmd.Parameters.AddWithValue("@Area", animal.Area);

    var affectedCount = cmd.ExecuteNonQuery();
    return affectedCount;
}

public int DeleteAnimal(int idAnimal)
{
    using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
    con.Open();

    using var cmd = new SqlCommand();
    cmd.Connection = con;
    cmd.CommandText = "DELETE FROM Animal WHERE Id = @IdAnimal";
    cmd.Parameters.AddWithValue("@IdAnimal", idAnimal);

    var affectedCount = cmd.ExecuteNonQuery();
    return affectedCount;
}

public int UpdateAnimal(Animal animal)
{
    using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
    con.Open();

    using var cmd = new SqlCommand();
    cmd.Connection = con;
    cmd.CommandText = "UPDATE Animal SET Name=@Name, Category=@Category, Description=@Description, Area=@Area WHERE Id = @IdAnimal";
    cmd.Parameters.AddWithValue("@IdAnimal", animal.IdAnimal);
    cmd.Parameters.AddWithValue("@Name", animal.Name);
    cmd.Parameters.AddWithValue("@Category", animal.Category);
    cmd.Parameters.AddWithValue("@Description", animal.Description);
    cmd.Parameters.AddWithValue("@Area", animal.Area);

    var affectedCount = cmd.ExecuteNonQuery();
    return affectedCount;
}


    
}