using Microsoft.VisualStudio.TestTools.UnitTesting;
using MobilePhonesOntology.Helpers;
using System.Linq;

namespace Tests
{
    [TestClass]
    public class RdfHelperTests
    { 
        [TestMethod]
        public void TestLibrary()
        {
            RdfHelper.Test();
        }
    }
}