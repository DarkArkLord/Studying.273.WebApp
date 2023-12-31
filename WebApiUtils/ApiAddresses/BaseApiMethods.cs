namespace WebApiUtils.ApiAddresses
{
    public class BaseApiMethods
    {
        protected string protocol;
        protected string host;

        public BaseApiMethods(string protocol, string host)
        {
            this.protocol = protocol;
            this.host = host;
        }

        public string GetAll => $"{protocol}://{host}/get-all";
        public string GetById => $"{protocol}://{host}/get-by-id";
        public string Add => $"{protocol}://{host}/add";
        public string Update => $"{protocol}://{host}/update";
    }
}
