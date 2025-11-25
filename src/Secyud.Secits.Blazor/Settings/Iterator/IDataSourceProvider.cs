namespace Secyud.Secits.Blazor.Settings;

/// <summary>
/// the provider for data source.
/// </summary>
/// <typeparam name="TValue"></typeparam>
public interface IDataSourceProvider<TValue>: IIsPlugin
{
    Task<DataResult<TValue>> GetDataAsync(DataRequest request);
}