using Microsoft.VisualStudio.TestTools.UnitTesting;
using MobilePhonesOntology.Helpers;
using System.Linq;

namespace Tests
{
    [TestClass]
    public class DataDownloadHelperTests
    {
        [TestMethod]
        public void GetAllBrandsSucces()
        {
            var brands = DataDownloadHelper.GetAllBrands();

            Assert.IsNotNull(brands);
            Assert.IsTrue(brands.Any());
        }

        [TestMethod]
        public void GetAllPhones()
        {
            
        }
    }
}