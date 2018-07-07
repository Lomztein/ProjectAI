using Lomztein.ProjectAI.Flowchart;
using Lomztein.ProjectAI.Flowchart.Nodes.Prefabs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lomztein.ProjectAI.Unit {
    public interface IProgrammable {

        Program Program { get; set; }

        INodePrefab[] GetAvailableNodePrefabs();

    }
}
 