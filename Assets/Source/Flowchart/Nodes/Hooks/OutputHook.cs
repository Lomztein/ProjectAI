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
            return new VariableConnection (ParentProgram);
        }

        public OutputHook (Program _parentProgram, Node _parent, string _name, string _description, Type _type) : base (_parentProgram, _parent, Direction.Out, 0) {
            Name = _name;
            Description = _description;
            ValueType = _type;
            Direction = Direction.Out;
        }
    }
}
