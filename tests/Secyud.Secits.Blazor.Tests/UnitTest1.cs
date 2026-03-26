using System.Text.Json;
using Xunit.Abstractions;

namespace Secyud.Secits.Blazor;

public class UnitTest1(ITestOutputHelper output)
{
    [Fact]
    public void Test1()
    {
        var option = new SOverlayOptions
        {
            ControlType = SOverlayControlType.Hover,
            HorizontalInterval = 1
        };
        output.WriteLine(JsonSerializer.Serialize(option));
    }
}