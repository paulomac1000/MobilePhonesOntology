using Microsoft.VisualStudio.TestTools.UnitTesting;
using MobilePhonesOntology.Helpers;
using MobilePhonesOntology.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VDS.RDF;

namespace Tests
{
    [TestClass]
    public class RdfHelperTests
    {
        [TestMethod]
        public void CreateGraphOfBrandsAndModels_Success()
        {
            var graph = RdfHelper.CreateGraphOfBrandsAndModels(DataDownloadHelper.GetAllSimplePhones());
            Assert.IsNotNull(graph);
            Assert.IsTrue(graph.Nodes.Any());
            Assert.IsTrue(graph.Triples.Any());
            Assert.IsTrue(graph.BaseUri.PathAndQuery.Any());
        }

        [TestMethod]
        public async Task CreateGraphOfPhones_Success()
        {
            var brand = new Brand
            {
                Id = 51,
                Name = "Acer",
                Url = "https://www.phonegg.com/brand/51-Acer"
            };

            var phonesSimpleByBrand = DataDownloadHelper.GetPhonesByBrand(brand);
            var phonesByBrand = new List<Phone>();

            foreach (var phoneSimple in phonesSimpleByBrand)
            {
                var phone = await DataDownloadHelper.GetPhone(phoneSimple.Model, phoneSimple.Brand);

                if (phone == null)
                    continue;

                phonesByBrand.Add(phone);
            }

            var graph = RdfHelper.CreateGraphOfPhones(phonesByBrand);
            Assert.IsNotNull(graph);
            Assert.IsTrue(graph.Nodes.Any());
            Assert.IsTrue(graph.Triples.Any());
            Assert.IsTrue(graph.BaseUri.PathAndQuery.Any());
        }
    }
}