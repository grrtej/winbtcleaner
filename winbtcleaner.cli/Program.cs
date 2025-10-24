using winbtcleaner.core;

IDeviceManager deviceManager = new MockDeviceManager();

await deviceManager.RefreshDevices();

Console.WriteLine("Paired Microsoft Bluetooth Keyboards:");
foreach (var device in deviceManager.Devices)
{
    Console.WriteLine($"- {device.Name}");
}
