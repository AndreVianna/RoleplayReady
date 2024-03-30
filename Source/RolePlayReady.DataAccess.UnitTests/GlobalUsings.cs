global using System.Abstractions;
global using System.Extensions;
global using System.Globalization;
global using System.Security.Cryptography;
global using System.Text;

global using FluentAssertions;

global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Logging.Abstractions;

global using NSubstitute;
global using NSubstitute.ExceptionExtensions;

global using RolePlayReady.DataAccess.Identity;
global using RolePlayReady.Handlers.Setting;
global using RolePlayReady.Models;

global using Xunit;

global using static System.Text.Json.JsonSerializer;