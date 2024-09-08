using System;
using System.Threading;

public class MutexHelper
{
    public static bool IsMutexRunning(string mutexName)
    {
        try
        {
            Mutex mutex = Mutex.OpenExisting(mutexName);
            return true;
        }
        catch (WaitHandleCannotBeOpenedException)
        {
            return false;
        }
    }

    public static void CreateMutex(string mutexName)
    {
        Mutex mutex = new Mutex(true, mutexName);
    }
}