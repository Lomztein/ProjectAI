using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lomztein.ProjectAI.Flowchart {

    /// <summary>
    /// Base interface that all flowchart elements should inherit from.
    /// </summary>
    public interface IFlowchartElement : INamed {

        Program ParentProgram { get; set; }

    }

}
