using System;
using System.Collections.Generic;
using Lomztein.ProjectAI.Flowchart.Nodes;
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

        public override Type[] ApplicableComponents => new Type[] { typeof(InputInterface), typeof(OutputInterface) };

        private INodeInterface component;

        public override Position GetPosition()
        {
            throw new NotImplementedException();
        }

        public override void LoadFrom(INodeComponent source)
        {
            component = source as INodeInterface;

            void InstantiateValueHooks (IEnumerable<IVariableHook> hooks, GameObject prefabObject) {
                foreach (var hook in hooks)
                {
                    InstantiateHook(hook, prefabObject, transform, TypeColors.GetColor(hook.ValueType));
                }
            }

            void InstantiateHook (IHook hook, GameObject prefabObject, Transform parent, Color color)
            {
                GameObject newHookWidget = Instantiate(prefabObject, parent);
                HookAttachment hookAttachment = newHookWidget.GetComponent<HookAttachment>();
                hookAttachment.Initialize(hook, color);
            }

            if (component is InputInterface input)
            {
                InstantiateValueHooks(input.IOHooks, HookAttachment.LeftHookWidget.Get());
            }

            if (source is OutputInterface output)
            {
                InstantiateValueHooks(output.IOHooks, HookAttachment.RightHookWidget.Get());
            }
        }

    }

}