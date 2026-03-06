using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Navigation;

public interface IRouterItemGenerator
{
    RouterItem Create(RouteData routeData, Uri uri);
}