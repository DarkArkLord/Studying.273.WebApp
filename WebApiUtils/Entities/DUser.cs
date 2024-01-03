namespace WebApiUtils.Entities
{
    public class DUser : DEntityIdName
    {
        public int PasswordHash { get; set; }
    }

    public static class DarkLibAuthInfo
    {
        public static string UserRole => "master";
    }
}
