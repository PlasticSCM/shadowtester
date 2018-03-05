using System.Drawing;

namespace ShadowTesterLib.Captures
{
    public interface IImagesCapturer
    {
        void ScreenShot(string filename, Point origin, Size size);
        void FromString(string filename, string text);
    }
}