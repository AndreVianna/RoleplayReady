global using System.Abstractions;
global using System.Extensions;
global using System.Globalization;
global using System.Results;
global using System.Text;

global using FluentAssertions;

global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Logging.Abstractions;

global using NSubstitute;
global using NSubstitute.ExceptionExtensions;

global using RolePlayReady.Handlers.GameSystem;
global using RolePlayReady.Handlers.Sphere;
global using RolePlayReady.Handlers.User;
global using RolePlayReady.Models;
global using RolePlayReady.Models.Abstractions;
global using RolePlayReady.Models.Attributes;

global using Xunit;

global using static System.Text.Json.JsonSerializer;