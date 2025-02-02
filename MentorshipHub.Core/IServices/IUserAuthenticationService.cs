using MentorshipHub.Core.DTOHelpers;

namespace MentorshipHub.Core.IServices
{
    public interface IUserAuthenticationService
    {
        Task<AuthenticationModel> Registering(RegisterModel model , string profilePicturePath);
        Task<AuthenticationModel> Login(LoginModel model);
        //TaskOfMentorship<string> CreateToken(AppUser user);
        //TaskOfMentorship<AuthenticationModel> RefreshToken(string refreshToken);
        //TaskOfMentorship<bool> RevokeToken(string refreshToken);
    }
}
