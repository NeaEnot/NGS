using Physics;

namespace Drawing.Centers
{
    public interface ICenter
    {
        public Vector Center { get; }
        public bool IsScaled { get; }
    }
}
