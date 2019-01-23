using Lomztein.ProjectAI.Flowchart.Nodes.Interfaces;
using Lomztein.ProjectAI.Flowchart.Nodes.Interfaces.Hooks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lomztein.ProjectAI.Flowchart.Nodes.Components {

    public class EventComponent : NodeComponent, IExecutable {

        private ChainInterface chainOut;
        private OutputInterface output;

        public void Execute(ExecutionMetadata metadata) {
            chainOut.Hook.EnqueueAndExecuteNextNextNodes ();
        }

        public override void Setup(Node parentNode)
        {
            chainOut = parentNode.GetOrAddInterface<ChainInterface>(Direction.Out);
            output = parentNode.GetOrAddInterface<OutputInterface>(Direction.Out);
        }

        public override void Init(Node parentNode)
        {
        }
    }
}
