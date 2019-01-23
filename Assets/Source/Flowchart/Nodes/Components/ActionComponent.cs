using Lomztein.ProjectAI.Flowchart.Nodes.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lomztein.ProjectAI.Flowchart.Nodes.Components {

    public class ActionComponent : NodeComponent {

        private ChainInterface chainIn;

        private readonly InputInterface input;
        private readonly OutputInterface output;

        public ProgramAction Action { get; set; }

        public ActionComponent (ChainInterface chainIn, InputInterface input, OutputInterface output, ProgramAction action)
        {
            this.chainIn = chainIn;
            this.input = input;
            this.output = output;
            Action = action;
        }

        public void Execute(ExecutionMetadata metadata) {
            Action.Execute (input, output);
        }

        public void Delete() {
            chainIn.Hook.OnExecute -= (data) => Execute(data);
        }

        public override void Init(Node parentNode)
        {
            chainIn.Hook.OnExecute += (data) => Execute(data);
        }
    }
}
