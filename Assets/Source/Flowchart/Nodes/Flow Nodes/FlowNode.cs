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

        public FlowNode (Program _parent) : base (_parent) {
            PreviousHook = new ChainHook (_parent, this, Direction.In);
            NextHook = new ChainHook (_parent, this, Direction.Out);
        }

        protected FlowNode SetRoutes (params ChainHook[] routes) {
            PossibleRoutes = routes;
            return this;
        }

        public virtual void Execute(ExecutionMetadata metadata) {
            NextHook.EnqueueAndExecuteNextNextNodes ();
        }

    }
}
