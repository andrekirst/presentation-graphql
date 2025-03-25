var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithPgWeb()
    .WithDataVolume();

var postgresdb = postgres.AddDatabase("postgresdb");

var apiService = builder
    .AddProject<Projects.ParkplatzDresden_ApiService>("apiservice")
    .WithReference(postgresdb)
    .WaitFor(postgresdb);

builder.AddProject<Projects.ParkplatzDresden_Web>("web")
    .WithReference(apiService)
    .WaitFor(apiService)
    .WithExternalHttpEndpoints();

builder.AddProject<Projects.ParkplatzDresden_ScraperService>("scraper")
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
