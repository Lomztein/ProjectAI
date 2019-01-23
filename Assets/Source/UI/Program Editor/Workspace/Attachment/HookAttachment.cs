using Lomztein.ProjectAI.Flowchart;
using Lomztein.ProjectAI.Flowchart.Nodes.Interfaces.Hooks;
using Lomztein.ProjectAI.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.ProjectAI.UI.Editor.ProgramEditor.Workspace.Attachments {

    public class HookAttachment : Attachment {

        public static Resource<GameObject> LeftHookWidget = new Resource<GameObject>("UI/Flowchart/LeftHookWidget");
        public static Resource<GameObject> RightHookWidget = new Resource<GameObject>("UI/Flowchart/RightHookWidget");

        public override IFlowchartElement InnerElement { get { return Hook; } }
        public IHook Hook { get; set; }

        // Resources

        // Unity references
        public Text text;
        public Button button;
        public Image image;

        public void Initialize (IHook hook, Color color) {

            Hook = hook;

            if (text)
                text.text = hook.Name;

            image.color = color;
            button.onClick.AddListener (() => OnClicked ());
        }

        private void OnClicked () {
            ProgramEditor.CurrentEditor.workspace.OnClickedHook (this);
        }

    }

}