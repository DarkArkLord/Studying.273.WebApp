namespace Entities
{
    public class DUser
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string PaswordHash { get; set; }
        public int? LibrarianId { get; set; }
    }
}
