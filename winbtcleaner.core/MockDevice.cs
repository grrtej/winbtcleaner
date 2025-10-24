namespace winbtcleaner.core;

public class MockDevice : IDevice
{
    public MockDevice(string name)
    {
        Name = name;
    }

    public string Name { get; }
}
