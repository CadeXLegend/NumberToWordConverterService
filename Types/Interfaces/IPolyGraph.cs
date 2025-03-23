using System.Collections.Immutable;

namespace NumberToWordConverterService.Types.Interfaces; 

public interface IPolyGraph<T> where T : struct {
    ImmutableArray<IPolygon<T>> Polygons();
}
