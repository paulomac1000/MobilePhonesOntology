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

                if(brandsAndModels == null)
                    throw new Exception("BrandsAndModels graph is not updated yet.");

                return brandsAndModels;
            }
            set
            {
                lockBrandsAndModels = true;
                brandsAndModels = value;
                lockBrandsAndModels = false;
                OntologyHelper.SaveGraph(brandsAndModels, Strings.BrandsAndModelsGraphName);
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

                if (phones == null)
                    throw new Exception("Phones graph is not updated yet.");

                return phones;
            }
            set
            {
                lockPhones = true;
                phones = value;
                lockPhones = false;
                OntologyHelper.SaveGraph(phones, Strings.PhonesGraphName);
            }
        }
    }
}