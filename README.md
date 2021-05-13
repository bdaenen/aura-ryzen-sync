# Aura Ryzen Sync
Syncs your Ryzen CPU temps with Asus Aura RGB

This can possibly work on other CPU's as well, but this was only tested on Ryzen Zen3.

## Features
 * Syncs all Asus Aura-supported RGB lights to a color based on the CPU temp.
 * Configurable "hot" and "cold" color and temperature values. The color applied will gradually interpolate between the two values based on the current temperature
 * Experimental support for Razer Chroma devices

## Dependencies
In order to run this you need the Asus Aura Sync service installed. This usually ships together with ARMOURY CRATE.

If you want to enable Razer Chroma support, make sure you have Synapse 3 installed.

## Usage
 1. Download and unzip the latest release
 2. Open "settings.json" and change the configuration to your liking.
 3. Start `AuraCpuSync.exe`
 
 ## Configuration
 The following fields are available in settings.json:
 
 ```json5
 {
    // The color to use when the CPU temp is <= coldTemp in RGB format (0-255)
    "coldColorRed": 0,
    "coldColorGreen": 255,
    "coldColorBlue": 0,
    // The temperature that's considered "cold"
    "coldTemp": 40,
    // The color to use when the CPU temp is >= hotTemp in RGB format (0-255)
    "hotColorRed": 255,
    "hotColorGreen": 0,
    "hotColorBlue": 0,
    // Temperature that's considered "hot"
    "hotTemp": 95,
    // Name of the sensor to use, as reported by LibreHardwareMonitor (https://github.com/LibreHardwareMonitor/LibreHardwareMonitor)
    "sensorName": "Core (Tctl/Tdie)",
    // Interval between updating the temperature & RGB
    "pollingInterval": 100,
    // Enable/disables Razer Chroma support (EXPERIMENTAL!)
    "enableRazerChroma": false
    // Reserved field for future use
    "version": 1,
}
```
 
 ## Installing as a service
 If you don't want to keep the console window open at all times, you can try installing the program as a service.
 
 To do so, simply run `AuraCpuSyncServiceInstaller.exe`
 
 If you wish to uninstall the service later, simply run `AuraCpuSyncServiceUninstaller.exe`
 
 __Note:__ Razer Chroma support does not seem to work when running as a service. As a workaround, you can add `AuraCpuSync.exe` to your startup items, although this will still keep a console window open.
 
 
 ## Building
 This is fairly straightforward to build, although you will have to compile [LibreHardwareMonitorLib](https://github.com/LibreHardwareMonitor/LibreHardwareMonitor/tree/master/LibreHardwareMonitorLib) by yourself, and probably update the reference to it.
 
 The binaries from all three projects are written to `AuraCpuSync\bin\Release` instead of having their own release folder.
