using System;
using System.Linq;
using System.Management;
using System.Security.Principal;

using ShadowTesterLib.Util;

namespace ShadowTesterLib.System
{
    public static class ManagementHandler
    {
        public static string GetUser()
        {
            return WindowsIdentity.GetCurrent().Name;
        }

        public static string GetProcessor()
        {
            ManagementObjectCollection.ManagementObjectEnumerator processors =
                GetManagementObjectCollection(WIN32_PROCESSOR).GetEnumerator();

            if (!processors.MoveNext())
                return UNKNOWN;

            return TryGetProcessor(processors.Current);
        }

        public static string GetHardDisk()
        {
            ManagementObjectCollection.ManagementObjectEnumerator diskDrives =
                GetManagementObjectCollection(WIN32_DISKDRIVE).GetEnumerator();

            if (!diskDrives.MoveNext())
                return UNKNOWN;

            return TryGetHardDisk(diskDrives.Current);
        }

        public static string GetOs()
        {
            foreach (ManagementObject managementObject in GetManagementObjectCollection(WIN32_OPERATINGSYSTEM))
            {
                string[] osParts = managementObject
                    .Properties["Name"]
                    .Value
                    .ToString()
                    .Split(
                        ("|").ToCharArray(),
                        StringSplitOptions.RemoveEmptyEntries);

                return osParts[0];
            }

            return String.Empty;
        }

        public static string GetRam()
        {
            long capacity = 
                GetManagementObjectCollection(WIN32_PHYSICALMEMORY)
                    .Cast<ManagementObject>()
                    .Sum(managementObject =>
                        long.Parse(
                            managementObject.Properties["Capacity"].Value.ToString()));

            return DataStorageUnitHelper.BytesToGbs(capacity) + " GB";
        }

        static string TryGetProcessor(ManagementBaseObject managementObject)
        {
            try
            {
                return managementObject.Properties["Name"].Value.ToString();
            }
            catch
            {
                return UNKNOWN;
            }
        }

        static string TryGetHardDisk(ManagementBaseObject managementObject)
        {
            try
            {
                return DataStorageUnitHelper.BytesToGbs(
                    long.Parse(
                        managementObject.Properties["Size"].Value.ToString())) + " GB";
            }
            catch
            {
                return UNKNOWN;
            }
        }

        static ManagementObjectCollection GetManagementObjectCollection(
            string queryObject)
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(
                string.Format("SELECT * FROM {0}", queryObject));
            return searcher.Get();
        }

        const string UNKNOWN = "Unknown";

        const string WIN32_PROCESSOR = "Win32_Processor";
        const string WIN32_DISKDRIVE = "Win32_DiskDrive";
        const string WIN32_OPERATINGSYSTEM = "Win32_OperatingSystem";
        const string WIN32_PHYSICALMEMORY = "Win32_PhysicalMemory";
    }
}