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

        private ProgramAction Action { get; set; }

        public ActionNodePrefab (string _name, string _description, ProgramAction _action) {
            Name = _name;
            Description = _description;
            Action = _action;
        }

        public Node Create(Program parentProgram) {

            ActionNode node = new ActionNode (parentProgram, Action, new VectorPosition (0, 0));
            this.CopyTo (node);

            InputHook[] inputs = new InputHook[Action.Inputs.Count];
            OutputHook[] outputs = new OutputHook[Action.Outputs.Count];

            for (int i = 0; i < Action.Inputs.Count; i++) {
                inputs[i] = new InputHook (parentProgram, node, Action.Inputs[i].Name, "", Action.Inputs[i].Type);
            }

            for (int i = 0; i < Action.Outputs.Count; i++) {
                outputs[i] = new OutputHook (parentProgram, node, Action.Outputs[i].Name, "", Action.Outputs[i].Type);
            }

            node.InputHooks = inputs;
            node.OutputHooks = outputs;

            return node;
        }
    }
}
