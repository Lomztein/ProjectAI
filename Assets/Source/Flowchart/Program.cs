using Lomztein.ProjectAI.Flowchart.Nodes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Lomztein.ProjectAI.UI.Editor.ProgramEditor;
using Lomztein.ProjectAI.Serialization;
using Newtonsoft.Json.Linq;
using Lomztein.ProjectAI.Flowchart.Nodes.Connections;
using Lomztein.ProjectAI.Flowchart.Nodes.Interfaces.Hooks;
using Lomztein.ProjectAI.Flowchart.Nodes.Interfaces;
using Lomztein.ProjectAI.Flowchart.Nodes.Components;
using Lomztein.ProjectAI.Flowchart.Nodes.Prefabs;
using System;

namespace Lomztein.ProjectAI.Flowchart {

    public class Program : MonoBehaviour, INamed, IJsonSerializable {

        public string Name { get; set; } = "Test";
        public string Description { get; set; } = "Merely a program here for early testing.";

        public List<Node> AllNodes { get; private set; } = new List<Node>();
        public List<IConnection> AllConnections { get; private set; } = new List<IConnection>();

        public List<Node> EventNodes { get; private set; } = new List<Node>();

        public event Action<Node> OnNodeInstantiated;
        public event Action<IConnection> OnConnectionInstantiated;

        public Node AddEvent (string eventName, string eventDescription, params OutputHook[] outputs) {

            Node eventNode = new Node()
                .SetPosition(new VectorPosition(0, 0))
                .SetProgram(this)
                .SetName(eventName)
                .SetDesc(eventDescription) as Node;

            ChainInterface eventChain = new ChainInterface(Direction.Out);
            OutputInterface eventOutput = new OutputInterface();
            eventOutput.SetHooks(outputs);

            EventComponent eventComponent = new EventComponent(eventChain, eventOutput);

            eventNode.AddComponent(eventChain);
            eventNode.AddComponent(eventOutput);

            eventNode.AddComponent(eventComponent);

            foreach (OutputHook hook in outputs)
            {
                hook.SetProgram(this);
                hook.SetNode(eventNode);
            }

            eventNode.Init();

            return AddEvent (eventNode);
        }

        public Node AddEvent (Node eventNode) {
            EventNodes.Add (eventNode);
            return eventNode;
        }

        public void ExecuteEvent(string eventName, params object[] arguments) {
            Node eventNode = EventNodes.Find (x => x.Name == eventName);

            OutputInterface output = eventNode.GetComponent<OutputInterface>();
            for (int i = 0; i < output.IOHooks.Count; i++)
                output.IOHooks[i].Value = arguments[i];

            Executor.CurrentExecutor.RootExecute (eventNode.GetComponent<EventComponent>());
        }

        public void AddNode (Node node) {
            AllNodes.Add (node);
        }

        public bool RemoveNode (Node node) {
            return AllNodes.Remove (node);
        }

        // TODO: Route node and connection instantiating through these two methods, as to have a single, centralized method for creating, initializing and storing.
        public Node InstantiateNode (INodePrefab nodePrefab)
        {
            Node node = nodePrefab.Create(this);
            AddNode(node);
            node.Init();

            OnNodeInstantiated?.Invoke (node);
            return node;
        }

        public IConnection InstantiateConnection (IHook from, IHook to)
        {
            IConnection connection = from.CreateConnection();
            connection.Connect(from, to);
            AddConnection(connection);
            connection.Init();

            OnConnectionInstantiated?.Invoke(connection);
            return connection;
        }

        public void AddConnection(IConnection connection) => AllConnections.Add(connection);

        public bool RemoveConnection(IConnection connection) => AllConnections.Remove(connection);

        public void Save ()
        {
            this.Save(Application.dataPath + "/StreamingAssets/Programs/" + Name + ".json");
        }

        public JToken Serialize()
        {
            return new JObject
            {
                { "Name", Name },
                { "Desc", Description },
                { "Nodes",  new JArray(AllNodes.Select(x => x.Serialize())) },
                { "Connections",  new JArray(AllConnections.Select(x => x.Serialize())) }
            };
        }

        public void Deserialize(JToken source)
        {
            throw new System.NotImplementedException();
        }
    }

}