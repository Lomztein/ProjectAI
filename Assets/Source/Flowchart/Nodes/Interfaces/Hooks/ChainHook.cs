using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lomztein.ProjectAI.Flowchart.Nodes.Connections;

namespace Lomztein.ProjectAI.Flowchart.Nodes.Interfaces.Hooks {

    public class ChainHook : Hook, IExecutable {

        public void Execute (ExecutionMetadata metadata)
        {
            OnExecute?.Invoke(metadata);
        }

        public event Action<ExecutionMetadata> OnExecute;

        public override IConnection CreateConnection() {
            ChainConnection newConn = new ChainConnection ().SetProgram (ParentProgram) as ChainConnection;
            newConn.Init();
            return newConn;
        }

        public override void Init () {

            switch (Direction) {
                case Direction.In:
                    Name = "Chain Input";
                    Description = "Hook a chain output up to this, in order to chain those two together.";
                    break;

                case Direction.Out:
                    Name = "Chain Output";
                    Description = "Hook this up to a chain input in order to further the chain.";
                    OnExecute += (data) => this.EnqueueAndExecuteNextNextNodes();
                    break;

                default:
                    Name = "Unsupported";
                    Description = "This hook direction is unsupported and shouldn't exist, go tell the dev that he fucked up.";
                    break;
            }

        }
    }
}
