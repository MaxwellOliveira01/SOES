using Microsoft.EntityFrameworkCore;
using SOE.Configuration;
using SOE.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<IIdentificationService, IdentificationService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IVoterSessionService, VoterSessionService>();
builder.Services.AddScoped<IOtpService, OtpService>();

builder.Services.AddSingleton<IEmailSender, EmailSender>();

builder.Services.Configure<SmtpConfig>(
    builder.Configuration.GetSection("SmtpConfig")
);

builder.Services.AddDataProtection();

var dbPath = Path.Combine(Directory.GetCurrentDirectory(), "Database", "app.db");
builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseSqlite($"Data Source={dbPath};")
);

builder.Configuration.AddUserSecrets<Program>();

// TODO: improve this policy before putting it in production
builder.Services.AddCors(options => {
    options.AddDefaultPolicy(policy => {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseHttpsRedirection();

app.UseCors(); // need to be before UseAuthorization
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope()) {
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

app.Run();