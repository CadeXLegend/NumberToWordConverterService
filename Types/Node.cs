using NumberToWordConverterService.Constants.Enums;
using NumberToWordConverterService.Types.Interfaces;

namespace NumberToWordConverterService.Types; 

public readonly record struct Node(int Number, ShortScales Scale) : INode<TriadicNumber> {
    readonly TriadicNumber Value = new(Number, Scale);
    public TriadicNumber Entity() => Value;
}
