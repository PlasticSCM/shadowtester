using System.Collections.Generic;

namespace ShadowTesterLib.Captures
{
    public interface ISystemCapturer
    {
        void CaptureForegroundProcess(string filename, IList<string> expectedProceses);
        void CaptureSystemInformation(string filename);
    }
}