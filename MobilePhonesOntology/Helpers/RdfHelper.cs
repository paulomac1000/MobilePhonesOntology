using System;
using MobilePhonesOntology.Models;
using System.Collections.Generic;
using VDS.RDF;

namespace MobilePhonesOntology.Helpers
{
    public static class RdfHelper
    {
        public static Graph CreateGraphOfBrandsAndModels(IEnumerable<PhoneSimple> phones)
        {
            var graphOfBrandsAndModels = new Graph {BaseUri = new Uri("http://example.org/")};

            var relation = graphOfBrandsAndModels.CreateLiteralNode("is", "en");

            foreach (var phone in phones)
            {
                var modelNode = graphOfBrandsAndModels.CreateLiteralNode(phone.Model);
                var brandNode = graphOfBrandsAndModels.CreateLiteralNode(phone.Brand, "en");

                graphOfBrandsAndModels.Assert(new Triple(modelNode, relation, brandNode));
            }

            return graphOfBrandsAndModels;
        }

        public static Graph CreateGraphOfPhones(IEnumerable<Phone> phones)
        {
            var graphOfPhones = new Graph { BaseUri = new Uri("http://example.org/") };

            var fields = typeof(Phone).GetFields();
            foreach (var phone in phones)
            {
                foreach (var field in fields)
                {
                    if (field.Name == "Model") continue;

                    var literalNodeValue = (string)phone.GetType().GetProperty(field.Name).GetValue(typeof(string), null);

                    if (string.IsNullOrEmpty(literalNodeValue)) continue;

                    var relation = graphOfPhones.CreateLiteralNode(field.Name, "en");

                    var uriNode = graphOfPhones.CreateUriNode(phone.Model);

                    var literalNode = graphOfPhones.CreateLiteralNode(literalNodeValue, "en");

                    graphOfPhones.Assert(new Triple(uriNode, relation, literalNode));
                }
            }

            return graphOfPhones;
        }
    }
}