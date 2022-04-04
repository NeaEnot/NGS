using Physics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Render.Renders
{
    internal class JpegsRender : IRender
    {
        private string path;
        private Date date;

        public void Configure(RenderConfig config)
        {
            path = config.Path;
            date = config.StartDate;
        }

        public void AddFrame(Bitmap bmp)
        {
            string framePath = GetCurrentFileName();
            using (FileStream s = new FileStream(framePath, FileMode.OpenOrCreate))
                bmp.Save(s, format: ImageFormat.Jpeg);

            date = date.NextDay();
        }

        public void Save()
        { }

        private string GetCurrentFileName()
        {
            DirectoryInfo pathDir = new DirectoryInfo(path);
            if (!pathDir.Exists)
                pathDir.Create();

            DirectoryInfo leodrDir = new DirectoryInfo($"{path}\\{date.Leodr}");
            if (!leodrDir.Exists)
                leodrDir.Create();

            DirectoryInfo milleniumDir = new DirectoryInfo($"{path}\\{date.Leodr}\\{date.Millenium}");
            if (!milleniumDir.Exists)
                milleniumDir.Create();

            DirectoryInfo yearDir = new DirectoryInfo($"{path}\\{date.Leodr}\\{date.Millenium}\\{date.Year}");
            if (!yearDir.Exists)
                yearDir.Create();

            return $"{path}\\{date.Leodr}\\{date.Millenium}\\{date.Year}\\{date.Day}.jpeg";
        }
    }
}
