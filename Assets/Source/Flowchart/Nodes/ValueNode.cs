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

        public ValueNode SetType (Type type)
        {
            ValueType = type;
            return this;
        }

        public ValueNode SetValue (object value)
        {
            Value = value;
            return this;
        }

        public override void Delete() {
            Output.DisconnectAll ();
            base.Delete ();
        }

        public override void InitChildren()
        {
            //this, "Value", "Contains a singular, constant value.", _valueType
            OutputHooks = new OutputHook[1];
            Output = new OutputHook()
                .SetNode (this)
                .SetName ("Value")
                .SetDesc ("Contains a singular, constant value.") as OutputHook;

            Output.Init();
        }
    }
}
