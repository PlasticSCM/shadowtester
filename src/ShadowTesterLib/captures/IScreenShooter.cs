using System.Drawing;

namespace ShadowTesterLib.Captures
{
    public interface IScreenShooter
    {
        void Capture(string filename, Point origin, Size size);
    }
}