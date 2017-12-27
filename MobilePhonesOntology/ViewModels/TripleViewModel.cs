using VDS.RDF;

namespace MobilePhonesOntology.ViewModels
{
    public class TripleViewModel
    {
        public string Subject { get; set; }
        public string SubjectUri { get; set; }

        public string Predicate { get; set; }
        public string PredicateUri { get; set; }

        public string Object { get; set; }
        public string ObjectUri { get; set; }
    }
}