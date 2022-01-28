using ImageMagick;
using System.Drawing;
using System.Linq;

namespace GUI.Models.Savers
{
    internal class GifSaver : ISaver
    {
        private string path;

        private MagickImageCollection collection;

        internal GifSaver(string path)
        {
            this.path = path;
            collection = new MagickImageCollection();
        }

        public void AddFrame(Bitmap bmp)
        {
            string framePath = $"{path}\\frame.jpeg";

            bmp.Save(framePath);

            collection.Add(framePath);
            collection.Last().AnimationDelay = 1;
        }

        public void Save()
        {
            QuantizeSettings settings = new QuantizeSettings();
            settings.Colors = 256;
            collection.Quantize(settings);

            collection.Optimize();

            collection.Write($"{path}\\universe.gif");
        }
    }
}
