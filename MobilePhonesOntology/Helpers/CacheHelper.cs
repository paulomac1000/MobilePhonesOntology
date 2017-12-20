using System;
using VDS.RDF;

namespace MobilePhonesOntology.Helpers
{
    public static class CacheHelper
    {
        private static Graph brandsAndModels { get; set; }
        private static bool lockBrandsAndModels { get; set; }

        public static Graph BrandsAndModels
        {
            get
            {
                if (lockBrandsAndModels)
                    throw new Exception("Wait when BrandsAndModels graph is updating.");

                return brandsAndModels;
            }
            set
            {
                lockBrandsAndModels = true;
                brandsAndModels = value;
                lockBrandsAndModels = false;
            }
        }

        private static Graph phones { get; set; }
        private static bool lockPhones { get; set; }

        public static Graph Phones
        {
            get
            {
                if (lockPhones)
                    throw new Exception("Wait when Phones graph is updating.");

                return phones;
            }
            set
            {
                lockPhones = true;
                phones = value;
                lockPhones = false;
            }
        }
    }
}