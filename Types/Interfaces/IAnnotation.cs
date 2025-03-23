namespace NumberToWordConverterService.Types.Interfaces; 

public interface IAnnotation<T> where T : struct {
    T Annotation();
}
