using Lomztein.ProjectAI.Flowchart.Nodes.Hooks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lomztein.ProjectAI.Flowchart.Nodes.Flow {

    public interface IFlowNode : IPrevNode {

        ChainHook[] PossibleRoutes { get; set; }

    }

}
