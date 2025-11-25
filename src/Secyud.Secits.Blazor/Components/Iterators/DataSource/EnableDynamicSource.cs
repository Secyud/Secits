using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public class EnableDynamicSource<TValue> : SPluginBase<SIteratorBase<TValue>>, IDataSourceProvider<TValue>
{
    [Parameter]
    public Func<DataRequest, Task<DataResult<TValue>>>? Items { get; set; }

    [Parameter]
    public Func<Exception, Task>? ErrorHandler { get; set; }

    protected override void ApplySetting()
    {
        Master.DataSource.Apply(this);
    }

    protected override void ForgoSetting()
    {
        Master.DataSource.Forgo(this);
    }

    public async Task<DataResult<TValue>> GetDataAsync(DataRequest request)
    {
        DataResult<TValue> result;
        try
        {
            result = Items is null
                ? new DataResult<TValue>()
                : await Items.Invoke(request);
        }
        catch (Exception e)
        {
            if (ErrorHandler is null)
                throw;
            await ErrorHandler.Invoke(e);
            result = new DataResult<TValue>();
        }

        return result;
    }
}