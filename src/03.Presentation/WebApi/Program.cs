using AIHR.EventScheduler.Persistence.EF;
using AIHR.EventScheduler.WebApi.Configs.Middlewares;
using AIHR.EventScheduler.WebApi.Configs.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Host.AddAutofacConfig();

builder.Services.AddDbContextPool<EfDataContext>(options =>
{
    options.UseSqlite("Data Source=EventScheduling.db");
});
builder.Services.AddExceptionHandler<KnownExceptionHandler>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddControllers();

var app = builder.Build();

EnsureDatabaseCreated(app);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();

void EnsureDatabaseCreated(WebApplication app)
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<EfDataContext>();
        if (dbContext.Database.GetPendingMigrations().Any())
        {
            dbContext.Database.Migrate();
        }
    }
}