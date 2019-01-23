using System.Collections.Generic;
using Lomztein.ProjectAI.Flowchart.Nodes.Interfaces.Hooks;
 
namespace Lomztein.ProjectAI.Flowchart.Nodes.Interfaces
{
    public class ChainInterface : NodeInterface
    {
        public ChainHook Hook;

        public override List<IHook> InterfaceHooks { get; set; } = new List<IHook>();

        public override Direction Direction { get; set; }

        public ChainInterface (Direction dir) { Direction = dir; }

        public override void Delete()
        {
            Hook.DisconnectAll();
        }

        public override void Init(Node parent)
        {
            Hook = new ChainHook()
                .SetDirection (Direction)
                .SetNode (parent)
                .SetProgram (parent.ParentProgram) as ChainHook;
            Hook.Init();

            InterfaceHooks.Add(Hook);
        }
    }
}
