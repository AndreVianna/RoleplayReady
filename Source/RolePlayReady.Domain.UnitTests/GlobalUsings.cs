global using System.Abstractions;
global using System.Results;
global using System.Security;
global using System.Security.Cryptography;
global using System.Validation;

global using FluentAssertions;

global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Logging.Abstractions;

global using NSubstitute;

global using RolePlayReady.Constants;
global using RolePlayReady.Models;
global using RolePlayReady.Models.Abstractions;
global using RolePlayReady.Models.Attributes;
global using RolePlayReady.Repositories.GameSystem;
global using RolePlayReady.Repositories.Sphere;
global using RolePlayReady.Repositories.User;

global using Xunit;