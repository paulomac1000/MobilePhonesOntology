using Microsoft.VisualStudio.TestTools.UnitTesting;
using MobilePhonesOntology.Helpers;
using MobilePhonesOntology.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class RdfHelperTests
    {
        [TestMethod]
        public void CreateGraphOfBrandsAndModels_Success()
        {
            var graph = OntologyHelper.CreateGraphOfBrandsAndModels(DataDownloadHelper.GetAllSimplePhones(), Strings.Domain);
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

            var graph = OntologyHelper.CreateGraphOfPhones(phonesByBrand, Strings.Domain);

            Assert.IsNotNull(graph);
            Assert.IsTrue(graph.Nodes.Any());
            Assert.IsTrue(graph.Triples.Any());
            Assert.IsTrue(graph.BaseUri.PathAndQuery.Any());
        }

        [Ignore]
        [TestMethod]
        public async Task CreateGraphOfAllPhones_Success()
        {
            var phones = (await DataDownloadHelper.GetAllPhones()).ToList();
            var graph = OntologyHelper.CreateGraphOfPhones(phones, Strings.Domain);

            Assert.IsNotNull(graph);
            Assert.IsTrue(graph.Nodes.Any());
            Assert.IsTrue(graph.Triples.Any());
            Assert.IsTrue(graph.BaseUri.PathAndQuery.Any());
        }

        [TestMethod]
        public void SaveAndLoadGraph_Succes()
        {
            var graph = OntologyHelper.CreateGraphOfBrandsAndModels(DataDownloadHelper.GetAllSimplePhones(), Strings.Domain);
            OntologyHelper.SaveGraph(graph, Strings.BrandsAndModelsGraphName);
            var loadedGraph = OntologyHelper.LoadGraph(Strings.BrandsAndModelsGraphName);

            Assert.AreEqual(graph, loadedGraph);
        }

        [Ignore]
        [TestMethod]
        public void SaveAndLoadBigGraph_Succes()
        {
            var graph = OntologyHelper.CreateGraphOfBrandsAndModels(DataDownloadHelper.GetAllSimplePhones(), Strings.Domain);
            OntologyHelper.SaveGraph(graph, Strings.PhonesGraphName);
            var loadedGraph = OntologyHelper.LoadGraph(Strings.PhonesGraphName);

            Assert.AreEqual(graph, loadedGraph);
        }
    }
}