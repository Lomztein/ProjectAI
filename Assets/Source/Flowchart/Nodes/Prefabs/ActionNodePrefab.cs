using Lomztein.ProjectAI.Flowchart.Nodes.Hooks;
using Lomztein.ProjectAI.UI.Editor.ProgramEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lomztein.ProjectAI.Flowchart.Nodes.Prefabs {

    public class ActionNodePrefab : INodePrefab {

        public string Name { get; set; }
        public string Description { get; set; }
        public string Identifier { get; set; }

        private ProgramAction Action { get; set; }

        public ActionNodePrefab (string _name, string _description, string identifier, ProgramAction _action) {
            Name = _name;
            Description = _description;
            Identifier = identifier;
            Action = _action;
        }

        public Node Create(Program parentProgram) {

            ActionNode node = new ActionNode()
                .SetAction(Action)
                .SetSource(Identifier, 0)
                .SetPosition (new VectorPosition (0, 0))
                .SetProgram (parentProgram) as ActionNode;

            this.CopyTo (node);

            InputHook[] inputs = new InputHook[Action.Inputs.Count];
            OutputHook[] outputs = new OutputHook[Action.Outputs.Count];

            for (int i = 0; i < Action.Inputs.Count; i++) {
                inputs[i] = new InputHook ().SetType (Action.Inputs[i].Type).SetNode (node).SetName (Action.Inputs[i].Name).SetProgram (parentProgram) as InputHook;
            }

            for (int i = 0; i < Action.Outputs.Count; i++) {
                outputs[i] = new OutputHook ().SetType (Action.Outputs[i].Type).SetNode (node).SetName (Action.Outputs[i].Name).SetProgram (parentProgram) as OutputHook;
            }

            node.SetInputs (inputs);
            node.SetOutputs (outputs);

            node.InitChildren();

            return node;
        }
    }
}
