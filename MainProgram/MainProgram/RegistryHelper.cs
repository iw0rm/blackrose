using System;
using Microsoft.Win32;

public class RegistryHelper
{
    public static void AddToRegistry(string key, string value)
    {
        using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true))
        {
            registryKey.SetValue(key, value);
        }
    }

    public static void RemoveFromRegistry(string key)
    {
        using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true))
        {
            registryKey.DeleteValue(key);
        }
    }
}