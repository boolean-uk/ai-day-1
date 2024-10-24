using System.Collections.Generic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5140") // Update with your frontend URL
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(); // Ensure CORS middleware is added to the pipeline

// In-memory list to store cars
var cars = new List<Car>
{
    new Car { Id = 1, Make = "Toyota", Model = "Corolla", Year = 2020 },
    new Car { Id = 2, Make = "Honda", Model = "Civic", Year = 2019 },
    new Car { Id = 3, Make = "Ford", Model = "Mustang", Year = 2021 }
};

app.MapGet("/cars", () => cars)
   .WithName("GetAllCars")
   .WithOpenApi();

app.MapGet("/cars/{id}", (int id) =>
{
    var car = cars.FirstOrDefault(c => c.Id == id);
    return car is not null ? Results.Ok(car) : Results.NotFound();
})
.WithName("GetCarById")
.WithOpenApi();

app.MapPost("/cars", (Car car) =>
{
    car.Id = cars.Max(c => c.Id) + 1;
    cars.Add(car);
    return Results.Created($"/cars/{car.Id}", car);
})
.WithName("CreateCar")
.WithOpenApi();

app.MapPut("/cars/{id}", (int id, Car inputCar) =>
{
    var car = cars.FirstOrDefault(c => c.Id == id);
    if (car is null) return Results.NotFound();

    car.Make = inputCar.Make;
    car.Model = inputCar.Model;
    car.Year = inputCar.Year;

    return Results.NoContent();
})
.WithName("UpdateCar")
.WithOpenApi();

app.MapDelete("/cars/{id}", (int id) =>
{
    var car = cars.FirstOrDefault(c => c.Id == id);
    if (car is not null)
    {
        cars.Remove(car);
        return Results.Ok(car);
    }

    return Results.NotFound();
})
.WithName("DeleteCar")
.WithOpenApi();

app.Run();

public class Car
{
    public int Id { get; set; }
    public required string Make { get; set; } // Added 'required' modifier
    public required string Model { get; set; } // Added 'required' modifier
    public int Year { get; set; }
}
