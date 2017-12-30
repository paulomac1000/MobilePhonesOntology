using Microsoft.VisualStudio.TestTools.UnitTesting;
using MobilePhonesOntology.Extensions;
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
            var brand = graph.Triples.First().Object.GetFromNode(NodeName.Brand);

            Assert.IsFalse(string.IsNullOrEmpty(brand));
        }

        [TestMethod]
        public void GetModelFromNode_Succes()
        {
            var model = Graph.Triples.First().Subject.GetFromNode(NodeName.Model);

            Assert.IsFalse(string.IsNullOrEmpty(model));
        }

        [TestMethod]
        public void GetRelationFromNode_Succes()
        {
            var relation = Graph.Triples.First().Predicate.GetFromNode(NodeName.Relation);

            Assert.IsFalse(string.IsNullOrEmpty(relation));
        }

        [TestMethod]
        public void GetFromNode_Fail()
        {
            var node = Graph.Triples.First().Object.GetFromNode(NodeName.Property);
            Assert.IsTrue(string.IsNullOrEmpty(node));

            node = Graph.Triples.First().Object.GetFromNode(NodeName.Model);
            Assert.IsTrue(string.IsNullOrEmpty(node));

            node = Graph.Triples.First().Object.GetFromNode(NodeName.Relation);
            Assert.IsTrue(string.IsNullOrEmpty(node));
        }
    }
}