using SharpAvi;
using SharpAvi.Output;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Render.Renders
{
    internal class AviRender : IRender
    {
        private AviWriter writer;
        private IAviVideoStream stream;

        public void Configure(RenderConfig config)
        {
            writer = new AviWriter($"{config.Path}\\universe.avi");
            writer.FramesPerSecond = 100 / config.Delay;
        }

        public void AddFrame(Bitmap bmp)
        {
            if (stream == null)
            {
                stream = writer.AddVideoStream(width: bmp.Width, height: bmp.Height);
                stream.Codec = KnownFourCCs.Codecs.Uncompressed;
                stream.BitsPerPixel = BitsPerPixel.Bpp32;
            }

            byte[] frameData = BitmapToByteArray(bmp);

            stream.WriteFrame(true, frameData, 0, frameData.Length);
        }

        public void Save()
        {
            writer.Close();
        }

        private byte[] BitmapToByteArray(Bitmap bitmap)
        {
            BitmapData bmpdata = null;
            Image img = Image.FromHbitmap(bitmap.GetHbitmap());
            img.RotateFlip(RotateFlipType.RotateNoneFlipY);
            bitmap = new Bitmap(img);

            try
            {
                bmpdata = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, bitmap.PixelFormat);
                int numbytes = bmpdata.Stride * bitmap.Height;
                byte[] bytedata = new byte[numbytes];
                IntPtr ptr = bmpdata.Scan0;

                Marshal.Copy(ptr, bytedata, 0, numbytes);

                return bytedata;
            }
            finally
            {
                if (bmpdata != null)
                    bitmap.UnlockBits(bmpdata);
            }
        }
    }
}
