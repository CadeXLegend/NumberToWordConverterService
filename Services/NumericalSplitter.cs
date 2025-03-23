using NumberToWordConverterService.Constants;
using NumberToWordConverterService.Constants.Enums;
using NumberToWordConverterService.Services.Interfaces;
using NumberToWordConverterService.Types;
using NumberToWordConverterService.Types.Interfaces;

namespace NumberToWordConverterService.Services;

public class NumericalSplitter : INumericalSplitter<IPolyGraph<TriadicNumber>>
{
    public Result<IPolyGraph<TriadicNumber>, InvalidRequestTypes> Split(int number)
    {
        if (number < uint.MinValue)
        {
            return new(default, InvalidRequestTypes.NegativeNumber);
        }
        if (number > int.MaxValue)
        {
            return new(default, InvalidRequestTypes.NumberTooBig);
        }

        int digitsInNumber = (int)Math.Floor(Math.Log10(number) + 1);
        int numberOfGroups = number is 0 ? 1 : (digitsInNumber + 2) / 3;
        var polygons = new IPolygon<TriadicNumber>[numberOfGroups];

        for (int groupIndex = 0; groupIndex < numberOfGroups; ++groupIndex)
        {
            if(number is 0)
            {
                polygons[groupIndex] = new Polygon(CreateNode(number, 0, 0));
                break;
            }

            int groupStartPosition = groupIndex * 3;
            int groupSize = Math.Min(3, digitsInNumber - groupStartPosition);

            polygons[groupIndex] = groupSize switch
            {
                1 => new Polygon(CreateNode(number, groupStartPosition, 0)),
                2 => new Polygon(
                    CreateNode(number, groupStartPosition, 0),
                    CreateNode(number, groupStartPosition, 1)
                ),
                3 => new Polygon(
                    CreateNode(number, groupStartPosition, 0),
                    CreateNode(number, groupStartPosition, 1),
                    CreateNode(number, groupStartPosition, 2)
                ),
                _ => default
            };
        }
        PolyGraph graph = new([.. polygons]);
        return new(graph, InvalidRequestTypes.None);
    }

    private static Node CreateNode(int number, int groupStartPosition, int offset)
    {
        if(number is 0)
            return new Node(0, ShortScales.Digit);

        int divider = (int)Math.Pow(10, groupStartPosition + offset);
        int digit = number / divider % 10;
        return new Node(digit, (ShortScales)offset);
    }
}
