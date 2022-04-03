using System.Drawing;

namespace Render
{
    /// <include file='Documentation.xml' path='documentation/members[@name="IRender"]/IRender/*'/>
    public interface IRender
    {
        /// <include file='Documentation.xml' path='documentation/members[@name="IRender"]/Configure/*'/>
        public void Configure(RenderConfig config);
        /// <include file='Documentation.xml' path='documentation/members[@name="IRender"]/AddFrame/*'/>
        public void AddFrame(Bitmap bmp);
        /// <include file='Documentation.xml' path='documentation/members[@name="IRender"]/Save/*'/>
        public void Save();
    }
}
