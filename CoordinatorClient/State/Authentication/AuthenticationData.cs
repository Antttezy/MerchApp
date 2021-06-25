namespace CoordinatorClient.State.Authentication
{
    public class AuthenticationData
    {
        public static readonly AuthenticationData Instance = new AuthenticationData();

        public string Login { get; set; } = "";
        public string Password { get; set; } = "";

        private AuthenticationData()
        {

        }
    }
}
