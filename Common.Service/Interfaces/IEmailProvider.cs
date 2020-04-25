namespace Common.Service.Interfaces
{
    public interface IEmailProvider
    {
        void SignUp();
        void SignIn();
        void SignOut();
        void SendEmail();
        void GetEmails();
    }
}
