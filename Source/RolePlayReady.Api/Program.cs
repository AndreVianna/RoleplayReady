using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

using RolePlayReady.Api.Utilities;
using RolePlayReady.Security.Abstractions;

var builder = WebApplication.CreateBuilder(args);

var env = builder.Environment;
builder.Configuration
       .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
       .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

if (env.IsDevelopment())
    builder.Configuration.AddUserSecrets<Program>();

Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .CreateLogger();

builder.Host.UseSerilog();

const string authScheme = "Bearer";
const string authHeaderKey = "Authorization";
builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = authScheme;
    options.DefaultChallengeScheme = authScheme;
}).AddScheme<AuthenticationSchemeOptions, ApiKeyAuthenticationHandler>(authScheme, null);

builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new(1, 0);
});
builder.Services.AddVersionedApiExplorer(options => {
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddDefaultSystemProviders();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IUserAccessor, ApiUserAccessor>();
builder.Services.AddDomainHandlers();
builder.Services.AddRepositories();
builder.Services.AddScoped(sp => new CustomExceptionFilter(sp.GetRequiredService<ILoggerFactory>(), env));

builder.Services.AddControllers(options => {
        options.Filters.Add<CustomExceptionFilter>();
    })
    .ConfigureApiBehaviorOptions(options => options.SuppressMapClientErrors = true);

const string apiTitle = "RoleplayReady API";
const string apiVersion = "v1";
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc(apiVersion, new OpenApiInfo { Title = apiTitle, Version = apiVersion });

    c.AddSecurityDefinition(authScheme, new OpenApiSecurityScheme {
        Description = "Please enter a valid JWT token.",
        Name = authHeaderKey,
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = authScheme,
    });

    c.DocInclusionPredicate((name, api) => true);
    c.TagActionsBy(api => new[] { api.GroupName });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement { {
        new OpenApiSecurityScheme {
            Reference = new OpenApiReference {
                Type = ReferenceType.SecurityScheme,
                Id = authScheme,
            },
        },
        new List<string>()
    }});

    c.EnableAnnotations();
});

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
