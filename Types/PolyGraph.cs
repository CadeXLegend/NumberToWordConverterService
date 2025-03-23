using System.Collections.Immutable;
using NumberToWordConverterService.Types.Interfaces;

namespace NumberToWordConverterService.Types;

public readonly record struct PolyGraph(ImmutableArray<IPolygon<TriadicNumber>> Polys) : IPolyGraph<TriadicNumber>
{
    public ImmutableArray<IPolygon<TriadicNumber>> Polygons() => Polys;
}
