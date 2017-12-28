using MobilePhonesOntology.Models.Enums;
using System.Text.RegularExpressions;
using VDS.RDF;

namespace MobilePhonesOntology.Helpers
{
    public static class GraphHelper
    {
        public static string GetFromNode(INode node, NodeName nodeName)
        {
            var name = nodeName.ToString().ToLower();

            var input = node.ToString();
            var expression = new Regex($@"({name}=)(?<{name}>[^&]+)");
            var match = expression.Match(input);
            return match.Success ? match.Groups[name].Value.Replace("%20", " ") : string.Empty;
        }
    }
}