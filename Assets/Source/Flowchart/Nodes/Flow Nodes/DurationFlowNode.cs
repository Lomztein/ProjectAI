using Lomztein.ProjectAI.Flowchart.Nodes.Hooks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lomztein.ProjectAI.Flowchart.Nodes.Flow {

    public class DurationFlowNode : FlowNode {

        public override string Name {
            get {
                return "Duration";
            }

            set {
                throw new NotImplementedException ();
            }
        }

        public override string Description {
            get {
                return "Executions something every tick for a given duration. Can function as an alarm as well.";
            }

            set {
                throw new NotImplementedException ();
            }
        }

        private List<Instance> Instances { get; set; }

        public DurationFlowNode (Program _parentProgram, INodePosition position) : base (_parentProgram, position) {

            this.SetInputs (new InputHook (ParentProgram, this, "Duration", "The duration in ticks that this will run.", typeof (int)));
            this.SetOutputs (new OutputHook (ParentProgram, this, "Counter", "The counter of the last execution instance.", typeof (int)));
            SetRoutes (
                new ChainHook (ParentProgram, this, Direction.Out, "Execute During", "The output that will be executed while this node is running."),
                new ChainHook (ParentProgram, this, Direction.Out, "Done", "Executed when a duration has completed.")
                );
            Instances = new List<Instance> ();
        }

        public override void Execute(ExecutionMetadata metadata) {

            Instance newInstance = new Instance (this, metadata, this.Get<int> ("Duration"), (instance) => OnInstanceComplete (instance));
            Instances.Add (newInstance);

            Executor.CurrentExecutor.OnTick += newInstance.Incriment;

            base.Execute (metadata);
        }

        private void OnInstanceComplete (Instance instance) {
            this.GetHook ("Done").EnqueueAndExecuteNextNextNodes ();
            Executor.CurrentExecutor.OnTick -= instance.Incriment;
            Instances.Remove (instance);

        }
        
        private class Instance {

            private int Counter { get; set; }
            private int Goal { get; set; }

            private Action<Instance> OnCompletion { get; set; }

            private DurationFlowNode Parent { get; set; }
            private ExecutionMetadata Metadata { get; set; }

            private void Execute () {
                Parent.Set ("Counter", Counter);
                Parent.GetHook ("Execute During").EnqueueAndExecuteNextNextNodes ();
            }

            public Instance (DurationFlowNode _parent, ExecutionMetadata _metadata, int _goal, Action<Instance> _onCompletion) {
                Parent = _parent;
                Metadata = _metadata;
                Goal = _goal;
                OnCompletion = _onCompletion;
            }

            public void Incriment (float deltaTime) {
                Counter++;
                Execute ();

                if (Counter >= Goal)
                    OnCompletion (this);
            }

        }

    }
}
