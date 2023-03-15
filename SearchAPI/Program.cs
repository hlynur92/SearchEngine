using Microsoft.AspNetCore.Hosting;
using SearchAPI.Controllers;
using System;
using System.Diagnostics;
using System.Net;
using System.Runtime.CompilerServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


bool success = false;
while(!success)
{
    HttpClient lb_api = new HttpClient();

    lb_api.BaseAddress = new Uri("http://searchengine-loadbalancer-1");

    var task = lb_api.GetAsync("/Load/RegisterServices");
    task.Wait();
    success = task.Result.IsSuccessStatusCode;
    if (!success) Thread.Sleep(5000);
}

app.Run();