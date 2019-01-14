using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.ProjectAI.Sim
{
    internal class NonSimObjectInstantiated : Exception
    {
        public NonSimObjectInstantiated() : base("Simulation instantiated object without SimObjects. Please use GameObject.Instantiate for that instead of Simulation.Instantiate.") { }
    }
}
