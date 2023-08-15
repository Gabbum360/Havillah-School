using ApplicationServices.AuthenticationManagement;
using ApplicationServices.Common.Model;
using ApplicationServices.DataStorage;
using ApplicationServices.Features.Teacher;
using ApplicationServices.Features.Teacher.Dto;
using Havillah_SchoolManagement_System;
using Havillah_SchoolManagement_System.ServiceCollections;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationServices(builder.Configuration);
//builder.Services.ConfigureSwagger();
builder.ConfigureSerilog();
/*builder.Services.AddApiVersioning(cfg =>
{
    cfg.AssumeDefaultVersionWhenUnspecified = true;
});*/

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.MigrateDatabase();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

#region Teacher
app.MapPost("/registerTeacher", async ([FromBody] AddTeacherDto model, IMediator mediator) =>
{
    var result = await mediator.Send(new RegisterTeacherCommand() { AddTeacherDto = model});
    if (!result.IsSuccess) return Result.Ok("unable to complete");
    if (result.IsSuccess) return Result.Ok("Successful!"); return Result.Fail("Bad Request!");
}).Produces<Result>(StatusCodes.Status204NoContent)
.Produces<Result>(StatusCodes.Status200OK)
.Produces<Result>(StatusCodes.Status400BadRequest).WithName("registerTeacher").WithTags("Teacher Registration");
#endregion

#region LoginUser
app.MapPost("/login", async ([FromBody] LoginCommand loginModel, IMediator mediator) =>
{
    var loginUser = await mediator.Send(loginModel);
    return Results.Ok(loginUser);
}).WithName("Login")
                .Produces<ErrorResult<(Guid, ErrorResult)>>(StatusCodes.Status200OK)
                .Produces<ErrorResult<(Guid, ErrorResult)>>(StatusCodes.Status400BadRequest)
                .WithTags("Authentication");
#endregion

#region RegisterUser
app.MapPost("/register", async ([FromBody] RegistrationCommand userModel, IMediator mediator) =>
{
    var registerUser = await mediator.Send(userModel);
    return Results.Ok(registerUser);
}).WithName("UserRegistration")
                .Produces<ErrorResult<string>>(StatusCodes.Status200OK)
                .Produces<ErrorResult<string>>(StatusCodes.Status400BadRequest)
                .WithTags("Authentication")
                .RequireCors("AllowSpecificOrigins");
#endregion

#region

#endregion
app.Run();
