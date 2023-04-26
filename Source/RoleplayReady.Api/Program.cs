using RolePlayReady.Security.Abstractions;

var builder = WebApplication.CreateBuilder(args);

var env = builder.Environment;
builder.Configuration
       .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
       .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

if (env.IsDevelopment()) {
    builder.Configuration.AddUserSecrets<Program>();
}

Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .CreateLogger();

builder.Host.UseSerilog();

const string authenticateScheme = "Bearer";
builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = authenticateScheme;
    options.DefaultChallengeScheme = authenticateScheme;
}).AddScheme<AuthenticationSchemeOptions, ApiKeyAuthenticationHandler>(authenticateScheme, null);

builder.Services.AddDefaultSystemProviders();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IUserAccessor, ApiUserAccessor>();
builder.Services.AddDomainHandlers();
builder.Services.AddRepositories();
builder.Services.AddScoped(sp => new CustomExceptionFilter(sp.GetRequiredService<ILoggerFactory>()));

builder.Services.AddControllers(options => options.Filters.Add<CustomExceptionFilter>())
                .ConfigureApiBehaviorOptions(options => options.SuppressMapClientErrors = true);

const string apiTitle = "RoleplayReady API";
const string apiVersion = "v1";
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc(apiVersion, new OpenApiInfo { Title = apiTitle, Version = apiVersion });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
        Description = "JWT Authorization header using the Bearer scheme.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement { {
        new OpenApiSecurityScheme {
            Reference = new OpenApiReference {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
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
