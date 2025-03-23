namespace NumberToWordConverterService.Types.Interfaces; 

public interface INode<T> where T : struct {
    T Entity();
}
