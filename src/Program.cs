using BlazorServerVolumeApp.Server.Hubs;
using BlazorServerVolumeApp.Services;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Hosting.WindowsServices;
using Radzen;

var isWindowsService = WindowsServiceHelpers.IsWindowsService();

if (isWindowsService)
{
    Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
}

var options = new WebApplicationOptions
{
    Args = args,
    ContentRootPath = isWindowsService ? AppContext.BaseDirectory : default
};

var builder = WebApplication.CreateBuilder(options);

if (isWindowsService)
{
    builder
        .WebHost
        .UseUrls();
}

builder.Host
    .UseWindowsService();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes =
    ResponseCompressionDefaults.MimeTypes.Concat(["application/octet-stream"]);
});

builder.Services.AddTransient<VolumeService>();
builder.Services.AddScoped<NotificationService>();

var app = builder.Build();

app.UseResponseCompression();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapHub<VolumeHub>("/volumehub");
app.MapFallbackToPage("/_Host");

await app.RunAsync();