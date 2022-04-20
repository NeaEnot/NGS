using ImageMagick;
using System.Drawing;
using System.Linq;

namespace Render.Renders
{
    internal class GifRender : IRender
    {
        private string path;
        private int delay;
        private MagickImageCollection collection;

        public void Configure(RenderConfig config)
        {
            path = config.Path;
            delay = config.Delay;
            collection = new MagickImageCollection();
        }

        public void AddFrame(Bitmap bmp)
        {
            string framePath = $"{path}\\frame.jpeg";

            bmp.Save(framePath);

            collection.Add(framePath);
            collection.Last().AnimationDelay = delay;
        }

        public void Save()
        {
            QuantizeSettings settings = new QuantizeSettings();
            settings.Colors = 256;
            collection.Quantize(settings);

            collection.Optimize();

            collection.Write($@"{path}\universe.gif");
        }
    }
}
