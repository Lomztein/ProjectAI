using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lomztein.ProjectAI.Flowchart.Nodes.Hooks {

    public interface IVariableHook {

        object Value { get; set; }
        Type ValueType { get; set; }

    }
}
