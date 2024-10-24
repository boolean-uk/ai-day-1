using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<CarRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Car API", Version = "v1" });
});

// Configure CORS to allow any origin
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Use the CORS policy
app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Car API v1"));
}

app.MapGet("/cars", (CarRepository repo) =>
    Results.Ok(repo.GetAll()))
    .WithName("GetAllCars")
    .WithTags("Cars");

app.MapGet("/cars/{id}", (int id, CarRepository repo) =>
    repo.GetById(id) is Car car
        ? Results.Ok(car)
        : Results.NotFound())
    .WithName("GetCarById")
    .WithTags("Cars");

app.MapPost("/cars", (Car car, CarRepository repo) =>
{
    repo.Add(car);
    return Results.Created($"/cars/{car.Id}", car);
})
    .WithName("CreateCar")
    .WithTags("Cars");

app.MapPut("/cars/{id}", (int id, Car inputCar, CarRepository repo) =>
{
    var car = repo.GetById(id);

    if (car is null) return Results.NotFound();

    car.Make = inputCar.Make;
    car.Model = inputCar.Model;
    car.Year = inputCar.Year;

    repo.Update(car);

    return Results.NoContent();
})
    .WithName("UpdateCar")
    .WithTags("Cars");

app.MapDelete("/cars/{id}", (int id, CarRepository repo) =>
{
    if (repo.Delete(id))
    {
        return Results.Ok();
    }

    return Results.NotFound();
})
    .WithName("DeleteCar")
    .WithTags("Cars");

app.Run();

public class Car
{
    public int Id { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
}

public class CarRepository
{
    private readonly List<Car> _cars = new();
    private int _nextId = 1;

    public CarRepository()
    {
        // Add some initial car data
        Add(new Car { Make = "Toyota", Model = "Corolla", Year = 2020 });
        Add(new Car { Make = "Honda", Model = "Civic", Year = 2019 });
        Add(new Car { Make = "Ford", Model = "Mustang", Year = 2021 });
    }

    public IEnumerable<Car> GetAll() => _cars;

    public Car GetById(int id) => _cars.FirstOrDefault(c => c.Id == id);

    public void Add(Car car)
    {
        car.Id = _nextId++;
        _cars.Add(car);
    }

    public void Update(Car car)
    {
        var index = _cars.FindIndex(c => c.Id == car.Id);
        if (index != -1)
        {
            _cars[index] = car;
        }
    }

    public bool Delete(int id)
    {
        var car = GetById(id);
        if (car != null)
        {
            _cars.Remove(car);
            return true;
        }
        return false;
    }
}
