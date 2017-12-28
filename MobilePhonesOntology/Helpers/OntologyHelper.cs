using MobilePhonesOntology.Models;
using MobilePhonesOntology.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Writing;

namespace MobilePhonesOntology.Helpers
{
    public static class OntologyHelper
    {
        public static Graph CreateGraphOfBrandsAndModels(IEnumerable<PhoneSimple> phones, string domain)
        {
            var graphOfBrandsAndModels = new Graph { BaseUri = new Uri(domain) };

            var relation = graphOfBrandsAndModels.CreateUriNode(new Uri($"{domain}/Relation/Index?{NodeName.Relation.ToString().ToLower()}=is"));

            foreach (var phone in phones)
            {
                var modelNode = graphOfBrandsAndModels.CreateUriNode(new Uri(
                    $"{domain}/Phone/Index?{NodeName.Brand.ToString().ToLower()}={phone.Brand}&{NodeName.Model.ToString().ToLower()}={phone.Model}"
                ));
                var brandNode = graphOfBrandsAndModels.CreateUriNode(new Uri(
                    $"{domain}/Phone/Index?{NodeName.Brand.ToString().ToLower()}={phone.Brand}"
                ));

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

                    var phoneNode = graphOfPhones.CreateUriNode(new Uri(
                        $"{domain}/Phone/Index?{NodeName.Brand.ToString().ToLower()}={phone.Brand}&{NodeName.Model.ToString().ToLower()}={phone.Model}"
                    ));
                    var relation = graphOfPhones.CreateUriNode(new Uri(
                        $"{domain}/Relation/Index?{NodeName.Relation.ToString().ToLower()}={field.Name}"
                    ));
                    var propertyNode = graphOfPhones.CreateUriNode(new Uri(
                        $"{domain}/Property/Index?{NodeName.Property.ToString().ToLower()}={propertyNodeValue}"
                    ));

                    graphOfPhones.Assert(new Triple(phoneNode, relation, propertyNode));
                }
            }

            return graphOfPhones;
        }

        public static void SaveGraph(Graph graph, string fileName)
        {
            var writer = new Notation3Writer();
            var path = AppDomain.CurrentDomain.BaseDirectory;
            writer.Save(graph, path + fileName);
        }

        public static Graph LoadGraph(string fileName)
        {
            var graph = new Graph();
            var parser = new Notation3Parser();
            var path = AppDomain.CurrentDomain.BaseDirectory;
            try
            {
                parser.Load(graph, path + fileName);
            }
            catch
            {
                // ignored
            }

            return graph;
        }
    }
}