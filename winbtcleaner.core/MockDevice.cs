namespace winbtcleaner.core;

public class MockDevice : IDevice
{
    public MockDevice(string name)
    {
        Name = name;
    }

    public string Name { get; }
    public bool IsConnected { get; set; } = false;

    public async Task<bool> Unpair()
    {
        await Task.Delay(100);
        return true;
    }
}
