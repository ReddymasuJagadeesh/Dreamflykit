using System.Security.Claims;
using Dreamflykit.Infrastructure.Identity;
using Dreamflykit.Web.Areas.Admin.Controllers;
using Dreamflykit.Web.Areas.Admin.Filters;
using Dreamflykit.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Xunit;

namespace Dreamflykit.Tests;

public class ApplicationSecurityTests
{
    private static Mock<UserManager<ApplicationUser>> CreateUserManagerMock()
    {
        var store = new Mock<IUserStore<ApplicationUser>>();
        return new Mock<UserManager<ApplicationUser>>(
            store.Object, null!, null!, null!, null!, null!, null!, null!, null!);
    }

    [Fact]
    public async Task AdminMfaFilter_AdminWithout2fa_RedirectsToMfaSetup()
    {
        // Arrange
        var userManagerMock = CreateUserManagerMock();
        var adminUser = new ApplicationUser
        {
            Id = Guid.NewGuid(),
            Email = "admin@dreamflykit.com",
            TwoFactorEnabled = false
        };

        userManagerMock
            .Setup(u => u.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
            .ReturnsAsync(adminUser);

        var filter = new AdminMfaFilter(userManagerMock.Object);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, adminUser.Id.ToString()),
            new Claim(ClaimTypes.Role, "Admin")
        };
        var identity = new ClaimsIdentity(claims, "TestAuth");
        var principal = new ClaimsPrincipal(identity);

        var httpContext = new DefaultHttpContext
        {
            User = principal
        };
        httpContext.Request.Path = "/Admin/Dashboard";

        var actionContext = new ActionContext(
            httpContext,
            new RouteData(),
            new ActionDescriptor(),
            new ModelStateDictionary());

        var executingContext = new ActionExecutingContext(
            actionContext,
            new List<IFilterMetadata>(),
            new Dictionary<string, object?>(),
            new object());

        var executedCalled = false;
        ActionExecutionDelegate next = () =>
        {
            executedCalled = true;
            return Task.FromResult(new ActionExecutedContext(actionContext, new List<IFilterMetadata>(), new object()));
        };

        // Act
        await filter.OnActionExecutionAsync(executingContext, next);

        // Assert
        Assert.False(executedCalled);
        var redirectResult = Assert.IsType<RedirectToActionResult>(executingContext.Result);
        Assert.Equal("Setup", redirectResult.ActionName);
        Assert.Equal("Mfa", redirectResult.ControllerName);
        Assert.Equal("Admin", redirectResult.RouteValues?["area"]);
    }

    [Fact]
    public async Task AdminMfaFilter_AdminWith2faEnabled_AllowsExecution()
    {
        // Arrange
        var userManagerMock = CreateUserManagerMock();
        var adminUser = new ApplicationUser
        {
            Id = Guid.NewGuid(),
            Email = "admin@dreamflykit.com",
            TwoFactorEnabled = true
        };

        userManagerMock
            .Setup(u => u.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
            .ReturnsAsync(adminUser);

        var filter = new AdminMfaFilter(userManagerMock.Object);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, adminUser.Id.ToString()),
            new Claim(ClaimTypes.Role, "Admin")
        };
        var identity = new ClaimsIdentity(claims, "TestAuth");
        var principal = new ClaimsPrincipal(identity);

        var httpContext = new DefaultHttpContext
        {
            User = principal
        };
        httpContext.Request.Path = "/Admin/Dashboard";

        var actionContext = new ActionContext(
            httpContext,
            new RouteData(),
            new ActionDescriptor(),
            new ModelStateDictionary());

        var executingContext = new ActionExecutingContext(
            actionContext,
            new List<IFilterMetadata>(),
            new Dictionary<string, object?>(),
            new object());

        var executedCalled = false;
        ActionExecutionDelegate next = () =>
        {
            executedCalled = true;
            return Task.FromResult(new ActionExecutedContext(actionContext, new List<IFilterMetadata>(), new object()));
        };

        // Act
        await filter.OnActionExecutionAsync(executingContext, next);

        // Assert
        Assert.True(executedCalled);
        Assert.Null(executingContext.Result);
    }

    [Fact]
    public async Task AdminMfaFilter_MfaSetupPath_BypassesFilterToPreventRedirectLoop()
    {
        // Arrange
        var userManagerMock = CreateUserManagerMock();
        var filter = new AdminMfaFilter(userManagerMock.Object);

        var claims = new[]
        {
            new Claim(ClaimTypes.Role, "Admin")
        };
        var identity = new ClaimsIdentity(claims, "TestAuth");
        var principal = new ClaimsPrincipal(identity);

        var httpContext = new DefaultHttpContext
        {
            User = principal
        };
        httpContext.Request.Path = "/Admin/Mfa/Setup";

        var actionContext = new ActionContext(
            httpContext,
            new RouteData(),
            new ActionDescriptor(),
            new ModelStateDictionary());

        var executingContext = new ActionExecutingContext(
            actionContext,
            new List<IFilterMetadata>(),
            new Dictionary<string, object?>(),
            new object());

        var executedCalled = false;
        ActionExecutionDelegate next = () =>
        {
            executedCalled = true;
            return Task.FromResult(new ActionExecutedContext(actionContext, new List<IFilterMetadata>(), new object()));
        };

        // Act
        await filter.OnActionExecutionAsync(executingContext, next);

        // Assert
        Assert.True(executedCalled);
        Assert.Null(executingContext.Result);
    }

    [Fact]
    public async Task AdminMfaFilter_CustomerUser_IsIgnoredByFilter()
    {
        // Arrange
        var userManagerMock = CreateUserManagerMock();
        var filter = new AdminMfaFilter(userManagerMock.Object);

        var claims = new[]
        {
            new Claim(ClaimTypes.Role, "Customer")
        };
        var identity = new ClaimsIdentity(claims, "TestAuth");
        var principal = new ClaimsPrincipal(identity);

        var httpContext = new DefaultHttpContext
        {
            User = principal
        };
        httpContext.Request.Path = "/Account/Profile";

        var actionContext = new ActionContext(
            httpContext,
            new RouteData(),
            new ActionDescriptor(),
            new ModelStateDictionary());

        var executingContext = new ActionExecutingContext(
            actionContext,
            new List<IFilterMetadata>(),
            new Dictionary<string, object?>(),
            new object());

        var executedCalled = false;
        ActionExecutionDelegate next = () =>
        {
            executedCalled = true;
            return Task.FromResult(new ActionExecutedContext(actionContext, new List<IFilterMetadata>(), new object()));
        };

        // Act
        await filter.OnActionExecutionAsync(executingContext, next);

        // Assert
        Assert.True(executedCalled);
        Assert.Null(executingContext.Result);
    }

    [Fact]
    public void MfaSetupViewModel_ValidatesVerificationCodeRequirements()
    {
        // Arrange
        var model = new MfaSetupViewModel
        {
            SharedKey = "abcd efgh ijkl mnop",
            AuthenticatorUri = "otpauth://totp/Dreamflykit:admin?secret=abcdefghijklmnop&issuer=Dreamflykit",
            VerificationCode = "123456"
        };

        // Assert
        Assert.Equal(6, model.VerificationCode.Length);
        Assert.Contains("otpauth://totp", model.AuthenticatorUri);
        Assert.Contains("secret=abcdefghijklmnop", model.AuthenticatorUri);
    }
}

