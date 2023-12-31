namespace WebApiUtils.ApiAddresses
{
    public class BaseApiMethods
    {
        protected string protocol;
        protected string host;
        protected string port;

        public BaseApiMethods(string protocol, string host, string port)
        {
            this.protocol = protocol;
            this.host = host;
            this.port = port;
        }

        public string GetAll => $"{protocol}://{host}:{port}/get-all";
        public string GetById => $"{protocol}://{host}:{port}/get-by-id";
        public string Add => $"{protocol}://{host}:{port}/add";
        public string Update => $"{protocol}://{host}:{port}/update";
    }
}
