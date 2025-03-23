using System.Collections.Immutable;
using NumberToWordConverterService.Types.Interfaces;

namespace NumberToWordConverterService.Types;

public readonly record struct Polygon : IPolygon<TriadicNumber>
{
    readonly ImmutableArray<INode<TriadicNumber>> Points;
    public ImmutableArray<INode<TriadicNumber>> Nodes() => Points;
    public ImmutableArray<INode<TriadicNumber>> ReversedNodes() => [.. Points.Reverse()];
    public Polygon(INode<TriadicNumber> first) => Points = [first];
    public Polygon(INode<TriadicNumber> first, INode<TriadicNumber> second) => Points = [first, second];
    public Polygon(INode<TriadicNumber> first, INode<TriadicNumber> second, INode<TriadicNumber> third) => Points = [first, second, third];
}
