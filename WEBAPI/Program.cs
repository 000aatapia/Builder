using Application.Services;
using Infraestructure.Factories;

var builder = WebApplication.CreateBuilder(args);


// Application
builder.Services.AddScoped<VirtualMachineProvisionService>();


// Repositorio (InMemory)
builder.Services.AddSingleton<InMemoryVirtualMachineRepository>();

// Infrastructure (Factories)
builder.Services.AddScoped<ICloudResourceFactory, AwsResourceFactory>();
builder.Services.AddScoped<ICloudResourceFactory, AzureResourceFactory>();
builder.Services.AddScoped<ICloudResourceFactory, GcpResourceFactory>();
builder.Services.AddScoped<ICloudResourceFactory, OnPremResourceFactory>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();