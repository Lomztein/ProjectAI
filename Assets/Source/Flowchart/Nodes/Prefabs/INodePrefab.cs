using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lomztein.ProjectAI.Flowchart.Nodes.Prefabs {

    public interface INodePrefab : INamed {

        string Identifier { get; set; }

        Node Create(Program parentProgram);

    }
}
