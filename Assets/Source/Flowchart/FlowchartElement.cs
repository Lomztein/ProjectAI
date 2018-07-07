using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lomztein.ProjectAI.Flowchart {

    /// <summary>
    /// Base class that all flowchart elements should inherit from.
    /// </summary>
    public abstract class FlowchartElement : IFlowchartElement {

        public Program ParentProgram { get; set; }

        public FlowchartElement (Program _parent) {
            ParentProgram = _parent;
        }

    }

}
