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
                throw new InvalidOperationException ();
            }
        }

        public override string Description {
            get {
                return "Executions something every tick for a given duration. Can function as an alarm as well.";
            }

            set {
                throw new InvalidOperationException();
            }
        }

        private List<Instance> Instances { get; set; } = new List<Instance>();

        public override void InitChildren () {
            base.InitChildren();

            this.SetInputs (new InputHook ().SetType (typeof (int)).SetName ("Duration").SetDesc ("The duration in ticks that this will run.") as InputHook);
            this.SetOutputs(new OutputHook().SetType(typeof(int)).SetName ("Counter").SetDesc ("The counter of execution steps.") as OutputHook);

            SetRoutes (
                new ChainHook ().SetNode (this).SetDirection (Direction.Out).SetName ("During").SetDesc ("Execute while running.").SetProgram (ParentProgram) as ChainHook,
                new ChainHook ().SetNode (this).SetDirection (Direction.Out).SetName ("Done").SetDesc ("Execute when done.").SetProgram (ParentProgram) as ChainHook
                );

            PossibleRoutes.InitAll();
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
