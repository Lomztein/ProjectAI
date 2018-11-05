using Lomztein.ProjectAI.Flowchart.Nodes.Hooks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lomztein.ProjectAI.Flowchart.Nodes {

    public class ValueNode : Node, IHasOutput {

        public object Value { get { return Output.Value; } set { Output.Value = value; } }
        public Type ValueType { get { return Output.ValueType; } set { Output.ValueType = value; } }

        public OutputHook Output { get { return OutputHooks.Single (); } set { OutputHooks[0] = value; } }
        public OutputHook[] OutputHooks { get; set; }

        public ValueNode (Program _parentProgram, object _value, Type _valueType, INodePosition position) : this (_parentProgram, _valueType, position) {
            Value = _value;
        }

        public ValueNode(Program _parentProgram, Type _valueType, INodePosition position) : base(_parentProgram, position) {
            OutputHooks = new OutputHook[1];
            Output = new OutputHook (_parentProgram, this, "Value", "Contains a singular, constant value.", _valueType);

            ValueType = _valueType; // Not strictly neccesary since it is already set in OutputHooks constructor, but I like it there anyways.
        }

        public override void Delete() {
            Output.DisconnectAll ();
            base.Delete ();
        }

    }
}
