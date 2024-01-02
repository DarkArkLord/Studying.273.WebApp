namespace WebApiUtils.ApiAddresses
{
    public class RentApiMethods : BaseApiMethods
    {
        public RentApiMethods(string protocol, string host, string port) : base(protocol, host, port) { }

        public string Calculate => $"{protocol}://{host}:{port}/calculate";
        public string Close => $"{protocol}://{host}:{port}/close";
    }
}
