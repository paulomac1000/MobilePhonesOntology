using MobilePhonesOntology.Models;
using System.Collections.Generic;

namespace MobilePhonesOntology.ViewModels
{
    public class PhoneViewModel : PhoneSimple
    {
        public bool Succes { get; set; }
        public string ErrorMessage { get; set; }
        public IEnumerable<TripleSimple> Triples { get; set; }
    }
}