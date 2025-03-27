using System.Text;
using NumberToWordConverterService.Constants.Enums;
using NumberToWordConverterService.Services.Interfaces;
using NumberToWordConverterService.Types;
using NumberToWordConverterService.Types.Interfaces;

namespace NumberToWordConverterService.Services;

public class PolyGraphAnalysisService : IPolyGraphAnalysisService
{
    public LongScales GetLongScale(IPolyGraph<TriadicNumber> Graph) => (LongScales)Graph.Polygons().Length;

    public string ToWords(IPolyGraph<TriadicNumber> Graph, string Notation = "")
    {
        List<string> sentence = [];
        var polygons = Graph.Polygons();
        var scale = GetLongScale(Graph);

        int polygonLength = polygons.Length;
        for (int i = polygonLength - 1; i >= 0; --i)
        {
            bool isLongScaleGroup = scale is not LongScales.Hundred && i > 0;
            string longScale = isLongScaleGroup ? $" {(LongScales)i + 1}" : string.Empty;
            var nodes = polygons[i].Nodes();

            // if the only number is 0, then the word is Zero, ez, get out
            // otherwise, let's transform the polygons into words
            bool isThereOnlyZero = polygonLength is 1 && nodes.Length is 1 && nodes[0].Entity().Number is 0;
            string polygonWords =  isThereOnlyZero ? 
                $"{(Digits)nodes[0].Entity().Number}" : 
                PolygonToWords(polygons[i], longScale);

            // We only wanna add the And delimiter if the last
            // triadic number group isn't all zeros (0, 00, or 000)
            // and isn't a part of a long scale group
            // and the hundreds position number is Zero
            // meaning it's either a ten, teen, or digit
            bool isLastNodeZeroInTriad = nodes.Length == 3 && nodes[2].Entity().Number == 0;
            bool allNodesAreNotZero = nodes.Select(n => n.Entity().Number).Sum() > 0;
            string finalDelimiter = allNodesAreNotZero && !isLongScaleGroup && isLastNodeZeroInTriad ? "And " : string.Empty;
            
            if(!allNodesAreNotZero && !isThereOnlyZero)
                continue;

            sentence.Add($"{finalDelimiter}{polygonWords}");
        }

        if(!string.IsNullOrEmpty(Notation))
            sentence.Add(Notation);

        return string.Join(' ', sentence);
    }

    private static string PolygonToWords(IPolygon<TriadicNumber> polygon, string longScale)
    {
        var nodes = polygon.Nodes();
        return nodes.Length switch
        {
            1 => SingleDigitToWords((Node)nodes[0], longScale),
            2 => TwoDigitsToWords((Node)nodes[0], (Node)nodes[1], longScale),
            3 => ThreeDigitsToWords((Node)nodes[0], (Node)nodes[1], (Node)nodes[2], longScale),
            _ => default,
        };
    }

    private static string SingleDigitToWords(Node node, string longScale)
    {

        return node.Number is 0 ? string.Empty : $"{(Digits)node.Number}{longScale}";
    }

    private static string TwoDigitsToWords(Node ones, Node tens, string longScale)
    {
        int number = (tens.Number * 10) + ones.Number;
        if (number is 0)
            return string.Empty;
        if (number < 9)
            return SingleDigitToWords(ones, longScale);
        if (number < 20)
            return $"{(Teens)number}{longScale}";
        if (ones.Number is 0)
            return $"{(Tens)tens.Number}";
        return $"{(Tens)tens.Number}-{(Digits)ones.Number}{longScale}";
    }

    private static string ThreeDigitsToWords(Node ones, Node tens, Node hundreds, string longScale)
    {
        if (hundreds.Number + tens.Number + ones.Number is 0)
            return string.Empty;

        var result = new StringBuilder();
        if (hundreds.Number > 0)
            result.Append($"{(Digits)hundreds.Number} {ShortScales.Hundred}");

        string twoDigits = TwoDigitsToWords(ones, tens, string.Empty);
        if (!string.IsNullOrEmpty(twoDigits))
        {
            if (result.Length > 0)
                result.Append(" And ");
            result.Append(twoDigits);
        }

        result.Append(longScale);

        return result.ToString();
    }
}
