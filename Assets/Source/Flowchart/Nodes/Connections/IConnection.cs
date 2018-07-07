﻿using Lomztein.ProjectAI.Flowchart.Nodes.Hooks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lomztein.ProjectAI.Flowchart.Nodes.Connections {

    public interface IConnection : IFlowchartElement, IDeletable {

        IHook From { get; set; }
        IHook To { get; set; }

        Type HookType { get; }

        bool CanConnect(IHook one, IHook two);

    }
}
