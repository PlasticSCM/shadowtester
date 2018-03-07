namespace ShadowTesterLib.System
{
    public class SystemMonitor : ISystemMonitor
    {
        public WindowData GetForegroundWindow()
        {
            return new WindowData()
            {
                Position = WindowHandler.GetForegroundWindowPosition(),
                Process = WindowHandler.GetForegroundProcess(),
                Size = WindowHandler.GetForegroundWindowSize()
            };
        }

        public SystemInfo GetSystemInformation()
        {
            return new SystemInfo()
            {
                Processor = ManagementHandler.GetProcessor(),
                HardDisk = ManagementHandler.GetHardDisk(),
                Os = ManagementHandler.GetOs(),
                Ram = ManagementHandler.GetRam(),
                User = ManagementHandler.GetUser()
            };
        }
    }
}