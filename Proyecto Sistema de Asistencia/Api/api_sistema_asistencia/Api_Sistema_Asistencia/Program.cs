using Api_Sistema_Asistencia;
using CapaDatos;
using CapaNegocio;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//services cors

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//JWT
//var secretKey = Crypto.Decrypt(builder.Configuration.GetSection("Jwt").GetSection("Key").Value);
//var audienceToken = builder.Configuration.GetSection("Jwt").GetSection("Audience").Value;
//var issuerToken = builder.Configuration.GetSection("Jwt").GetSection("Issuer").Value;
//var expireTime = Crypto.Decrypt(builder.Configuration.GetSection("Jwt").GetSection("ExpireMinutes").Value);

var secretKey = builder.Configuration.GetSection("Jwt").GetSection("Key").Value;
var audienceToken = builder.Configuration.GetSection("Jwt").GetSection("Audience").Value;
var issuerToken = builder.Configuration.GetSection("Jwt").GetSection("Issuer").Value;
var expireTime = builder.Configuration.GetSection("Jwt").GetSection("ExpireMinutes").Value;

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            //ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            //ValidIssuer = issuerToken,
            //ValidAudience = audienceToken,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
        };
    });

// Configuración de la conexión a la base de datos
builder.Services.AddScoped<DConexion>();

// Configuración de la inyección de dependencias
builder.Services.AddTransient<DUsuario>();
builder.Services.AddTransient<NUsuario>();

builder.Services.AddTransient<DAsistencia>();
builder.Services.AddTransient<NAsistencia>();



builder.Services.AddAutoMapper(typeof(MappingConfig));


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseCors();

app.MapControllers();

app.Run();
