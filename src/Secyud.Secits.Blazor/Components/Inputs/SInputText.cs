﻿namespace Secyud.Secits.Blazor.Components;

public class SInputText : SMaskableInputBase<string?>
{
    protected override bool TryParseValueFromString(string? value, out string? output)
    {
        output = value;
        return true;
    }
}