using Lomztein.ProjectAI.Flowchart.Nodes.Hooks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lomztein.ProjectAI.Flowchart.Nodes {
    class VariableNode : Node, INextNode {

        public ChainHook PreviousHook { get; set; }

        public object Value { get; set; }

        public void Execute(ExecutionMetadata metadata) {
        }

        public override void InitChildren()
        {
            PreviousHook = new ChainHook()
                .SetNode(this)
                .SetDirection(Direction.In)
                .SetProgram(ParentProgram) as ChainHook;

            PreviousHook.Init();
        }
    }
}
