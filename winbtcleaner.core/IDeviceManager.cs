namespace winbtcleaner.core;

// handles bluetooth device discovery and management
public interface IDeviceManager
{
    IEnumerable<IDevice> Devices { get; }
    Task RefreshDevices();
}
