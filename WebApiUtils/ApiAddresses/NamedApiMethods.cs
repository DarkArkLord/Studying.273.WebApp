namespace WebApiUtils.ApiAddresses
{
    public class NamedApiMethods : BaseApiMethods
    {
        public NamedApiMethods(string protocol, string host, string port) : base(protocol, host, port) { }

        public string GetByName => $"{protocol}://{host}:{port}/get-by-name";
    }
}
