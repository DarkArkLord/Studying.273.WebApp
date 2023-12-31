namespace WebApiUtils.ApiAddresses
{
    public static class ApiDictionary
    {
        private static string protocol = "https";

        public static NamedApiMethods AuthorApi { get; private set; } = new NamedApiMethods(protocol, "author_api");
        public static NamedApiMethods BookSeriesApi { get; private set; } = new NamedApiMethods(protocol, "bookseries_api");
        public static NamedApiMethods BranchApi { get; private set; } = new NamedApiMethods(protocol, "branch_api");
        public static NamedApiMethods ClientApi { get; private set; } = new NamedApiMethods(protocol, "client_api");
        public static NamedApiMethods LibratianApi { get; private set; } = new NamedApiMethods(protocol, "librarian_api");
    }
}
