using Lomztein.ProjectAI.Flowchart.Nodes.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lomztein.ProjectAI.Flowchart.Nodes.Components {

    public class ActionComponent : NodeComponent {

        private ChainInterface chainIn;

        private InputInterface input;
        private OutputInterface output;

        public ProgramAction Action { get; set; }

        public ActionComponent SetAction (ProgramAction action)
        {
            Action = action;
            return this;
        }

        public void Execute(ExecutionMetadata metadata) {
            Action.Execute (input, output);
        }

        public void Delete() {
            chainIn.Hook.OnExecute -= (data) => Execute(data);
        }

        public override void Setup(Node parentNode)
        {
            chainIn = parentNode.GetOrAddInterface<ChainInterface>(Direction.In);

            input = parentNode.GetOrAddInterface<InputInterface>(Direction.In);
            output = parentNode.GetOrAddInterface<OutputInterface>(Direction.Out);
        }

        public override void Init(Node parentNode)
        {
            chainIn.Hook.OnExecute += (data) => Execute(data);
        }
    }
}
