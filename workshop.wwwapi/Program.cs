var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllersWithViews();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});


// In-memory database for simplicity
var cars = new List<Car>
{
    new Car { Id = 1, Make = "Toyota", Model = "Corolla", Year = 2020, Price = 200000 },
    new Car { Id = 2, Make = "Honda", Model = "Civic", Year = 2019, Price = 100000 },
    new Car { Id = 3, Make = "Ford", Model = "Mustang", Year = 2021, Price = 5000 }
};

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


// Use the CORS policy
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapGet("/cars", () => Results.Ok(cars));

app.MapGet("/cars/{id}", (int id) =>
{
    var car = cars.FirstOrDefault(c => c.Id == id);
    return car is not null ? Results.Ok(car) : Results.NotFound();
});

app.MapPost("/cars", (Car car) =>
{
    car.Id = cars.Count > 0 ? cars.Max(c => c.Id) + 1 : 1;
    cars.Add(car);
    return Results.Created($"/cars/{car.Id}", car);
});

app.MapPut("/cars/{id}", (int id, Car updatedCar) =>
{
    var car = cars.FirstOrDefault(c => c.Id == id);
    if (car is null) return Results.NotFound();

    car.Make = updatedCar.Make;
    car.Model = updatedCar.Model;
    car.Year = updatedCar.Year;
    car.Price = updatedCar.Price;

    return Results.NoContent();
});

app.MapDelete("/cars/{id}", (int id) =>
{
    var car = cars.FirstOrDefault(c => c.Id == id);
    if (car is null) return Results.NotFound();

    cars.Remove(car);
    return Results.NoContent();
});

app.Run();

public class Car
{
    public int Id { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public int Price { get; set; }
}
