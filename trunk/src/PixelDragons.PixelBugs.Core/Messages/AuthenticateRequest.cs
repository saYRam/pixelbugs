using PixelDragons.PixelBugs.Core.Exceptions;

namespace PixelDragons.PixelBugs.Core.Messages
{
    public class AuthenticateRequest : IRequest
    {
        public string Password { get; set; }
        public string UserName { get; set; }

        public AuthenticateRequest()
        {
        }

        /*public AuthenticateRequest(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }*/

        public void Validate()
        {
            if(string.IsNullOrEmpty(UserName))
                throw new InvalidRequestException("This is an invalid request as no user name was supplied");

            if (string.IsNullOrEmpty(Password))
                throw new InvalidRequestException("This is an invalid request as no password was supplied");
        }
    }
}