using System.Collections.Immutable;

namespace NumberToWordConverterService.Types.Interfaces; 

public interface IPolygon<T> where T : struct {
    ImmutableArray<INode<T>> Nodes();
    ImmutableArray<INode<T>> ReversedNodes();
}
