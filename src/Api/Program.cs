using ErpApp.TruckModule.Api;
using ErpApp.TruckModule.Application.Command;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies([
    typeof(CreateTruckCommand).Assembly,
]));

builder.Services.RegisterTruckModuleDependencies(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler();
}

app.UseHttpsRedirection();

app.AddTruckModuleApi();

app.MapControllers();

await app.RunMigrations();
await app.RunAsync();
