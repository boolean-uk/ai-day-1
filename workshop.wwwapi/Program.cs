var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ICarRepository, CarRepository>();

// Configure CORS to allow any origin
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
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

// Use the CORS policy
app.UseCors("AllowAll");

// Car API Endpoints
app.MapGet("/api/cars", (ICarRepository carRepository) =>
{
    return Results.Ok(carRepository.GetAll());
})
.WithName("GetAllCars")
.WithOpenApi();

app.MapGet("/api/cars/{id}", (int id, ICarRepository carRepository) =>
{
    var car = carRepository.GetById(id);
    return car is not null ? Results.Ok(car) : Results.NotFound();
})
.WithName("GetCarById")
.WithOpenApi();

app.MapPost("/api/cars", (Car car, ICarRepository carRepository) =>
{
    carRepository.Add(car);
    return Results.Created($"/api/cars/{car.Id}", car);
})
.WithName("CreateCar")
.WithOpenApi();

app.MapPut("/api/cars/{id}", (int id, Car car, ICarRepository carRepository) =>
{
    if (id != car.Id)
    {
        return Results.BadRequest();
    }

    var existingCar = carRepository.GetById(id);
    if (existingCar is null)
    {
        return Results.NotFound();
    }

    carRepository.Update(car);
    return Results.NoContent();
})
.WithName("UpdateCar")
.WithOpenApi();

app.MapDelete("/api/cars/{id}", (int id, ICarRepository carRepository) =>
{
    var car = carRepository.GetById(id);
    if (car is null)
    {
        return Results.NotFound();
    }

    carRepository.Delete(id);
    return Results.NoContent();
})
.WithName("DeleteCar")
.WithOpenApi();

app.Run();

public class Car
{
    public int Id { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
}

public interface ICarRepository
{
    IEnumerable<Car> GetAll();
    Car GetById(int id);
    void Add(Car car);
    void Update(Car car);
    void Delete(int id);
}

public class CarRepository : ICarRepository
{
    private readonly List<Car> _cars = new List<Car>();

    public IEnumerable<Car> GetAll() => _cars;

    public Car GetById(int id) => _cars.FirstOrDefault(c => c.Id == id);

    public void Add(Car car)
    {
        car.Id = _cars.Count > 0 ? _cars.Max(c => c.Id) + 1 : 1;
        _cars.Add(car);
    }

    public void Update(Car car)
    {
        var existingCar = GetById(car.Id);
        if (existingCar != null)
        {
            existingCar.Make = car.Make;
            existingCar.Model = car.Model;
            existingCar.Year = car.Year;
        }
    }

    public void Delete(int id)
    {
        var car = GetById(id);
        if (car != null)
        {
            _cars.Remove(car);
        }
    }
}
