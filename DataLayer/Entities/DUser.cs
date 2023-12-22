namespace DataLayer.Entities
{
    public class DUser
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string PassHash { get; set; }
        public EUserType UserType { get; set; }
    }
}
