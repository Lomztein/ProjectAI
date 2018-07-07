using Lomztein.ProjectAI.Flowchart.Nodes.Hooks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lomztein.ProjectAI.Flowchart.Nodes {

    public class ActionNode : Node, IPrevNode, INextNode, IExecutable, IHasInput, IHasOutput {

        public ChainHook NextHook { get; set; }
        public ChainHook PreviousHook { get; set; }

        public InputHook[] InputHooks { get; set; }
        public OutputHook[] OutputHooks { get; set; }

        public ProgramAction Action { get; set; }

        public ActionNode (Program _parent, ProgramAction _action) : base (_parent) {
            NextHook = new ChainHook (_parent, this, Direction.Out);
            PreviousHook = new ChainHook (_parent, this, Direction.In);
            Action = _action;
        }

        public void Execute(ExecutionMetadata metadata) {
            Action.Execute (this, this);
            NextHook.EnqueueAndExecuteNextNextNodes ();
        }

        public override void Delete() {

            foreach (var hook in InputHooks)
                hook.DisconnectAll ();
            
            foreach (var hook in OutputHooks)
                hook.DisconnectAll ();

            NextHook.DisconnectAll ();
            PreviousHook.DisconnectAll ();

            base.Delete ();
        }
    }
}
