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

        public virtual string Name { get; set; }
        public virtual string Description { get; set; }

        public FlowchartElement SetProgram (Program parent)
        {
            ParentProgram = parent;
            return this;
        }

        public FlowchartElement SetName (string name)
        {
            Name = name;
            return this;
        }

        public FlowchartElement SetDesc (string desc)
        {
            Description = desc;
            return this;
        }

    }

}
