namespace winbtcleaner.core;

public class MockDeviceManager : IDeviceManager
{
    private readonly List<MockDevice> _devices = new();

    public IEnumerable<IDevice> Devices => _devices;

    public async Task RefreshDevices()
    {
        _devices.Clear();
        var random = new Random();
        int deviceCount = random.Next(5, 16);
        for (int i = 0; i < deviceCount; i++)
        {
            int delay = random.Next(50, 201);
            await Task.Delay(delay);
            _devices.Add(new MockDevice($"Mock Device {i + 1}"));
        }
        _devices[random.Next(deviceCount)].IsConnected = true;
    }

    // TODO: figure out how to not duplicate this code everywhere
    public async Task RemoveStaleDevices()
    {
        for (int i = _devices.Count - 1; i >= 0; i--)
        {
            var device = _devices[i];
            if (!device.IsConnected)
            {
                bool unpaired = await device.Unpair();
                if (unpaired)
                {
                    _devices.RemoveAt(i);
                }
            }
        }
    }
}
