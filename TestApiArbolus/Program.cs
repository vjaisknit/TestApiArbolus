using Services.ApiService;
using Services.EmployeeService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Register HttpClient for API client
builder.Services.AddHttpClient<IEmployeeApiService, EmployeeApiService>();

// Register EmployeeService
builder.Services.AddScoped<IEmployeeService, EmployeeService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
