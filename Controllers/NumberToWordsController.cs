using Microsoft.AspNetCore.Mvc;
using NumberToWordConverterService.Constants;
using NumberToWordConverterService.Models;
using NumberToWordConverterService.Services.Interfaces;
using NumberToWordConverterService.Types;
using NumberToWordConverterService.Types.Interfaces;

namespace NumberToWordConverterService.Controllers;

[ApiController]
[Route("number-to-words")]
public class NumberToWordsController(
    INumericalSplitter<IPolyGraph<TriadicNumber>> NumericalSplitter,
    IPolyGraphAnalysisService PolyGraphAnalysisService) : ControllerBase
{

    [HttpPost]
    public IActionResult Get([FromBody] NumberToWordsRequest request)
    {
        var splitNumbersResult = NumericalSplitter.Split(request.Number);
        HttpContext.Items["Outcome"] = splitNumbersResult.Outcome;

        if (splitNumbersResult.Outcome is not InvalidRequestTypes.None)
        {
            string outcome = InvalidRequestResponses.Messages[splitNumbersResult.Outcome];
            return BadRequest(outcome);
        }

        return Ok(PolyGraphAnalysisService.ToWords(splitNumbersResult.Value));
    }
}
