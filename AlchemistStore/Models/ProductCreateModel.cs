using DataLayer.Entities;
using System.Collections.Generic;

namespace AlchemistStore.Models
{
    public class ProductCreateModel
    {
        public IEnumerable<DElement> Elements { get; set; }
        public string ErrorText { get; set; }
    }
}
