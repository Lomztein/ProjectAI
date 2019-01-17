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

        public ActionNode SetAction (ProgramAction action)
        {
            Action = action;
            return this;
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

        public override void InitChildren()
        {
            NextHook = new ChainHook().SetNode (this)
                .SetDirection (Direction.Out)
                .SetProgram (ParentProgram) as ChainHook;

            PreviousHook = new ChainHook().SetNode(this)
                .SetDirection(Direction.In)
                .SetProgram(ParentProgram) as ChainHook;

            NextHook.Init();
            PreviousHook.Init();

            InputHooks.InitAll();
            OutputHooks.InitAll();
        }
    }
}
