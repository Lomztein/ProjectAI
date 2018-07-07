using Lomztein.ProjectAI.Flowchart.Nodes.Hooks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lomztein.ProjectAI.Flowchart.Nodes {

    /// <summary>
    /// A "PrevNode" is a node that can have a successing NextNode.
    /// </summary>
    public interface IPrevNode {

        ChainHook NextHook { get; set; }

    }
}
