using Lomztein.ProjectAI.Flowchart;
using Lomztein.ProjectAI.Flowchart.Nodes;
using Lomztein.ProjectAI.Flowchart.Nodes.Flow;
using Lomztein.ProjectAI.Flowchart.Nodes.Hooks;
using Lomztein.ProjectAI.Flowchart.Nodes.Prefabs;
using Lomztein.ProjectAI.UI.Editor.ProgramEditor;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.ProjectAI.Unit {

    public class Unit : MonoBehaviour, IProgrammable, IDamageable, IKillable, INamed {

        public string Name { get => name; set => name = value; }
        public string Description { get; set; }

        public float Health { get; set; }

        public Program Program { get; set; }

        // Unity references.
        public GameObject deathParticle;

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
            Program.AddEvent ("Tick", "Runs " + Executor.TickRate + " times per second.", new OutputHook ().SetType (typeof(float)).SetName ("Delta Time").SetDesc ("The time between last and current tick.").SetProgram (Program) as OutputHook);
            Program.AddEvent ("Destroyed", "Runs when the the unit is destroyed for whatever reason.", new OutputHook().SetType(typeof(Unit)).SetName("Destroyer").SetDesc("The one responsible.").SetProgram(Program) as OutputHook);
            Program.AddEvent ("Test", "Runs whenever the test button is clicked.");

            Executor.CurrentExecutor.AddProgram (Program);

            programEditor.Initialize ();

        }

        public void MoveForwards () {
            transform.position += transform.forward * Time.fixedDeltaTime;
        }

        public void Rotate (int sign) {
            sign = (int)Mathf.Sign (sign);
            transform.Rotate (transform.up, 30 * Time.fixedDeltaTime * sign);
        }

        public void Damage (float damage) {
            Health -= damage;
        }

        public void Kill () {
            Destroy (gameObject);
            Instantiate (deathParticle, transform.position, Quaternion.identity);
        }

        private void OnDestroy() {
            Executor.CurrentExecutor.RemoveProgram (Program);
        }

        public INodePrefab[] GetAvailableNodePrefabs () {

            PrefabGathering gathering = new PrefabGathering ();
            transform.root.BroadcastMessage ("GatherNodePrefabs", gathering);

            return gathering.PrefabList.ToArray ();
        }

        public void OnTestButton () {
            Program.ExecuteEvent ("Test");
        }

        public void GatherNodePrefabs(PrefabGathering prefabGathering) {
            prefabGathering.AddActions(new List<INodePrefab>() {
            new ActionNodePrefab ("Sudoku", "Commit sudoku out of shame.", "Unit.Suicide", new ProgramAction ((input, output) => Kill ())),
            new ActionNodePrefab ("Log", "Log the given input.", "Log.Write", new ProgramAction ((input, output) => Debug.Log (input.Get<string> ("Text"))).AddInput (typeof (string), "Text", "The text to print.")),
            new ActionNodePrefab ("Add", "Add together the two numbers.", "Math.Add", new ProgramAction ((input, output) => output.Set ("Result", input.Get<float> ("Num1") + input.Get<float>("Num2"))).AddInput (typeof (float), "Num1", "The first number.").AddInput (typeof (float), "Num2", "The second number.").AddOutput (typeof (float), "Result", "The resulting number.")),

            // Movement test actions
            new ActionNodePrefab ("Move Forwards", "Move slightly forwards.", "Unit.MoveForwards", new ProgramAction ((input, output) => MoveForwards ())),
            new ActionNodePrefab ("Turn", "Turn a direction", "Unit.Turn", new ProgramAction ((input, output) => Rotate (input.Get<int> ("Sign"))).AddInput (typeof (int), "Sign", "Sign of the direction to turn.")),
            new ActionNodePrefab ("Fly", "Begone, thot!", "Unit.Fly", new ProgramAction ((input, output) => transform.Translate (Vector3.up * input.Get<float>("Speed") * Time.deltaTime)).AddInput (typeof (float), "Speed", "The speed of begoneness")),
            // End of movement test actions.

            new FlowNodePrefab (typeof (IfFlowNode), "If", "Control flow of execution with a bool value."),
            new FlowNodePrefab (typeof (DurationFlowNode), "Duration", "Execute something for a duration."),
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
