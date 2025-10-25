using Windows.Devices.Bluetooth;
using Windows.Devices.Enumeration;

namespace winbtcleaner.core;

public class DeviceManager : IDeviceManager
{
    private readonly List<Device> _devices = new();
    private readonly string _deviceSelector;

    public DeviceManager()
    {
        string pairingStateSelector = BluetoothLEDevice.GetDeviceSelectorFromPairingState(true);
        string deviceNameSelector = BluetoothLEDevice.GetDeviceSelectorFromDeviceName("Microsoft Bluetooth Keyboard");

        _deviceSelector = $"({pairingStateSelector}) AND ({deviceNameSelector})";
    }

    public IEnumerable<IDevice> Devices => _devices;

    public async Task RefreshDevices()
    {
        _devices.Clear();
        var collection = await DeviceInformation.FindAllAsync(_deviceSelector);
        foreach (var deviceInfo in collection)
        {
            BluetoothLEDevice bleDevice = await BluetoothLEDevice.FromIdAsync(deviceInfo.Id);
            if (bleDevice != null)
            {
                _devices.Add(new Device(bleDevice));
            }
        }
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
