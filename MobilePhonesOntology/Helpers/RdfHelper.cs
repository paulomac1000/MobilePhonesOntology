using MobilePhonesOntology.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Writing;

namespace MobilePhonesOntology.Helpers
{
    public static class RdfHelper
    {
        public static Graph CreateGraphOfBrandsAndModels(IEnumerable<PhoneSimple> phones, string domain)
        {
            var graphOfBrandsAndModels = new Graph { BaseUri = new Uri(domain) };

            var relation = graphOfBrandsAndModels.CreateUriNode(new Uri($"{domain}/Relation/Index?relation=is"));

            foreach (var phone in phones)
            {
                var modelNode = graphOfBrandsAndModels.CreateUriNode(new Uri($"{domain}/Phone/brand={phone.Brand}&model={phone.Model}"));
                var brandNode = graphOfBrandsAndModels.CreateUriNode(new Uri($"{domain}/Phone/brand={phone.Brand}"));

                graphOfBrandsAndModels.Assert(new Triple(modelNode, relation, brandNode));
            }

            return graphOfBrandsAndModels;
        }

        public static Graph CreateGraphOfPhones(IEnumerable<Phone> phones, string domain)
        {
            var graphOfPhones = new Graph { BaseUri = new Uri(domain) };

            var properties = typeof(Phone).GetTypeInfo().DeclaredProperties.ToArray();

            foreach (var phone in phones)
            {
                foreach (var field in properties)
                {
                    if (field.Name == "Model") continue;

                    var propertyInfo = phone.GetType().GetProperty(field.Name);
                    if (propertyInfo == null) continue;

                    var propertyNodeValue = (string)propertyInfo.GetValue(phone, null);
                    if (string.IsNullOrEmpty(propertyNodeValue)) continue;

                    var relation = graphOfPhones.CreateUriNode(new Uri($"{domain}/Relation/Index?relation={field.Name}"));
                    var modelNode = graphOfPhones.CreateUriNode(new Uri($"{domain}/Phone/Index?brand={phone.Brand}&model={phone.Model}"));
                    var propertyNode = graphOfPhones.CreateUriNode(new Uri($"{domain}/Property/Index?property={propertyNodeValue}&model={phone.Model}"));

                    graphOfPhones.Assert(new Triple(modelNode, relation, propertyNode));
                }
            }

            return graphOfPhones;
        }

        public static void SaveGraph(Graph graph, string fileName)
        {
            var writer = new Notation3Writer();
            writer.Save(graph, fileName);
        }

        public static Graph LoadGraph(string fileName)
        {
            var graph = new Graph();
            var parser = new Notation3Parser();
            parser.Load(graph, fileName);

            return graph;
        }
    }
}