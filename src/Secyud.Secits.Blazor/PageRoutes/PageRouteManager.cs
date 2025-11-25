using System.Reflection;
using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.PageRoutes;

public class PageRouteManager
{
    public EventHandler? RouterItemActivated;
    public EventHandler? RouterItemRemoved;
    private readonly List<PageRouteItem> _items = [];
    private PageRouteItem? _currentItem;

    public PageRouteItem? CurrentItem => _currentItem;
    public IReadOnlyList<PageRouteItem> Items => _items.AsReadOnly();

    public void ActivatePageRouteItem(RouteData routeData, string uriString)
    {
        if (!Uri.TryCreate(uriString, UriKind.Absolute, out var uri)) return;
        _currentItem = _items.FirstOrDefault(u => u.Uri == uri);
        if (_currentItem is null)
        {
            _currentItem = CreatePageRouterItem(routeData, uri);
            _items.Add(_currentItem);
        }

        RouterItemActivated?.Invoke(this, EventArgs.Empty);
    }

    public void RemovePageRouteItem(PageRouteItem item)
    {
        var index = _items.IndexOf(item);
        if (index < 0) return;
        _items.RemoveAt(index);
        if (_currentItem == item)
        {
            _currentItem = _items.Count > 0 ? _items[Math.Max(0, index - 1)] : null;
        }

        RouterItemRemoved?.Invoke(this, EventArgs.Empty);
    }

    private PageRouteItem CreatePageRouterItem(RouteData routeData, Uri uri)
    {
        var pageType = routeData.PageType;
        var routeValues = routeData.RouteValues;
        var pageParameters = routeData.RouteValues
            .Where(u => u.Value is not null)
            .Select(u => new KeyValuePair<string, object>(u.Key, u.Value!)).ToList();
        var result = new PageRouteItem(uri, pageType, pageParameters);
        var attribute = pageType.GetCustomAttribute<PageRouteAttribute>();
        if (attribute is null) return result;
        if (attribute.Name is not null)
            result.Name = attribute.Name;
        result.ResourceType = attribute.ResourceType;

        var len = Math.Min(attribute.Parameters.Length, attribute.ParameterPrefixes.Length);
        var parameters = new string[len];
        for (var i = 0; i < len; i++)
        {
            var parameter = attribute.Parameters[i];
            var prefix = attribute.ParameterPrefixes[i];
            if (routeValues.TryGetValue(parameter, out var parameterValue))
            {
                parameters[i] = $"{prefix}{parameterValue}";
            }
            else
            {
                parameters[i] = string.Empty;
            }
        }

        result.Parameters = parameters;
        return result;
    }
}