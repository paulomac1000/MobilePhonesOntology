using MobilePhonesOntology.Models.Enums;
using System.Text.RegularExpressions;
using VDS.RDF;

namespace MobilePhonesOntology.Extensions
{
    public static class INodeExtensions
    {
        public static string GetFromNode(this INode source, NodeName nodeName)
        {
            var name = nodeName.ToString().ToLower();

            var input = source.ToString();
            var expression = new Regex($@"({name}=)(?<{name}>[^&]+)");
            var match = expression.Match(input);
            return match.Success ? match.Groups[name].Value.Replace("%20", " ") : string.Empty;
        }
    }
}