using Microsoft.VisualStudio.TestTools.UnitTesting;
using MobilePhonesOntology.Helpers;
using MobilePhonesOntology.Models.Enums;
using System.Linq;
using VDS.RDF;

namespace Tests
{
    [TestClass]
    public class GraphHelperTests
    {
        public readonly Graph Graph = OntologyHelper.CreateGraphOfBrandsAndModels(DataDownloadHelper.GetAllSimplePhones(), Strings.Domain);

        [TestMethod]
        public void GetBrandFromNode_Succes()
        {
            var graph = OntologyHelper.CreateGraphOfBrandsAndModels(DataDownloadHelper.GetAllSimplePhones(), Strings.Domain);
            var brand = GraphHelper.GetFromNode(graph.Triples.First().Object, NodeName.Brand);

            Assert.IsFalse(string.IsNullOrEmpty(brand));
        }

        [TestMethod]
        public void GetModelFromNode_Succes()
        {
            var model = GraphHelper.GetFromNode(Graph.Triples.First().Subject, NodeName.Model);

            Assert.IsFalse(string.IsNullOrEmpty(model));
        }

        [TestMethod]
        public void GetRelationFromNode_Succes()
        {
            var relation = GraphHelper.GetFromNode(Graph.Triples.First().Predicate, NodeName.Relation);

            Assert.IsFalse(string.IsNullOrEmpty(relation));
        }

        [TestMethod]
        public void GetFromNode_Fail()
        {
            var graph = OntologyHelper.CreateGraphOfBrandsAndModels(DataDownloadHelper.GetAllSimplePhones(), Strings.Domain);
            var node = GraphHelper.GetFromNode(graph.Triples.First().Object, NodeName.Property);
            Assert.IsTrue(string.IsNullOrEmpty(node));

            node = GraphHelper.GetFromNode(graph.Triples.First().Object, NodeName.Model);
            Assert.IsTrue(string.IsNullOrEmpty(node));

            node = GraphHelper.GetFromNode(graph.Triples.First().Object, NodeName.Relation);
            Assert.IsTrue(string.IsNullOrEmpty(node));
        }
    }
}