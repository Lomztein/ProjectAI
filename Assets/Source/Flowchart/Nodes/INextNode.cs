using Lomztein.ProjectAI.Flowchart.Nodes.Hooks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lomztein.ProjectAI.Flowchart.Nodes {

    // A "Next Node" is a node that can be chained to a previous "PrevNode" node.
    public interface INextNode : IExecutable {

        ChainHook PreviousHook { get; set; }

    }

}
