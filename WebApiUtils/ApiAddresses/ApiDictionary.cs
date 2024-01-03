namespace WebApiUtils.ApiAddresses
{
    public static class ApiDictionary
    {
        private static string protocol = "https";
        private static string port = "8081";

        public static NamedApiMethods AuthorApi { get; private set; } = new NamedApiMethods(protocol, "author_api", port);
        public static NamedApiMethods BookSeriesApi { get; private set; } = new NamedApiMethods(protocol, "bookseries_api", port);
        public static NamedApiMethods BranchApi { get; private set; } = new NamedApiMethods(protocol, "branch_api", port);
        public static NamedApiMethods ClientApi { get; private set; } = new NamedApiMethods(protocol, "client_api", port);
        public static NamedApiMethods LibratianApi { get; private set; } = new NamedApiMethods(protocol, "librarian_api", port);

        public static NamedApiMethods BookApi { get; private set; } = new NamedApiMethods(protocol, "book_api", port);
        public static RentApiMethods BookRentApi { get; private set; } = new RentApiMethods(protocol, "bookrent_api", port);
    }
}
