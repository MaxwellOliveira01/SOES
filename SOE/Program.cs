using Microsoft.Extensions.Options;
using SOE.Configuration;
using SOE.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IEmailSender, EmailSender>();

builder.Services.Configure<SmtpConfig>(
    builder.Configuration.GetSection("SmtpConfig")
);

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment()) {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

app.UseHttpsRedirection();

var scope = app.Services.CreateScope();
// var emailSender = scope.ServiceProvider.GetRequiredService<IEmailSender>();
// await emailSender.SendOtpAsync("<TODO>", "123456");

app.Run();
