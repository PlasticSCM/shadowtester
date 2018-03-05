namespace ShadowTesterLib.System
{
    public interface ISystemMonitor
    {
        WindowData GetForegroundWindow();
        SystemInfo GetSystemInformation();
    }
}