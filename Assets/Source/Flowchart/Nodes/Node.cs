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

        private List<IHook> AllHooks { get => NodeComponents.OfType<INodeInterface>().SelectMany(x => x.InterfaceHooks).ToList (); }
        public List<INodeComponent> NodeComponents { get; private set; } = new List<INodeComponent>();

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
            NodeComponents.ForEach(x => x.Init(this));
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
                { "Components", new JArray (NodeComponents.Select (x => x.Serialize ())) },
            };
        }

        public void Deserialize(JObject source)
        {
            Position.Deserialize(source.GetValue("Position") as JObject);
            JArray components = source.GetValue("Components") as JArray;
            for (int i = 0; i < components.Count; i++)
            {
                NodeComponents[i].Deserialize(components[i] as JObject);
            }
        }

        public void AddComponent (INodeComponent component)
        {
            NodeComponents.Add(component);
        }

        public T GetComponent<T>() where T : INodeComponent => GetComponent<T>(x => true);

        public T GetComponent<T>(Predicate<T> filter)
        {
            return (T)NodeComponents.Find(x => x.GetType().IsEquivalentTo(typeof(T)) && filter((T)x));
        }

        public bool HasComponent (Type componentType)
        {
            return NodeComponents.Find(x => x.GetType().IsEquivalentTo(componentType)) != null;
        }

        public int GetHookIndex(IHook hook) => AllHooks.IndexOf(hook);
        public int GetNodeIndex() => ParentProgram.AllNodes.IndexOf(this);

    }
}