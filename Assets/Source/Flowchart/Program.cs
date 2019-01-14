using Lomztein.ProjectAI.Flowchart.Nodes;
using Lomztein.ProjectAI.Flowchart.Nodes.Hooks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Lomztein.ProjectAI.UI.Editor.ProgramEditor;

namespace Lomztein.ProjectAI.Flowchart {

    public class Program : MonoBehaviour {

        public List<Node> AllNodes { get; private set; }

        public List<EventNode> EventNodes { get; private set; }

        public void Awake() {
            AllNodes = new List<Node> ();
            EventNodes = new List<EventNode> ();
        }

        public EventNode AddEvent (string eventName, string eventDescription, params OutputHook[] outputs) {

            EventNode eventNode = new EventNode (this, new VectorPosition (0, 0), outputs) { Name = eventName, Description = eventDescription };

            foreach (OutputHook hook in outputs) {
                hook.ParentProgram = this;
                hook.ParentNode = eventNode; 
            }

            return AddEvent (eventNode);
        }

        public EventNode AddEvent (EventNode eventNode) {
            EventNodes.Add (eventNode);
            AddNode (eventNode);
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

        public void RemoveNode (Node node) {
            AllNodes.Remove (node);
        }

    }

}