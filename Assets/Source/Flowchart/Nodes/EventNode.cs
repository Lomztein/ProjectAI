using Lomztein.ProjectAI.Flowchart.Nodes.Hooks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lomztein.ProjectAI.Flowchart.Nodes {

    public class EventNode : Node, IPrevNode, IExecutable, IHasOutput {

        public ChainHook NextHook { get; set; }

        public OutputHook[] OutputHooks { get; set; }

        public EventNode (Program _parent, params OutputHook[] _outputs) : base (_parent) {
            NextHook = new ChainHook (_parent, this, Direction.Out);
            OutputHooks = _outputs;
        }

        public void Execute(ExecutionMetadata metadata) {
            NextHook.EnqueueAndExecuteNextNextNodes ();
        }

        public override void Delete() {

            foreach (var hook in OutputHooks)
                hook.DisconnectAll ();

            NextHook.DisconnectAll ();

            base.Delete ();
        }
    }
}
