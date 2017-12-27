namespace MobilePhonesOntology.Helpers
{
    public static class Strings
    {
        public static string BrandsAndModelsGraphName = "brandsAndModels.graph";
        public static string PhonesGraphName = "phones.graph";

#if DEBUG
        public static string Domain { get; } = "http://localhost:16273";
#else
        public static string Domain { get; } = "http://54.202.59.212:49155/";
#endif
    }
}