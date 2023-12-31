namespace WebApiUtils.ApiAddresses
{
    public class NamedApiMethods : BaseApiMethods
    {
        public NamedApiMethods(string protocol, string host) : base(protocol, host) { }

        public string GetByName => $"{protocol}://{host}/get-by-name";
    }
}
