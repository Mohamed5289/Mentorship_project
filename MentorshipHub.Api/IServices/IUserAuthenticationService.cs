using MentorshipHub.Api.DTOHelpers;

namespace MentorshipHub.Api.IServices
{
    public interface IUserAuthenticationService
    {
        Task<AuthenticationModel> Registering(RegisterModel model , string profilePicturePath);
        Task<AuthenticationModel> Login(LoginModel model);
        //Task<string> CreateToken(AppUser user);
        //Task<AuthenticationModel> RefreshToken(string refreshToken);
        //Task<bool> RevokeToken(string refreshToken);
    }
}
