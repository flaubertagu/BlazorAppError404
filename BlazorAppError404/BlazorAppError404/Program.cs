using BlazorAppError404.Client.Helpers;
using BlazorAppError404.Components;
using BlazorHub404.Hubs;
using SharedConfig;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// Add services to the container.
services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

//SignalR config -- START
services.AddHttpClient(Config.CorsDefinition, op =>
{
    op.BaseAddress = new Uri(Config.HubAddressHttps);
});

//services.AddControllers();
services
    .AddSignalR(conf =>
    {
        conf.MaximumReceiveMessageSize = null;
    })
    .AddHubOptions<ChatHub>(config => config.EnableDetailedErrors = true);
services.AddCors(options => options.AddPolicy(Config.CorsDefinition, builder =>
{
    builder
    .AllowAnyMethod()
    .AllowAnyHeader()
    .WithOrigins(Config.HubAddressHttps);
}));
services.AddScoped<SignalRHub>();
//SignalR config -- STOP

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.UseCors(Config.CorsDefinition);
app.MapHub<ChatHub>(Config.HubDefinition);

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(BlazorAppError404.Client._Imports).Assembly);

app.UseStatusCodePagesWithRedirects("/404");

app.Run();
