using Lomztein.ProjectAI.Flowchart;
using Lomztein.ProjectAI.Flowchart.Nodes;
using Lomztein.ProjectAI.Flowchart.Nodes.Flow;
using Lomztein.ProjectAI.Flowchart.Nodes.Hooks;
using Lomztein.ProjectAI.UI.Editor.ProgramEditor.Workspace.Attachments;
using Lomztein.ProjectAI.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.ProjectAI.UI.Editor.ProgramEditor.Workspace.Widgets {

    public class NodeWidget : Widget {

        public static Resource<GameObject> LeftHookElement = new Resource<GameObject> ("UI/Flowchart/LeftHookWidget");
        public static Resource<GameObject> RightHookElement = new Resource<GameObject> ("UI/Flowchart/RightHookWidget");
        public static Resource<GameObject> FlowOutputElement = new Resource<GameObject> ("UI/Flowchart/FlowOutputHook");

        public Vector2 Position { get { return transform.position; } set { transform.position = value; } }

        public Node Node { get; set; }
        protected override IDeletable Deletable { get { return Node; } }
        public override IFlowchartElement InnerElement { get { return Node; } }

        // Unity references.
        public Text nameHeader;
        public Button dragButton;

        public HookAttachment chainInput;
        public HookAttachment chainOutput;
        public RectTransform chainIOParent;

        public RectTransform inputParent;
        public RectTransform outputParent;
        public RectTransform flowOutputParent;

        public void Initialize (Node node) {

            Node = node;
            nameHeader.text = Node.Name;
            Node.OnDeleted += () => { Destroy (gameObject); };

            bool anyChainIO = false;
            if (Node is IPrevNode) {
                IPrevNode pNode = Node as IPrevNode;
                chainOutput.Initialize (pNode.NextHook, Color.white);
                anyChainIO = true;
            } else
                Destroy (chainOutput.gameObject);

            if (Node is INextNode) {
                INextNode nNode = Node as INextNode;
                chainInput.Initialize (nNode.PreviousHook, Color.white);
                anyChainIO = true;
            } else
                Destroy (chainInput.gameObject);

            if (!anyChainIO)
                Destroy (chainIOParent);

            if (Node is IHasInput) {
                IHasInput inNode = Node as IHasInput;
                if (inNode.InputHooks != null)
                    CreateIOHooks (inputParent, LeftHookElement.Get (), inNode.InputHooks);
                else
                    Destroy (inputParent.gameObject);
            } else {
                Destroy (inputParent.gameObject);
            }


            if (Node is IHasOutput) {
                IHasOutput outNode = Node as IHasOutput;
                if (outNode.OutputHooks != null)
                    CreateIOHooks (outputParent, RightHookElement.Get (), outNode.OutputHooks);
                else
                    Destroy (outputParent.gameObject);
            } else {
                Destroy (outputParent.gameObject);
            }

            if (Node is IFlowNode) {
                IFlowNode flowNode = Node as IFlowNode;

                foreach (var output in flowNode.PossibleRoutes) {
                    GameObject newHook = Instantiate (FlowOutputElement.Get (), flowOutputParent);
                    newHook.GetComponent<HookAttachment> ().Initialize (output, Color.white);
                }
            } else {
                Destroy (flowOutputParent.gameObject);
            }

        }

        private void CreateIOHooks (RectTransform parent, GameObject prefab, IVariableHook[] hooks) {
            foreach (var io in hooks) {
                GameObject newHook = Instantiate (prefab, parent);
                newHook.GetComponent<HookAttachment> ().Initialize (io as IHook, TypeColors.GetColor (io.ValueType));
            }
        }

        public void OnMove() {
            
        }
    }

}
