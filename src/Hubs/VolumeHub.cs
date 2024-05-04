using BlazorServerVolumeApp.Hubs;
using BlazorServerVolumeApp.Services;
using Microsoft.AspNetCore.SignalR;
using static BlazorServerVolumeApp.Contants;

namespace BlazorServerVolumeApp.Server.Hubs;

public class VolumeHub(VolumeService volumeService) : Hub<IVolumeHubClient>
{
    public override async Task OnConnectedAsync()
    {
        var value = volumeService.Current() * 100;
        await Clients.Client(Context.ConnectionId).LoadVolumeSync((int)value);
    }

    [HubMethodName(VolumeHubMethods.ReLoadVolumeSync)]
    public Task<int> ReLoadVolumeSync()
    {
        var value = volumeService.Current() * 100;
        return Task.FromResult((int)value);
    }

    [HubMethodName(VolumeHubMethods.SendVolumeSync)]
    public async Task SendMessageSync(int volume)
    {
        var value = volumeService.Change(volume) * 100;
        await Clients.All.ReceiveVolumeSync((int)value);
    }

    [HubMethodName(VolumeHubMethods.FrontEndVolumeSync)]
    public async Task FrontEndVolumeSync(string connectionId, int volume)
    {
        await Clients.AllExcept(connectionId).LoadVolumeSync(volume);
    }
}
