using MentorshipHub.Api.DTOHelpers;
using MentorshipHub.Api.Models;
using Microsoft.AspNetCore.Identity;
using MentorshipHub.Api.IServices;
using MentorshipHub.Api.Data;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System.Text.RegularExpressions;

namespace MentorshipHub.Api.Services
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly UserManager<AppUser> userManager;
        private readonly TokenGenerator userToken;
        private readonly AppIdentityDbContext appIdentityDbContext;


        public UserAuthenticationService(UserManager<AppUser> userManager, AppIdentityDbContext appIdentityDbContext, TokenGenerator userToken)
        {
            this.userManager = userManager;
            this.userToken = userToken;
            this.appIdentityDbContext = appIdentityDbContext;
        }

        public async Task<AuthenticationModel> Login(LoginModel model)
        {
            if (model is null)
                return new AuthenticationModel { Message = "Login model is null!" };

            string pattern = @"^[a-zA-Z0-9.AppIdentityDbContext%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            var IsValid = Regex.IsMatch(model.Email, pattern);

            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password) || !IsValid)
                return new AuthenticationModel { Message = "Email or password is incorrect !" };

            await using var transaction = await appIdentityDbContext.Database.BeginTransactionAsync();

            try
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user is null || !await userManager.CheckPasswordAsync(user, model.Password))
                {
                    return new AuthenticationModel { Message = "Email or password is incorrect !" };
                }

                var authenticationModel = new AuthenticationModel();

                var token = await userToken.CreateToken(user);

                if (user.RefreshTokens.Any(u => u.IsActive))
                {
                    var refreshToken = user.RefreshTokens.FirstOrDefault(r => r.IsActive);
                    authenticationModel.RefreshToken = refreshToken!.Token;
                    authenticationModel.RefreshTokenExpiration = refreshToken.Expires;
                }
                else
                {
                    var refreshToken = userToken.GeneratorRefreshToken();
                    authenticationModel.RefreshToken = refreshToken.Token;
                    authenticationModel.RefreshTokenExpiration = refreshToken.Expires;
                    user.RefreshTokens.Add(refreshToken);
                    await userManager.UpdateAsync(user);
                }

                authenticationModel.IsAuthenticated = true;
                authenticationModel.Token = token;
                authenticationModel.Email = user.Email!;
                authenticationModel.Username = user.UserName!;
                authenticationModel.Roles = (await userManager.GetRolesAsync(user)).ToList();

                await transaction.CommitAsync();

                return authenticationModel;

            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                return new AuthenticationModel { Message = e.Message };
            }
        }

        public async Task<AuthenticationModel> Registering(RegisterModel model , string profilePicturePath)
        {
            if (model is null)
                return new AuthenticationModel { Message = "Register model is null!" };

            string pattern = @"^[a-zA-Z0-9.AppIdentityDbContext%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            var IsValid = Regex.IsMatch(model.Email, pattern);

            if(!IsValid)
                return new AuthenticationModel { Message = "Email is not valid!" };

            if (await userManager.FindByEmailAsync(model.Email) is not null)
                return new AuthenticationModel { Message = "Email is already registered!" };

            if (await userManager.FindByNameAsync(model.Username) is not null)
                return new AuthenticationModel { Message = "Username is already registered!" };

            await using var transaction = await appIdentityDbContext.Database.BeginTransactionAsync();
            try
            {
                var user = new AppUser
                {
                    Email = model.Email,
                    UserName = model.Username,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    ProfilePicture = profilePicturePath ?? "",
                    RefreshTokens = new List<RefreshToken>()
                };

                var passwordValidator = new PasswordValidator<AppUser>();
                var validationResult = await passwordValidator.ValidateAsync(userManager, user, model.Password);

                if (!validationResult.Succeeded)
                {
                    var errors = validationResult.Errors.Select(e => e.Description);
                    return new AuthenticationModel { Message = string.Join(" ", errors) };
                }

                var result = await userManager.CreateAsync(user, model.Password);

                if (!result.Succeeded)
                {
                    var errors = result.Errors.Select(e => e.Description);
                    return new AuthenticationModel { Message = string.Join(" ", errors) };
                }

                var authenticationModel = new AuthenticationModel();

                var refreshToken = userToken.GeneratorRefreshToken();
                authenticationModel.RefreshToken = refreshToken.Token;
                authenticationModel.RefreshTokenExpiration = refreshToken.Expires;
                user.RefreshTokens.Add(refreshToken);
                await userManager.UpdateAsync(user);

                await userManager.AddToRoleAsync(user, "Mentee");

                authenticationModel.IsAuthenticated = true;
                authenticationModel.Email = user.Email!;
                authenticationModel.Username = user.UserName!;
                authenticationModel.Token = await userToken.CreateToken(user);
                var roles = await userManager.GetRolesAsync(user);
                authenticationModel.Roles = roles.ToList();

                await transaction.CommitAsync();

                return authenticationModel;
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                return new AuthenticationModel { Message = e.Message };
            }
        }
    }
}
