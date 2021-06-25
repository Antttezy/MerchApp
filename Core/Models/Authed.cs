namespace Domain.Core.Models
{
    public class Authed<T> : Authed
    {
        public T InnerData { get; set; }
    }

    public class Authed
    {
        public string Login { get; set; } = "";

        public string Password { get; set; } = "";
    }
}
