using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SmartLedger.BAL.Interfaces;
using SmartLedger.BAL.Services;
using SmartLedger.DAL.Context;
using SmartLedger.DAL.Interfaces;
using SmartLedger.DAL;
using System.Text;
using SmartLedger.DAL.Entities;

var builder = WebApplication.CreateBuilder(args);

// 🔧 Configure CORS (if needed for frontend)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// 🚀 Add DbContext
builder.Services.AddDbContext<SmartLedgerDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DatabaseConnection")));

// 🚀 Register repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IVendorRepository, VendorRepository>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<ICashflowPredictionRepository, CashflowPredictionRepository>();



// Ensure invoice repository is registered (used by PaymentService)
builder.Services.AddScoped<IRepository<Invoice>, Repository<Invoice>>();


// 🚀 Register services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IVendorService, VendorService>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<ICashflowService, CashflowService>();
// 🚀 Add Controllers
builder.Services.AddControllers();

// 🚀 JWT Configuration
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var jwtKey = jwtSettings["Key"];

if (string.IsNullOrEmpty(jwtKey))
{
    throw new InvalidOperationException("JWT Key is not configured in appsettings.json");
}

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
        ClockSkew = TimeSpan.Zero // Remove default 5 minute clock skew
    };
});

builder.Services.AddAuthorization();

// 🚀 Swagger Configuration with JWT Auth Support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "SmartLedger API",
        Version = "v1",
        Description = "API documentation for SmartLedger – AI-enhanced finance tracker",
        Contact = new OpenApiContact
        {
            Name = "SmartLedger Team",
            Email = "support@smartledger.com"
        }
    });

    // 🔐 Enable JWT input in Swagger UI
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' followed by a space and then your JWT token.\n\nExample: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// 🔧 Enable CORS
app.UseCors("AllowAll");

// ✅ Enable Swagger with explicit configuration
app.UseSwagger(c =>
{
    c.RouteTemplate = "swagger/{documentName}/swagger.json";
});

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "SmartLedger API v1");
    options.RoutePrefix = "swagger";
    options.DisplayRequestDuration();
    options.EnableTryItOutByDefault();
});

// 🔥 Root endpoint
app.MapGet("/", () => Results.Ok(new
{
    message = "SmartLedger API",
    swagger = "/swagger"
}));

// 🔒 Important: Authentication must come before Authorization
app.UseAuthentication();
app.UseAuthorization();

// 🎯 Map Controllers
app.MapControllers();

// 🚀 Start the application
app.Run();