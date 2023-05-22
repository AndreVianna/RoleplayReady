// Global using directives

global using System.Abstractions;
global using System.ComponentModel.DataAnnotations;
global using System.Diagnostics.CodeAnalysis;
global using System.Extensions;
global using System.IdentityModel.Tokens.Jwt;
global using System.Net;
global using System.Results;
global using System.Security.Claims;
global using System.Security.Cryptography;
global using System.Security.Principal;
global using System.Text;
global using System.Text.Encodings.Web;
global using System.Utilities;

global using Microsoft.AspNetCore.Authentication;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.Filters;
global using Microsoft.AspNetCore.Mvc.ModelBinding;
global using Microsoft.Extensions.Options;
global using Microsoft.IdentityModel.Tokens;
global using Microsoft.OpenApi.Models;

global using RolePlayReady.Api.Controllers.Auth.Models;
global using RolePlayReady.Api.Controllers.Common;
global using RolePlayReady.Api.Controllers.Systems.Models;
global using RolePlayReady.Api.Utilities;
global using RolePlayReady.Constants;
global using RolePlayReady.DataAccess.Extensions;
global using RolePlayReady.DataAccess.Identity;
global using RolePlayReady.Extensions;
global using RolePlayReady.Handlers.Auth;
global using RolePlayReady.Handlers.System;
global using RolePlayReady.Models;

global using Serilog;

global using Swashbuckle.AspNetCore.Annotations;
global using Swashbuckle.AspNetCore.SwaggerGen;

global using ILogger = Microsoft.Extensions.Logging.ILogger;
