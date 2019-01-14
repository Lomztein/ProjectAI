using System;
using System.Collections;
using System.Collections.Generic;
using Lomztein.ProjectAI.Flowchart;
using Lomztein.ProjectAI.Flowchart.Nodes;
using Lomztein.ProjectAI.Flowchart.Nodes.Flow;
using Lomztein.ProjectAI.Flowchart.Nodes.Hooks;
using Lomztein.ProjectAI.UI.Editor.ProgramEditor.Workspace.Attachments;
using Lomztein.ProjectAI.Unity;
using UnityEngine;

namespace Lomztein.ProjectAI.UI.Editor.ProgramEditor.Workspace.NodeComponents
{
    public class ValueIO : NodeComponent
    {
        private static readonly Resource<GameObject> FlowOutputHook = new Resource<GameObject>("UI/Flowchart/FlowOutputHook");

        public RectTransform inputSide;
        public RectTransform outputSide;
        public RectTransform flowSide;

        public override Type[] ApplicableTypes => new Type[]
        {
            typeof (IHasInput),
            typeof (IHasOutput),
            typeof (IFlowNode)
        };

        public override int Depth => 2;

        public override void LoadFrom(Node source)
        {
            void InstantiateValueHooks (IEnumerable<IVariableHook> hooks, GameObject prefabObject, Transform parent) {
                foreach (var hook in hooks)
                {
                    InstantiateHook(hook, prefabObject, parent, TypeColors.GetColor(hook.ValueType));
                }
            }

            void InstantiateHook (IHook hook, GameObject prefabObject, Transform parent, Color color)
            {
                GameObject newHookWidget = Instantiate(prefabObject, parent);
                HookAttachment hookAttachment = newHookWidget.GetComponent<HookAttachment>();
                hookAttachment.Initialize(hook, color);
            }

            if (source is IHasInput input)
            {
                InstantiateValueHooks(input.InputHooks, HookAttachment.LeftHookWidget.Get(), inputSide);
            }else
            {
                Destroy(inputSide.gameObject);
            }

            if (source is IHasOutput output)
            {
                InstantiateValueHooks(output.OutputHooks, HookAttachment.RightHookWidget.Get(), outputSide);
            }else
            {
                Destroy(outputSide.gameObject);
            }

            if (source is IFlowNode flowNode)
            {
                foreach (var hook in flowNode.PossibleRoutes)
                {
                    InstantiateHook(hook, FlowOutputHook.Get(), flowSide, Color.white);
                }
            }
            else
            {
                Destroy(flowSide.gameObject);
            }
        }
    }

}