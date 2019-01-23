using Lomztein.ProjectAI.Flowchart.Nodes.Interfaces.Hooks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lomztein.ProjectAI.Flowchart.Nodes.Connections {

    public class ChainConnection : Connection {

        public override Type HookType { get { return typeof (ChainHook); } }

    }
}
