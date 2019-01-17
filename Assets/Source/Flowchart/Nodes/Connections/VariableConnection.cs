using Lomztein.ProjectAI.Flowchart.Nodes.Hooks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lomztein.ProjectAI.Flowchart.Nodes.Connections {

    public class VariableConnection : Connection {

        public override Type HookType { get { return typeof (IVariableHook); } }

        public override bool CanConnect(IHook one, IHook two) {

            IHook from;
            IHook to;

            Hook.GetOrderedIO (one, two, out from, out to);

            OutputHook output = from as OutputHook;
            InputHook input = to as InputHook;

            if (output == null || input == null)
                return false;

            return output.ValueType.IsConvertibleTo (input.ValueType);
        }


    }
}
