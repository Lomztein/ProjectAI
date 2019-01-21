using System.Collections.Generic;
using Lomztein.ProjectAI.Flowchart.Nodes.Interfaces.Hooks;
 
namespace Lomztein.ProjectAI.Flowchart.Nodes.Interfaces
{
    class ChainNodeInterface : INodeInterface
    {
        public ChainHook Hook;

        public List<IHook> InterfaceHooks { get; set; } = new List<IHook>();

        public void Init(Node parent, Direction direction)
        {
            Hook = new ChainHook()
                .SetDirection (direction)
                .SetMaxConnections (direction == Direction.In ? 1 : 0)
                .SetNode (parent)
                .SetName (direction == Direction.In ? "In" : "Out")
                .SetDesc (direction == Direction.In ? "Chain execution input." : "Chain execution output")
                .SetProgram (parent.ParentProgram) as ChainHook;
            Hook.Init();
        }
    }
}
