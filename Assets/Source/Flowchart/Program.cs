using Lomztein.ProjectAI.Flowchart.Nodes;
using Lomztein.ProjectAI.Flowchart.Nodes.Hooks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Lomztein.ProjectAI.UI.Editor.ProgramEditor;
using Lomztein.ProjectAI.Serialization;
using Newtonsoft.Json.Linq;
using Lomztein.ProjectAI.Flowchart.Nodes.Connections;

namespace Lomztein.ProjectAI.Flowchart {

    public class Program : MonoBehaviour, INamed, IJsonSerializable {

        public string Name { get; set; } = "Test";
        public string Description { get; set; } = "Merely a program here for early testing.";

        public List<Node> AllNodes { get; private set; } = new List<Node>();
        public List<IConnection> AllConnections { get; private set; } = new List<IConnection>();

        public List<EventNode> EventNodes { get; private set; } = new List<EventNode>();

        public EventNode AddEvent (string eventName, string eventDescription, params OutputHook[] outputs) {

            EventNode eventNode = new EventNode()
                .SetPosition(new VectorPosition(0, 0))
                .SetProgram(this)
                .SetName(eventName)
                .SetDesc(eventDescription) as EventNode;
            eventNode.SetOutputs(outputs);

            eventNode.Init();
            eventNode.InitChildren();

            foreach (OutputHook hook in outputs) {
                hook.SetProgram (this);
                hook.SetNode (eventNode); 
            }

            return AddEvent (eventNode);
        }

        public EventNode AddEvent (EventNode eventNode) {
            EventNodes.Add (eventNode);
            return eventNode;
        }

        public void ExecuteEvent(string eventName, params object[] arguments) {
            EventNode eventNode = EventNodes.Find (x => x.Name == eventName);

            for (int i = 0; i < eventNode.OutputHooks.Length; i++)
                eventNode.OutputHooks[i].Value = arguments[i];

            Executor.CurrentExecutor.RootExecute (eventNode);
        }

        public void AddNode (Node node) {
            AllNodes.Add (node);
        }

        public bool RemoveNode (Node node) {
            return AllNodes.Remove (node);
        }

        public void AddConnection(IConnection connection) => AllConnections.Add(connection);

        public bool RemoveConnection(IConnection connection) => AllConnections.Remove(connection);

        public void Save ()
        {
            this.Save(Application.dataPath + "/StreamingAssets/Programs/" + Name + ".json");
        }

        public JObject Serialize()
        {
            return new JObject
            {
                { "Name", Name },
                { "Desc", Description },
                { "Nodes",  new JArray(AllNodes.Select(x => x.Serialize())) },
                { "Connections",  new JArray(AllConnections.Select(x => x.Serialize())) }
            };
        }

        public void Deserialize(JObject source)
        {
            throw new System.NotImplementedException();
        }
    }

}