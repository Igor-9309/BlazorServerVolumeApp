using BlazorServerVolumeApp.Hubs;
using BlazorServerVolumeApp.Services;
using Microsoft.AspNetCore.SignalR;
using static BlazorServerVolumeApp.Contants;

namespace BlazorServerVolumeApp.Server.Hubs;

public class VolumeHub : Hub<IVolumeHubClient>
{
    private readonly VolumeService _volumeService;

    public VolumeHub(VolumeService volumeService)
    {
        _volumeService = volumeService;
    }

    public override async Task OnConnectedAsync()
    {
        var value = _volumeService.Current() * 100;
        await Clients.Client(Context.ConnectionId).LoadVolumeSync((int)value);
    }

    [HubMethodName(VolumeHubMethods.ReLoadVolumeSync)]
    public Task<int> ReLoadVolumeSync()
    {
        var value = _volumeService.Current() * 100;
        return Task.FromResult((int)value);
    }

    [HubMethodName(VolumeHubMethods.SendVolumeSync)]
    public async Task SendMessageSync(int volume)
    {
        var value = _volumeService.Change(volume) * 100;
        await Clients.All.ReceiveVolumeSync((int)value);
    }
}
