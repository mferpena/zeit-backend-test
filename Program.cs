using Core.Ports;
using Core.UseCases;
using Core.UseCases.Impl;
using Infrastructure.Persistence;
using Infrastructure.Secondary.Security;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<AuthUseCase, AuthServiceImpl>();
builder.Services.AddScoped<TodoUseCase, TodoServiceImpl>();

builder.Services.AddSingleton<TodoPersistencePort, TodoPersistenceAdapter>();
builder.Services.AddSingleton<UserPersistencePort, UserPersistenceAdapter>();

builder.Services.AddSingleton<TokenSecurityPort>(new TokenAdapter("q8Znyn0MQUQqyLyTS5h5Mfmcd9ZblXp9auYGnJ8SB2J0Cxao4a"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
