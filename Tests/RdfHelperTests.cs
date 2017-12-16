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
        private string domain = "http://localhost:16273";

        [TestMethod]
        public void CreateGraphOfBrandsAndModels_Success()
        {
            var graph = RdfHelper.CreateGraphOfBrandsAndModels(DataDownloadHelper.GetAllSimplePhones(), domain);
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

            var graph = RdfHelper.CreateGraphOfPhones(phonesByBrand, domain);

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
            var graph = RdfHelper.CreateGraphOfPhones(phones, domain);

            Assert.IsNotNull(graph);
            Assert.IsTrue(graph.Nodes.Any());
            Assert.IsTrue(graph.Triples.Any());
            Assert.IsTrue(graph.BaseUri.PathAndQuery.Any());
        }

        [TestMethod]
        public void SaveAndLoadGraph_Succes()
        {
            const string graphName = "brandsAndModelsGraph.rdf";

            var graph = RdfHelper.CreateGraphOfBrandsAndModels(DataDownloadHelper.GetAllSimplePhones(), domain);
            RdfHelper.SaveGraph(graph, graphName);
            var loadedGraph = RdfHelper.LoadGraph(graphName);

            Assert.AreEqual(graph, loadedGraph);
        }
    }
}