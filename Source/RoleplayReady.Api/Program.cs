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

builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = "ApiKey";
    options.DefaultChallengeScheme = "ApiKey";
}).AddScheme<AuthenticationSchemeOptions, ApiKeyAuthenticationHandler>("ApiKey", null);

builder.Services.AddDefaultSystemProviders();
builder.Services.AddDummyUserAccessor();
builder.Services.AddDomainHandlers();
builder.Services.AddRepositories();
builder.Services.AddScoped(sp => new CustomExceptionFilter(sp.GetRequiredService<ILoggerFactory>()));

builder.Services.AddControllers(options => options.Filters.Add<CustomExceptionFilter>())
                .ConfigureApiBehaviorOptions(options => options.SuppressMapClientErrors = true);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "RoleplayReady API", Version = "v1" });
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
