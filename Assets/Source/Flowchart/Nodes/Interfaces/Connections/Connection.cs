using Lomztein.ProjectAI.Flowchart.Nodes.Interfaces.Hooks;
using Newtonsoft.Json.Linq;
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

        public virtual void Delete() {
            From.Disconnect (this, false);
            To.Disconnect (this, false);
            OnDeleted?.Invoke();
        }

        public virtual void Init ()
        {
            ParentProgram.AddConnection(this);
            OnDeleted += () => ParentProgram.RemoveConnection(this);
        }

        public void Deserialize(JObject source)
        {
            throw new InvalidOperationException("Connections shouldn't be deserialized directly, but instead reconnected on program deserialization.");
        }

        public JObject Serialize()
        {
            return new JObject()
            {
                { "FromNode", From.ParentNode.GetNodeIndex () },
                { "FromHook", From.ParentNode.GetHookIndex (From) },
                { "ToNode", To.ParentNode.GetNodeIndex () },
                { "ToHook", To.ParentNode.GetHookIndex (To) }
            };
        }
    }
}
