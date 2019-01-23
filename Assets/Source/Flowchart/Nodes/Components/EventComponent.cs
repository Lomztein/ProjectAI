using Lomztein.ProjectAI.Flowchart.Nodes.Interfaces;
using Lomztein.ProjectAI.Flowchart.Nodes.Interfaces.Hooks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lomztein.ProjectAI.Flowchart.Nodes.Components {

    public class EventComponent : NodeComponent, IExecutable {

        private ChainInterface chainOut;
        private readonly OutputInterface output;

        public EventComponent (ChainInterface chainOut, OutputInterface valOut)
        {
            this.chainOut = chainOut;
            output = valOut;
        }

        public void Execute(ExecutionMetadata metadata) {
            chainOut.Hook.EnqueueAndExecuteNextNextNodes ();
        }

        public override void Init(Node parentNode)
        {
            chainOut.Hook.OnExecute += Execute;
        }
    }
}
