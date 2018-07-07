﻿using Lomztein.ProjectAI.Flowchart.Nodes.Hooks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lomztein.ProjectAI.Flowchart.Nodes {
    class VariableNode : Node, INextNode {

        public VariableNode(Program _parentProgram) : base (_parentProgram) { }

        public ChainHook PreviousHook { get; set; }

        public object Value { get; set; }

        public void Execute(ExecutionMetadata metadata) {
        }

    }
}
