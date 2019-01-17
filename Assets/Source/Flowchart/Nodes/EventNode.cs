using Lomztein.ProjectAI.Flowchart.Nodes.Hooks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lomztein.ProjectAI.Flowchart.Nodes {

    public class EventNode : Node, IPrevNode, IExecutable, IHasOutput {
        private readonly Program program;
        private readonly OutputHook[] outputs;

        public ChainHook NextHook { get; set; }

        public OutputHook[] OutputHooks { get; set; }

        public EventNode SetOutputs (params OutputHook[] outputs)
        {
            NodeExtensions.SetOutputs(this, outputs);
            return this;
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

        public override void InitChildren()
        {
            NextHook = new ChainHook()
                .SetNode(this)
                .SetDirection(Direction.Out)
                .SetProgram(ParentProgram) as ChainHook;

            NextHook.Init();
            OutputHooks.InitAll();
        }
    }
}
