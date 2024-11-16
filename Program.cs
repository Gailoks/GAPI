
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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


/* 
1. **Create Session**
   API: `POST /sessions`
   Description: Create a new session for a user
   Input: `user_id` (required), `api_key` (required), `session_name` (optional)
   Output: `session_id`, `session_name`, `created_at`

2. **Get User Sessions**
   API: `GET /users/{user_id}/sessions`
   Description: Get all sessions for a user
   Input: `user_id` (required), `api_key` (required)
   Output: `sessions` (array of session objects with `session_id`, `session_name`, `created_at`)

3. **Get User Session by ID**
   API: `GET /users/{user_id}/sessions/{session_id}`
   Description: Get a session by ID for a user
   Input: `user_id` (required), `session_id` (required), `api_key` (required)
   Output: `session_id`, `session_name`, `created_at`
*/



var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
