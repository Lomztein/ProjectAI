using Lomztein.ProjectAI.Flowchart.Nodes.Interfaces;
using Lomztein.ProjectAI.Flowchart.Nodes.Interfaces.Hooks;
using Lomztein.ProjectAI.Serialization;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.ProjectAI.Flowchart.Nodes {

    public class Node : FlowchartElement, INamed, IDeletable, IJsonSerializable {

        public string PrefabIdentifier { get; private set; }
        public int PrefabSourceIndex { get; private set; }

        private List<IHook> AllHooks { get => nodeInterfaces.SelectMany(x => x.InterfaceHooks).ToList (); }
        private List<INodeComponent> nodeComponents = new List<INodeComponent>();
        private List<INodeInterface> nodeInterfaces = new List<INodeInterface>();

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
                { "Position", Position.Serialize () },
                { "Components", new JArray (nodeComponents.Select (x => x.Serialize ())) },
                { "Interfaces", new JArray (nodeInterfaces.Select (x => x.Serialize ())) }
            };
        }

        public void Deserialize(JObject source)
        {
            // Everyhing but position is automatically populated when the node is created from prefab.
            Position.Deserialize(source.GetValue("Position") as JObject);
        }

        public T GetOrAddInterface<T> (Direction direction) where T : INodeInterface, new ()
        {
            T nodeInterface = (T)nodeInterfaces.Find(x => x.GetType().IsEquivalentTo(typeof(T)) && x.Direction == direction);
            if (nodeInterface == null)
            {
                nodeInterface = new T();
                nodeInterfaces.Add(nodeInterface);
                nodeInterface.Init(this, direction);
            }

            return nodeInterface;
        }

        public void AddComponent ()

        public int GetHookIndex(IHook hook) => AllHooks.IndexOf(hook);
        public int GetNodeIndex() => ParentProgram.AllNodes.IndexOf(this);

    }
}