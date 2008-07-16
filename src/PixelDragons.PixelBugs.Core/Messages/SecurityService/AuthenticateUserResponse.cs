namespace PixelDragons.PixelBugs.Core.Messages.SecurityService
{
    public class AuthenticateUserResponse     
    {
        public string Token { get; set; }
        
        public AuthenticateUserResponse(string token)
        {
            Token = token;
        }
    }
}