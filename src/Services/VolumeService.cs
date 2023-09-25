using CoreAudio;

namespace BlazorServerVolumeApp.Services
{
    /// <summary>
    /// Note the AudioSession manager did not have a method to enumerate all sessions in windows Vista
    /// this will only work on Win7 and newer.
    /// </summary>
    public class VolumeService
    {
        public float Change(int volume)
        {
            try
            {
                var devEnum = new MMDeviceEnumerator(Guid.NewGuid());
                using var device = devEnum.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);

                device.AudioEndpointVolume.MasterVolumeLevelScalar = MathF.Round(volume / 100.0f, 2);

                return device.AudioEndpointVolume.MasterVolumeLevelScalar;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                return 0.0f;
            }
        }

        public float Current()
        {
            try
            {
                var devEnum = new MMDeviceEnumerator(Guid.NewGuid());
                using var device = devEnum.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);

                return device.AudioEndpointVolume.MasterVolumeLevelScalar;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                return 0.0f;
            }
        }
    }
}
