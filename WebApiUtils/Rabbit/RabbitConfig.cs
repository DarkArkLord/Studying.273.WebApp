namespace WebApiUtils.Rabbit
{
    public static class RabbitConfig
    {
        public static string Server => "rabbitmq";
        public static int Port => 5672;
        public static string User => "guest";
        public static string Password => "guest";

        public static string CloseRentQueue => "dark.lib.rent.close.queue";
    }
}
