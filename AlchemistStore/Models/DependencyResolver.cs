using DataLayer.Repositories;
using LogicLayer;

namespace AlchemistStore.Models
{
    public static class DependencyResolver
    {
        private static ElementRepository elementRepository;

        public static ElementsService ElementsService { get; private set; }

        static DependencyResolver()
        {
            elementRepository = new ElementRepository();

            ElementsService = new ElementsService(elementRepository);
        }
    }
}
