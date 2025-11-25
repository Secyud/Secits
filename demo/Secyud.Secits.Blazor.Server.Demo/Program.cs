using Secyud.Secits.Blazor;
using Secyud.Secits.Blazor.Components;
using Secyud.Secits.Blazor.Layout;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddSecitsBlazor();
builder.Services.AddSecitsFontAwesome();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddAdditionalAssemblies(typeof(MainLayout).Assembly);

app.Run();

// void ConfigureServices(IServiceCollection services)
// {
//     services.AddMvc()
//         .AddDataAnnotationsLocalization(options => {
//             options.DataAnnotationLocalizerProvider = (type, factory) =>
//                 factory.Create(typeof(Program));
//         });
// }