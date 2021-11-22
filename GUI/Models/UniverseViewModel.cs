using Physics;

namespace GUI.Models
{
    internal class UniverseViewModel
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public double G { get; private set; }
        public int CountBodies { get; private set; }

        internal UniverseViewModel(Universe universe)
        {
            Id = universe.Id;
            Name = universe.Name;
            G = universe.G;
            CountBodies = universe.Bodies.Count;
        }
    }
}
