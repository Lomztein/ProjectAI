using Lomztein.ProjectAI.Flowchart.Nodes.Hooks;
using Lomztein.ProjectAI.Serialization;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.ProjectAI.Flowchart.Nodes {

    public abstract class Node : FlowchartElement, INamed, IDeletable, IJsonSerializable {

        public string PrefabIdentifier { get; private set; }
        public int PrefabSourceIndex { get; private set; }

        private List<IHook> allHooks = new List<IHook>();

        public INodePosition Position { get; private set; }

        public uint LastActiveTick { get; set; }

        public event OnDeletedEvent OnDeleted;

        public Node SetPosition(INodePosition position)
        {
            Position = position;
            return this;
        }

        public void Init ()
        {
            ParentProgram.AddNode(this);
            OnDeleted += () => ParentProgram.RemoveNode(this);
        }

        /// <summary>
        /// Once all properties has been set, call InitChildren to set up child objects.
        /// </summary>
        /// <returns></returns>
        public abstract void InitChildren();

        public Node SetSource (string identifier, int index)
        {
            PrefabIdentifier = identifier;
            PrefabSourceIndex = index;
            return this;
        }

        public virtual void Delete() {
            OnDeleted?.Invoke();
        }

        public JObject Serialize()
        {
            return new JObject
            {
                { "PrefabIdentifier", PrefabIdentifier },
                { "PrefabSourceIndex", PrefabSourceIndex },
                { "Position", Position.Serialize () }
            };
        }

        public void Deserialize(JObject source)
        {
            // Everyhing but position is automatically populated when the node is created from prefab.
            Position.Deserialize(source.GetValue("Position") as JObject);
        }

        public int GetHookIndex(IHook hook) => allHooks.IndexOf(hook);
        public int GetNodeIndex() => ParentProgram.AllNodes.IndexOf(this);

        public void AddHooks (params IHook[] hooks)
        {
            allHooks.AddRange(hooks);
        }

    }
}