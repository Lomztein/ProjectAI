using Lomztein.ProjectAI.Flowchart.Nodes.Hooks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lomztein.ProjectAI.Flowchart.Nodes.Connections {

    public class ChainConnection : Connection {

        public ChainConnection(Program _parent) : base (_parent) { }

        public override Type HookType { get { return typeof (ChainHook); } }

    }
}
