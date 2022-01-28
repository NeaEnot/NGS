using System.Drawing;

namespace GUI.Models.Savers
{
    internal interface ISaver
    {
        public void AddFrame(Bitmap bmp);
        public void Save();
    }
}
