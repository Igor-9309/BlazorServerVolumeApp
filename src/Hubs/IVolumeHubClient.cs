namespace BlazorServerVolumeApp.Hubs
{
    public interface IVolumeHubClient
    {
        /// <summary>
        ///  <see cref="Contants.VolumeHubMethods.LoadVolumeSync"/>
        /// </summary>
        /// <param name="volume"></param>
        /// <returns></returns>
        Task LoadVolumeSync(int volume);

        /// <summary>
        ///  <see cref="Contants.VolumeHubMethods.ReceiveVolumeSync"/>
        /// </summary>
        /// <param name="volume"></param>
        /// <returns></returns>
        Task ReceiveVolumeSync(int volume);
    }
}
