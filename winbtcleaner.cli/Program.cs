using winbtcleaner.core;

if (args.Length != 1)
{
    return Usage();
}

string command = args[0].ToLowerInvariant();
switch (command)
{
    case "list":
        return await ListDevices();
    case "cleanup":
        return await CleanupDevices();
    default:
        return Usage();
}

int Usage()
{
    Console.WriteLine("usage: winbtcleaner.cli <command>\n");
    Console.WriteLine("remove stale bluetooth keyboards\n");
    Console.WriteLine("available commands:\n");
    Console.WriteLine("    list\tlist paired bluetooth keyboards");
    Console.WriteLine("    cleanup\tremove stale bluetooth keyboards");

    return 1;
}

void printDeviceManager(IDeviceManager deviceManager)
{
    Console.WriteLine("Paired Bluetooth Keyboards:\n");
    foreach (var device in deviceManager.Devices)
    {
        string connectionStatus = device.IsConnected ? "CONNECTED" : "DISCONNECTED";
        Console.WriteLine($"    {device.Name}\t[{connectionStatus}]");
    }
}

async Task<int> ListDevices()
{
    IDeviceManager deviceManager = new MockDeviceManager();

    await deviceManager.RefreshDevices();

    printDeviceManager(deviceManager);

    return 0;
}

async Task<int> CleanupDevices()
{
    IDeviceManager deviceManager = new MockDeviceManager();

    await deviceManager.RefreshDevices();

    printDeviceManager(deviceManager);

    Console.WriteLine("\nCleaning up...\n");
    await deviceManager.RemoveStaleDevices();

    printDeviceManager(deviceManager);

    return 0;
}