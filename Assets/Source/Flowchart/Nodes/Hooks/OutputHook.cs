using Lomztein.ProjectAI.Flowchart.Nodes.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lomztein.ProjectAI.Flowchart.Nodes.Hooks {

    public class OutputHook : Hook, IVariableHook {

        public object Value { get; set; }
        public Type ValueType { get; set; }

        public override IConnection CreateConnection() {
            return new VariableConnection ().SetProgram (ParentProgram) as VariableConnection;
        }

        public OutputHook SetType (Type type)
        {
            ValueType = type;
            return this;
        }

        public override void Init()
        {
            Direction = Direction.Out;
        }
    }
}
