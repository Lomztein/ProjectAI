using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lomztein.ProjectAI.Flowchart.Nodes.Hooks;

namespace Lomztein.ProjectAI.Flowchart.Nodes.Flow {
    public class IfFlowNode : FlowNode {

        public override string Name {
            get {
                return "If";
            }

            set {
                throw new NotImplementedException ();
            }
        }

        public override string Description {
            get {
                return "Splits the flowchart into two different paths, based on the condition given.";
            }

            set {
                throw new NotImplementedException ();
            }
        }

        public IfFlowNode(Program _parent) : base (_parent) {

            this.SetInputs (new InputHook (ParentProgram, this, "Condition", "The condition that decides which path execution will continue on.", typeof (bool)));
            SetRoutes (
                new ChainHook (ParentProgram, this, Direction.Out, "True", "Continues here if the condition is true."),
                new ChainHook (ParentProgram, this, Direction.Out, "False", "Continues here if the condition is false."));
        }

        public override void Execute(ExecutionMetadata metadata) {

            if (this.Get<bool> ("Condition")) {
                this.GetHook ("True").EnqueueAndExecuteNextNextNodes ();
            }else {
                this.GetHook ("False").EnqueueAndExecuteNextNextNodes ();
            }

            base.Execute (metadata);
        }

    }
}
