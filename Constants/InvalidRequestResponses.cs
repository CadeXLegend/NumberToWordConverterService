using System.Collections.Immutable;

namespace NumberToWordConverterService.Constants;

public enum InvalidRequestTypes : int
{
    None,
    NegativeNumber,
    NumberTooBig,
}

public static class InvalidRequestResponses
{
    public static readonly ImmutableDictionary<InvalidRequestTypes, string> Messages = ImmutableDictionary.CreateRange(
        [
            KeyValuePair.Create(InvalidRequestTypes.NegativeNumber, "Only positive numbers are currently supported"),
            KeyValuePair.Create(InvalidRequestTypes.NumberTooBig, "Only numbers within the Max range of a 32-bit integer are currently supported"),
        ]);
}
