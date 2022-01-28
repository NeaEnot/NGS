using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace GUI.Models.Savers
{
    internal class JpegsSaver : ISaver
    {
        private string path;

        private int day = 0;
        private int year = 0;
        private int millenium = 0;
        private int leodr = 0;

        internal JpegsSaver(string path)
        {
            this.path = path;
        }

        public void AddFrame(Bitmap bmp)
        {
            string framePath = GetCurrentFileName();
            using (FileStream s = new FileStream(framePath, FileMode.OpenOrCreate))
                bmp.Save(s, format: ImageFormat.Jpeg);

            NextDay();
        }

        public void Save()
        {
            
        }

        private void NextDay()
        {
            day++;

            if (day == 365)
            {
                day = 0;
                year++;
            }

            if (year == 1000)
            {
                year = 0;
                millenium++;
            }

            if (millenium == 1000)
            {
                millenium = 0;
                leodr++;
            }
        }

        private string GetCurrentFileName()
        {
            DirectoryInfo pathDir = new DirectoryInfo(path);
            if (!pathDir.Exists)
                pathDir.Create();

            DirectoryInfo leodrDir = new DirectoryInfo($"{path}\\{leodr}");
            if (!leodrDir.Exists)
                leodrDir.Create();

            DirectoryInfo milleniumDir = new DirectoryInfo($"{path}\\{leodr}\\{millenium}");
            if (!milleniumDir.Exists)
                milleniumDir.Create();

            DirectoryInfo yearDir = new DirectoryInfo($"{path}\\{leodr}\\{millenium}\\{year}");
            if (!yearDir.Exists)
                yearDir.Create();

            return $"{path}\\{leodr}\\{millenium}\\{year}\\{day}.jpeg";
        }
    }
}
