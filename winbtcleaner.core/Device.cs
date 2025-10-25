using Windows.Devices.Bluetooth;

namespace winbtcleaner.core;

public class Device : IDevice
{
    private readonly BluetoothLEDevice _bleDevice;

    public Device(BluetoothLEDevice bleDevice)
    {
        _bleDevice = bleDevice;
    }

    public string Name => _bleDevice.Name;

    public bool IsConnected => _bleDevice.ConnectionStatus == BluetoothConnectionStatus.Connected;

    public async Task<bool> Unpair()
    {
        var result = await _bleDevice.DeviceInformation.Pairing.UnpairAsync();
        return result.Status == Windows.Devices.Enumeration.DeviceUnpairingResultStatus.Unpaired;
    }
}
