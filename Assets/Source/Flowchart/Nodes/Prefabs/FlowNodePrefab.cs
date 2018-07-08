using Lomztein.ProjectAI.Flowchart.Nodes.Flow;
using Lomztein.ProjectAI.Flowchart.Nodes.Hooks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lomztein.ProjectAI.Flowchart.Nodes.Prefabs {

    public class FlowNodePrefab : INodePrefab {

        public string Name { get; set; }
        public string Description { get; set; }

        private Type FlowNodeType { get; set; }

        public FlowNodePrefab (Type flowNodeType) {
            FlowNodeType = flowNodeType;
            GetNameAndDesc ();
        }

        private void GetNameAndDesc () { // This is a very hacky solution, see if you can't figure out something more clever in the long run, perhaps with constants or something.
            Node node = Create (null);
            node.CopyTo (this);
        }

        public Node Create(Program parentProgram) {
            FlowNode node = Activator.CreateInstance (FlowNodeType, parentProgram as object) as FlowNode;
            return node;
        }
    }
}
