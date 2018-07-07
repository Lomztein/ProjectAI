using Lomztein.ProjectAI.Flowchart.Nodes.Hooks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Lomztein.ProjectAI.Flowchart.Nodes.Connections {

    public class Connection : FlowchartElement, IConnection {

        public virtual Type HookType { get { return typeof (IHook); } }

        public IHook From { get; set; }
        public IHook To { get; set; }

        public event OnDeletedEvent OnDeleted;

        public virtual bool CanConnect(IHook one, IHook two) {
            return (HookType.IsInstanceOfType (one) && HookType.IsInstanceOfType (two));
        }

        public Connection (Program _parent) : base (_parent) { }

        public virtual void Delete() {

            From.Disconnect (this, false);
            To.Disconnect (this, false);

            if (OnDeleted != null)
                OnDeleted ();
        }
    }
}
