using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using QuizApp.Business;
using QuizApp.Data;
using QuizApp.Models;
using QuizApp.WebAPI;

var builder = WebApplication.CreateBuilder(args);

// Configure app settings
builder.Services.Configure<AppSetting>(builder.Configuration);

var appSettings = builder.Configuration.Get<AppSetting>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() { Title = "QuizKuber Web API", Version = "v1" });
    options.DocumentFilter<AuthTokenDocumentFilter>();

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter into field the word 'Bearer' following by space and JWT",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT"
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
            new string[] { }
        }
    });

    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

builder.Services.AddDbContext<QuizAppDbContext>(options =>
{
    options.UseSqlServer(appSettings?.ConnectionStrings.QuizAppConnection);
});

builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<QuizAppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddHttpContextAccessor();

builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserIdentity, UserIdentity>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(QuizGetAllQuery).Assembly));

// Add CORS policy with allowed origins
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        options.AddPolicy("AllowedOrigins", builder => builder
            .WithOrigins("http://localhost:4200", "https://localhost:4200")
            .WithHeaders(HeaderNames.ContentType, HeaderNames.Authorization, HeaderNames.Accept, HeaderNames.XRequestedWith)
            .WithMethods("GET", "POST", "PUT", "DELETE"));
    });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings?.Jwt?.Secret ?? "congdinh2012@hotmail.com"))
    };
});

builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{


    using (var scope = app.Services.CreateScope())
    {
        var dbcontext = scope.ServiceProvider.GetRequiredService<QuizAppDbContext>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();

        var rolesJsonPath = Path.Combine(app.Environment.WebRootPath, "data", "roles.json");
        var usersJsonPath = Path.Combine(app.Environment.WebRootPath, "data", "users.json");
        DbInitializer.Seed(dbcontext, userManager, roleManager, rolesJsonPath, usersJsonPath, true);
    }
}

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("v1/swagger.json", "Quiz Kuber Web API v1");
});

// Error Handling:
// Implement global error handling middleware to catch and log exceptions thrown during request processing.
// Use status codes (e.g., BadRequest, NotFound, InternalServerError) to return appropriate HTTP responses for different error scenarios.
// Check each exception for specific types and return the appropriate status code and message.
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
        var exception = exceptionHandlerPathFeature?.Error;

        var result = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "An error occurred while processing your request.",
            Detail = exception?.Message
        };

        if (exception is KeyNotFoundException)
        {
            result.Status = StatusCodes.Status404NotFound;
            result.Title = "The requested resource was not found.";
            result.Detail = exception.Message;
        }
        else if (exception is ArgumentException)
        {
            result.Status = StatusCodes.Status400BadRequest;
            result.Title = "The request was invalid.";
            result.Detail = exception.Message;
        }

        context.Response.StatusCode = result.Status ?? StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/json";

        await context.Response.WriteAsJsonAsync(result);
    });
});

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

// Enable CORS using AllowedOrigins policy
app.UseCors("AllowedOrigins");

app.MapControllers();

app.Run();