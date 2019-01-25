﻿using Lomztein.ProjectAI.Flowchart.Nodes.Components;
using Lomztein.ProjectAI.Flowchart.Nodes.Interfaces;
using Lomztein.ProjectAI.Flowchart.Nodes.Interfaces.Hooks;
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

            ChainInterface chainIn = new ChainInterface(Direction.In);
            ChainInterface chainOut = new ChainInterface(Direction.Out);

            InputInterface input = new InputInterface();
            OutputInterface output = new OutputInterface();

            ActionComponent action = new ActionComponent(chainIn, input, output, Action);
            ChainLinkComponent chainLink = new ChainLinkComponent(chainIn, chainOut);

            InputHook[] inputs = new InputHook[Action.Inputs.Count];
            OutputHook[] outputs = new OutputHook[Action.Outputs.Count];

            Node actionNode = new Node()
                .SetSource(Identifier, 0)
                .SetPosition(new VectorPosition(0, 0))
                .SetProgram(parentProgram)
                .SetName(Name)
                .SetDesc(Description) as Node;

            actionNode.AddComponent(chainIn);
            actionNode.AddComponent(chainOut);

            actionNode.AddComponent(input);
            actionNode.AddComponent(output);

            actionNode.AddComponent(action);
            actionNode.AddComponent(chainLink);

            for (int i = 0; i < Action.Inputs.Count; i++) {
                inputs[i] = new InputHook ().SetType (Action.Inputs[i].Type).SetNode (actionNode).SetName (Action.Inputs[i].Name).SetProgram (parentProgram) as InputHook;
            }

            for (int i = 0; i < Action.Outputs.Count; i++) {
                outputs[i] = new OutputHook ().SetType (Action.Outputs[i].Type).SetNode (actionNode).SetName (Action.Outputs[i].Name).SetProgram (parentProgram) as OutputHook;
            }

            input.SetHooks(inputs);
            output.SetHooks(outputs);

            actionNode.Init();

            return actionNode;
        }
    }
}
