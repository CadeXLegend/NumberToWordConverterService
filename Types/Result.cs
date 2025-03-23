namespace NumberToWordConverterService.Types;

public readonly record struct Result<T, InvalidRequestTypes>(T Value, InvalidRequestTypes Outcome);
