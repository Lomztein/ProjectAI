using Lomztein.ProjectAI.Flowchart.Nodes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.ProjectAI.Flowchart.Nodes.Components
{
    public class ChainLinkComponent : NodeComponent
    {
        private ChainInterface chainIn;
        private ChainInterface chainOut;

        public ChainLinkComponent (ChainInterface chainIn, ChainInterface chainOut)
        {
            this.chainIn = chainIn;
            this.chainOut = chainOut;
        }

        public override void Init(Node parentNode)
        {
            chainIn.Hook.OnExecute += (data) => chainOut.Hook.Execute(data);
        }
    }
}
