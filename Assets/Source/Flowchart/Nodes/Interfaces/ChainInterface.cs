using System.Collections.Generic;
using Lomztein.ProjectAI.Flowchart.Nodes.Interfaces.Hooks;
 
namespace Lomztein.ProjectAI.Flowchart.Nodes.Interfaces
{
    class ChainInterface : NodeInterface
    {
        public ChainHook Hook;

        public override List<IHook> InterfaceHooks { get; set; } = new List<IHook>();

        public override Direction Direction { get; set; }

        public override void Delete()
        {
            Hook.DisconnectAll();
        }

        public override void Init(Node parent, Direction direction)
        {
            Direction = direction;
            Hook = new ChainHook()
                .SetDirection (direction)
                .SetMaxConnections (direction == Direction.In ? 1 : 0)
                .SetNode (parent)
                .SetName (direction == Direction.In ? "In" : "Out")
                .SetDesc (direction == Direction.In ? "Chain execution input." : "Chain execution output")
                .SetProgram (parent.ParentProgram) as ChainHook;
            Hook.Init();

            InterfaceHooks.Add(Hook);
        }
    }
}
