using MobilePhonesOntology.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Query;
using VDS.RDF.Writing;

namespace MobilePhonesOntology.Helpers
{
    public static class RdfHelper
    {
        public static Graph CreateGraphOfBrandsAndModels(IEnumerable<PhoneSimple> phones)
        {
            var graphOfBrandsAndModels = new Graph { BaseUri = new Uri("http://example.org/") };

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

                    var relation = graphOfPhones.CreateLiteralNode(field.Name, "en");
                    var modelNode = graphOfPhones.CreateLiteralNode(phone.Model);
                    var propertyNode = graphOfPhones.CreateLiteralNode(propertyNodeValue, "en");

                    graphOfPhones.Assert(new Triple(modelNode, relation, propertyNode));
                }
            }

            return graphOfPhones;
        }

        public static void SaveGraph(Graph graph, string fileName)
        {
            var rdfxmlwriter = new RdfXmlWriter();
            rdfxmlwriter.Save(graph, fileName);
        }

        public static Graph LoadGraph(string fileName)
        {
            var graph = new Graph();
            var xmlParser = new RdfXmlParser();
            xmlParser.Load(graph, fileName);

            return graph;
        }

        public static void LoadGraphIntoServer()
        {
            
        }

        public static void LoadGraphFromServer()
        {
            //First define a SPARQL Endpoint for DBPedia
            var endpoint = new SparqlRemoteEndpoint(new Uri("http://dbpedia.org/sparql"));
            endpoint.QueryWithResultGraph("");

            //Next define our query
            //We're going to ask DBPedia to describe the first thing it finds which is a Person
            var query = "DESCRIBE ?person WHERE {?person a <http://dbpedia.org/ontology/Person>} LIMIT 1";

            //Get the result
            var g = endpoint.QueryWithResultGraph(query);
        }
    }
}