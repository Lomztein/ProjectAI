using Lomztein.ProjectAI.Flowchart;
using Lomztein.ProjectAI.Flowchart.Nodes;
using Lomztein.ProjectAI.Flowchart.Nodes.Hooks;
using Lomztein.ProjectAI.Flowchart.Nodes.Prefabs;
using Lomztein.ProjectAI.UI.Editor.ProgramEditor;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.ProjectAI.Unit {

    public class Unit : MonoBehaviour, IProgrammable, IDamageable, IKillable, INamed {

        public string Name { get; set; }
        public string Description { get; set; }

        public float Health { get; set; }

        public Program Program { get; set; }

        // Testing related stuff, remove later.
        public ProgramEditor programEditor;

        void Awake() {
            Name = "Testing Unit";
            Description = "Unit used exclusively for testing early flowchart functionality.";
            Health = 100;

            Program = GetComponent<Program> ();
        }

        private void Start() {

            Program.AddEvent ("Begin", "Runs when the the unit is first constructed.");
            Program.AddEvent ("Tick", "Runs " + Executor.TickRate + " times per second.", new OutputHook (null, null, "Delta Time", "The time between last and current tick.", typeof (float)));
            Program.AddEvent ("Destroyed", "Runs when the the unit is destroyed for whatever reason.", new OutputHook (null, null, "Destroyer", "The unit that destroyed this unit.", typeof (Unit)));

            Executor.CurrentExecutor.AddProgram (Program);

            programEditor.Initialize ();

        }

        public void Damage (float damage) {
            Health -= damage;
        }

        public void Kill () {
            Destroy (gameObject);
        }

        private void OnDestroy() {
            Executor.CurrentExecutor.RemoveProgram (Program);
        }

        public INodePrefab[] GetAvailableNodePrefabs () {

            PrefabGathering gathering = new PrefabGathering ();
            transform.root.BroadcastMessage ("GatherNodePrefabs", gathering);

            return gathering.PrefabList.ToArray ();
        }

        public void GatherNodePrefabs (PrefabGathering prefabGathering) {
            prefabGathering.AddActions (new List<INodePrefab> () {
                new ActionNodePrefab ("Suicide", "Kill self out of pure shame of being alive.", new ProgramAction ((input, output) => Kill ())
                .AddInput (typeof (bool), "Certain?", "Are you certain that you can go through with this?")
                .AddInput (typeof (int), "Selfshots", "The amount of shots you will shoot yourself with.")
                .AddOutput (typeof (bool), "Success", "Were you succesful in taking your own life?")),
            new ActionNodePrefab ("Log", "Log the given input.", new ProgramAction ((input, output) => Debug.Log (input.Get<string> ("Text"))).AddInput (typeof (string), "Text", "The text to print.")),
            new ActionNodePrefab ("Add", "Add together the two numbers.", new ProgramAction ((input, output) => output.Set ("Result", input.Get<float> ("Num1") + input.Get<float>("Num2"))).AddInput (typeof (float), "Num1", "The first number.").AddInput (typeof (float), "Num2", "The second number.").AddOutput (typeof (float), "Result", "The resulting number.")),
            });
        }

        public class PrefabGathering {

            public List<INodePrefab> PrefabList { get; set; }

            public void AddActions (IEnumerable<INodePrefab> actions) {
                PrefabList.AddRange (actions);
            }

            public PrefabGathering () {
                PrefabList = new List<INodePrefab> ();
            }

        }
    }

}
