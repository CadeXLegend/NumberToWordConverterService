using NumberToWordConverterService.Constants.Enums;
using NumberToWordConverterService.Types;
using NumberToWordConverterService.Types.Interfaces;

namespace NumberToWordConverterService.Services.Interfaces;

public interface IPolyGraphAnalysisService
{
    LongScales GetLongScale(IPolyGraph<TriadicNumber> Graph);
    string ToWords(IPolyGraph<TriadicNumber> Graph, string Notation);
}
