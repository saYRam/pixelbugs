using PixelDragons.PixelBugs.Core.Exceptions;

namespace PixelDragons.PixelBugs.Core.Messages
{
    public class AuthenticateRequest : IRequest
    {
        private string userName;
        private string password;
        
        public AuthenticateRequest()
        {
        }

        public AuthenticateRequest(string userName, string password)
        {
            this.userName = userName;
            this.password = password;
        }

        public virtual string Password
        {
            get { return password; }
            set { password = value; }
        }

        public virtual string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        public void Validate()
        {
            if(string.IsNullOrEmpty(userName))
                throw new InvalidRequestException("This is an invalid request as no user name was supplied");

            if (string.IsNullOrEmpty(password))
                throw new InvalidRequestException("This is an invalid request as no password was supplied");
        }
    }
}