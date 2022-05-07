using GameServices.Services;
using GameServices.Settings;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));


// Add services to the container.
// NEED this line to register controllers with their associated veiws
builder.Services.AddControllersWithViews();

builder.Services.AddControllers(options => {
  options.SuppressAsyncSuffixInActionNames = false;
});

var mongoDbSettings = builder.Configuration.GetSection(nameof(MongoDBSettings)).Get<MongoDBSettings>();

builder.Services.AddSingleton<IMongoClient>(serviceProvider => {
  return new MongoClient(mongoDbSettings.ConnectionString);
});
builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<ItemService>();
//builder.Services.AddSingleton<IInMemUserRepo, InMemUserRepo>();
// builder.Services.AddSingleton<IInMemItemsRepo, InMemItemsRepo>(); // in memory repo
builder.Services.AddEndpointsApiExplorer();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen();
// Add health checks to Server as well as add AspNetCore.HealthChecks.MongoDb package to check MongoDB
builder.Services.AddHealthChecks().AddMongoDb(
  mongoDbSettings.ConnectionString,
  name: "mongodb",
  timeout: TimeSpan.FromSeconds(3.5),
  tags: new[] { "ready" }
  );

var app = builder.Build();

// Configure the HTTP request pipeline.
if ( app.Environment.IsDevelopment() ) {
  app.UseSwagger();
  app.UseSwaggerUI();
  app.UseHttpsRedirection();
}

app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health/ready", new HealthCheckOptions {
  Predicate = (check) => check.Tags.Contains("ready"),
  // Add custom response object back for ready health check
  ResponseWriter = async (context, report) => {
    var result = JsonSerializer.Serialize(
      new {
        status = report.Status.ToString(),
        checks = report.Entries.Select(entry => new {
          name = entry.Key,
          status = entry.Value.Status.ToString(),
          exception = entry.Value.Exception != null ? entry.Value.Exception.Message : "none",
          duration = entry.Value.Duration.ToString()
        })
      }
      );
    context.Response.ContentType = "application/json";
    await context.Response.WriteAsync(result);
  }
});
app.MapHealthChecks("/health/live", new HealthCheckOptions {
  Predicate = (_) => false
});

app.Run();
