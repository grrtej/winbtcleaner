namespace winbtcleaner.core;

public class MockDeviceManager : IDeviceManager
{
    private readonly List<MockDevice> _devices = new();

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
    }

    public IEnumerable<IDevice> Devices => _devices;
}
