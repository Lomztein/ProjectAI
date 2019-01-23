/*using System;
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

        public override void InitChildren()
        {
            base.InitChildren();

            this.SetInputs(new InputHook().SetType(typeof(bool)).SetNode(this).SetName("Condition").SetDesc("Execute \"True\" or \"False\" dependant on this.").SetProgram(ParentProgram) as InputHook);
            SetRoutes(
                new ChainHook().SetNode(this).SetDirection(Direction.Out).SetName("True").SetDesc("This way if true").SetProgram(ParentProgram) as ChainHook,
                new ChainHook().SetNode(this).SetDirection(Direction.Out).SetName("False").SetDesc("This way if false").SetProgram(ParentProgram) as ChainHook
                );

            PossibleRoutes.InitAll();
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
*/