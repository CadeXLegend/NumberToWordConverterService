using System.Text;
using NumberToWordConverterService.Constants.Enums;
using NumberToWordConverterService.Services.Interfaces;
using NumberToWordConverterService.Types;
using NumberToWordConverterService.Types.Interfaces;

namespace NumberToWordConverterService.Services;

public class PolyGraphAnalysisService : IPolyGraphAnalysisService
{
    public LongScales GetLongScale(IPolyGraph<TriadicNumber> Graph) => (LongScales)Graph.Polygons().Length;

    public string ToWords(IPolyGraph<TriadicNumber> Graph)
    {
        var result = new StringBuilder();
        var polygons = Graph.Polygons();
        var scale = GetLongScale(Graph);
        bool needsAnd = true;

        int polygonLength = polygons.Length;
        for (int i = polygonLength - 1; i >= 0; --i)
        {
            var nodes = polygons[i].Nodes();
            string polygonWords = polygonLength is 1 && nodes.Length is 1 && nodes[0].Entity().Number is 0 ?
                $"{(Digits)nodes[0].Entity().Number}" : PolygonToWords(polygons[i]);

            result.Append(polygonWords);

            bool areCurrentNodesZero = nodes.Length is not 3 && nodes.All(n => n.Entity().Number is 0);
            if (areCurrentNodesZero)
                continue;

            bool isLongScaleGroup = scale is not LongScales.Hundred && i > 0;
            LongScales currentLongScale = (LongScales)i + 1;
            if (isLongScaleGroup)
            {
                needsAnd = true;
                result.Append(' ').Append(currentLongScale.ToString());
            }

            if (needsAnd && i < 1 &&
                polygonLength > 1 &&
                currentLongScale is not LongScales.Hundred)
            {
                result.Append(" And ");
                needsAnd = false;
            }
            else
            {
                if (i > 0)
                    result.Append(' ');

                needsAnd = true;
            }
        }

        return result.ToString().Trim();
    }

    private static string PolygonToWords(IPolygon<TriadicNumber> polygon)
    {
        var nodes = polygon.Nodes();
        return nodes.Length switch
        {
            1 => SingleDigitToWords((Node)nodes[0]),
            2 => TwoDigitsToWords((Node)nodes[0], (Node)nodes[1]),
            3 => ThreeDigitsToWords((Node)nodes[0], (Node)nodes[1], (Node)nodes[2]),
            _ => default,
        };
    }

    private static string SingleDigitToWords(Node node)
    {
        return node.Number is 0 ? string.Empty : $"{(Digits)node.Number}";
    }

    private static string TwoDigitsToWords(Node ones, Node tens)
    {
        int number = (tens.Number * 10) + ones.Number;
        if (number is 0)
            return string.Empty;
        if (number < 9)
            return SingleDigitToWords(ones);
        if (number < 20)
            return $"{(Teens)number}";
        if (ones.Number is 0)
            return $"{(Tens)tens.Number}";
        return $"{(Tens)tens.Number}-{(Digits)ones.Number}";
    }

    private static string ThreeDigitsToWords(Node ones, Node tens, Node hundreds)
    {
        if (hundreds.Number + tens.Number + ones.Number is 0)
            return string.Empty;

        var result = new StringBuilder();
        if (hundreds.Number > 0)
            result.Append($"{(Digits)hundreds.Number} Hundred");

        string twoDigits = TwoDigitsToWords(ones, tens);
        if (!string.IsNullOrEmpty(twoDigits))
        {
            if (result.Length > 0)
                result.Append(" And ");
            result.Append(twoDigits);
        }

        return result.ToString();
    }
}
