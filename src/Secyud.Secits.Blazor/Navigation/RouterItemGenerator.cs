using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Navigation;

public class RouterItemGenerator : IRouterItemGenerator
{
    protected virtual string GetRouterItemName(RouteData routeData, Uri uri)
    {
        return routeData.PageType.ToString();
    }

    public virtual RouterItem Create(RouteData routeData, Uri uri)
    {
        var result = new RouterItem
        {
            Uri = uri,
            DisplayName = GetRouterItemName(routeData, uri),
            Content = builder =>
            {
                builder.OpenComponent(0, routeData.PageType);
                foreach (var routeValue in routeData.RouteValues)
                    builder.AddComponentParameter(1, routeValue.Key, routeValue.Value);
                builder.CloseComponent();
            }
        };

        return result;
    }
}