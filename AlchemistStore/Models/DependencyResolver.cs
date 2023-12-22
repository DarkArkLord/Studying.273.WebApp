using DataLayer.Repositories;

namespace AlchemistStore.Models
{
    public static class DependencyResolver
    {
        public static ElementRepository ElementRepository { get; private set; }

        static DependencyResolver()
        {
            ElementRepository = new ElementRepository();
        }
    }
}
