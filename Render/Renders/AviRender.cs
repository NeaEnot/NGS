using SharpAvi;
using SharpAvi.Output;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Render.Renders
{
    internal class AviRender : IRender
    {
        private string path;
        private int delay;

        private AviWriter writer;
        private IAviVideoStream stream;

        private ImageConverter converter;

        public void Configure(RenderConfig config)
        {
            writer = new AviWriter(config.Path);
            writer.FramesPerSecond = 1000 / config.Delay;

            converter = new ImageConverter();
        }

        public void AddFrame(Bitmap bmp)
        {
            if (stream == null)
            {
                stream = writer.AddVideoStream(width: bmp.Width, height: bmp.Height);
                stream.Codec = KnownFourCCs.Codecs.Uncompressed;
            }

            byte[] frameData = (byte[])converter.ConvertTo(bmp, typeof(byte[]));

            stream.WriteFrame(true, frameData, 0, frameData.Length);
        }

        public void Save()
        {
            writer.Close();
        }
    }
}
