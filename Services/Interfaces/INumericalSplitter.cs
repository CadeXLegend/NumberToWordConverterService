using NumberToWordConverterService.Constants;
using NumberToWordConverterService.Types;

namespace NumberToWordConverterService.Services.Interfaces;

public interface INumericalSplitter<T> {
    Result<T, InvalidRequestTypes> Split(int number);
}
