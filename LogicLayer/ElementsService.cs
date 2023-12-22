using DataLayer.Entities;
using DataLayer.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace LogicLayer
{
    public class ElementsService
    {
        private ElementRepository elementRepository;

        public ElementsService(ElementRepository elementRepository)
        {
            this.elementRepository = elementRepository;
        }

        public IEnumerable<DElement> GetAll()
        {
            return elementRepository.GetAll().ToArray();
        }
    }
}
