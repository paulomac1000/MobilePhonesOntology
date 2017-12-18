using System;
using VDS.RDF;

namespace MobilePhonesOntology.Helpers
{
    public static class CacheHelper
    {
        private static Graph brandsAndModels { get; set; }
        private static bool LockBrandsAndModels { get; set; } = false;

        public static Graph BrandsAndModels
        {
            get
            {
                if (LockBrandsAndModels)
                    throw new Exception("Wait when BrandsAndModels graph is updating.");
                else
                    return brandsAndModels;
            }
            set
            {
                LockBrandsAndModels = true;
                brandsAndModels = value;
                LockBrandsAndModels = false;
            }
        }

        private static Graph phones { get; set; }
        private static bool lockPhones { get; set; } = false;

        public static Graph Phones
        {
            get
            {
                if (lockPhones)
                    throw new Exception("Wait when Phones graph is updating.");
                else
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