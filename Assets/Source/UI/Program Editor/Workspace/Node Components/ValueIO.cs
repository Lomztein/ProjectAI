using System;
using System.Collections.Generic;
using Lomztein.ProjectAI.Flowchart.Nodes;
using Lomztein.ProjectAI.Flowchart.Nodes.Flow;
using Lomztein.ProjectAI.Flowchart.Nodes.Interfaces;
using Lomztein.ProjectAI.Flowchart.Nodes.Interfaces.Hooks;
using Lomztein.ProjectAI.UI.Editor.ProgramEditor.Workspace.Attachments;
using Lomztein.ProjectAI.Unity;
using UnityEngine;

namespace Lomztein.ProjectAI.UI.Editor.ProgramEditor.Workspace.NodeComponents
{
    public class ValueIO : NodeWidgetComponent
    {
        public override int Depth => 2;

        public RectTransform valueIn;
        public RectTransform valueOut;
        public RectTransform flowOut;

        public override Type[] ApplicableComponents => new Type[] { typeof(InputInterface), typeof(OutputInterface) };

        public override void LoadFrom(Node source)
        {
            void InstantiateValueHooks (IEnumerable<IVariableHook> hooks, GameObject prefabObject, Transform parent) {
                foreach (var hook in hooks)
                {
                    Debug.Log("Instantiating hook at this parent!", parent);
                    InstantiateHook(hook, prefabObject, parent, TypeColors.GetColor(hook.ValueType));
                }
            }

            void InstantiateHook (IHook hook, GameObject prefabObject, Transform parent, Color color)
            {
                GameObject newHookWidget = Instantiate(prefabObject, parent);
                HookAttachment hookAttachment = newHookWidget.GetComponent<HookAttachment>();
                hookAttachment.Initialize(hook, color);
            }

            InputInterface input = source.GetComponent<InputInterface>(x => x.Direction == Direction.In);
            if (input != null)
            {
                InstantiateValueHooks(input.IOHooks, HookAttachment.LeftHookWidget.Get(), valueIn);
            }
            else
            {
                Destroy(valueIn.gameObject);
            }

            OutputInterface output = source.GetComponent<OutputInterface>(x => x.Direction == Direction.Out);
            if (output != null)
            {
                InstantiateValueHooks(output.IOHooks, HookAttachment.RightHookWidget.Get(), valueOut);
            }
            else
            {
                Destroy(valueOut.gameObject);
            }
        }

    }

}