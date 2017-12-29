using MobilePhonesOntology.Models;
using System.Collections.Generic;

namespace MobilePhonesOntology.ViewModels
{
    public class FindViewModel : PhoneSimple
    {
        public string Uri { get; set; }

        public IEnumerable<PhoneSimpleWithUri> Phones { get; set; }
    }
}