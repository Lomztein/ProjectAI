using Lomztein.ProjectAI.Flowchart.Nodes.Flow;
using Lomztein.ProjectAI.UI.Editor.ProgramEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lomztein.ProjectAI.Flowchart.Nodes.Prefabs {

    public class FlowNodePrefab : INodePrefab {

        public string Name { get; set; }
        public string Description { get; set; }
        public string Identifier { get; set; }

        private Type FlowNodeType { get; set; }

        public FlowNodePrefab (Type flowNodeType, string name, string desc) {
            FlowNodeType = flowNodeType;
            Name = name;
            Description = desc;
            Identifier = FlowNodeType.Name;
        }

        public Node Create(Program parentProgram) {
            Node node = new Node()
                .SetSource(Identifier, 0)
                .SetProgram(parentProgram) as Node;

            return node;
        }
    }
}
