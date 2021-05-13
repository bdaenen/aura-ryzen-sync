using System.Threading.Tasks;
using AuraServiceLib;
using Colore;

namespace AuraCpuSync
{
    class LedUpdater
    {
        private readonly IAuraSyncDeviceCollection devices;
        private readonly IAuraSdk2 sdk;
        private IChroma chroma;

        public LedUpdater()
        {
            sdk = new AuraSdk() as IAuraSdk2;
            // Aquire control
            sdk.SwitchMode();
            // enumerate all devices
            devices = sdk.Enumerate(0);
        }

        public async Task InitChromaAsync()
        {
            chroma = await ColoreProvider.CreateNativeAsync();
        }

        public void UpdateColor(byte red, byte green, byte blue)
        {
            // Traverse all devices
            foreach (IAuraSyncDevice dev in devices)
            {
                // Traverse all LED's
                foreach (IAuraRgbLight light in dev.Lights)
                {
                    // Set all LED's to blue
                    light.Red = red;
                    light.Green = green;
                    light.Blue = blue;
                }
                // Apply colors that we have just set
                dev.Apply();
            }

            if (chroma != null)
            {
                chroma.SetAllAsync(Colore.Data.Color.FromRgb((uint)(red << 16 | green << 8 | blue)));
            }
        }

        public void ReleaseControl()
        {
            sdk.ReleaseControl(0);
        }
    }
}
