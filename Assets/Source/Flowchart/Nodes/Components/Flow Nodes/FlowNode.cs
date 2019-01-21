using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lomztein.ProjectAI.Flowchart.Nodes.Hooks;

namespace Lomztein.ProjectAI.Flowchart.Nodes.Flow {

    public abstract class FlowNode : Node, IHasInput, IHasOutput, INextNode, IFlowNode {

        public InputHook[] InputHooks { get; set; }
        public OutputHook[] OutputHooks { get; set; }

        public ChainHook PreviousHook { get; set; }
        public ChainHook NextHook { get; set; }
        public ChainHook[] PossibleRoutes { get; set; }

        public override void InitChildren ()
        {
            PreviousHook = new ChainHook()
                .SetNode (this)
                .SetDirection (Direction.In)
                .SetProgram (ParentProgram) as ChainHook;

            NextHook = new ChainHook()
                .SetNode (this)
                .SetDirection (Direction.Out)
                .SetProgram (ParentProgram) as ChainHook;

            PreviousHook.Init();
            NextHook.Init();

            AddHooks(PreviousHook);
            AddHooks(NextHook);
        }

        protected FlowNode SetRoutes (params ChainHook[] routes) {
            PossibleRoutes = routes;
            AddHooks(routes);
            return this;
        }

        public virtual void Execute(ExecutionMetadata metadata) {
            NextHook.EnqueueAndExecuteNextNextNodes ();
        }

    }
}
