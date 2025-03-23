using NumberToWordConverterService.Constants.Enums;
using NumberToWordConverterService.Services.Interfaces;

namespace NumberToWordConverterService.Types;

public readonly record struct TriadicNumber(int Number, ShortScales Annotation) : ITriadicNumber<int, ShortScales>;
