using System.Collections.Generic;

namespace MobilePhonesOntology.ViewModels
{
    public class StatsViewModel
    {
        public int NumberOfBrandAndModels { get; set; }
        public int NumberOfPhones { get; set; }
        public int NumbersOfRelations { get; set; }
        public int NumbersOfUniqueRelations { get; set; }
        public IEnumerable<string> NamesOfRelations { get; set; }
        public int NumbersOfUniqueProperties { get; set; }
    }
}