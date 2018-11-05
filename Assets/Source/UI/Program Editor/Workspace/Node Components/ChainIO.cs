using System;
using System.Collections;
using System.Collections.Generic;
using Lomztein.ProjectAI.Flowchart;
using Lomztein.ProjectAI.Flowchart.Nodes;
using Lomztein.ProjectAI.UI.Editor.ProgramEditor.Workspace.Attachments;
using UnityEngine;

namespace Lomztein.ProjectAI.UI.Editor.ProgramEditor.Workspace.NodeComponents
{
    public class ChainIO : NodeComponent
    {
        public HookAttachment Input;
        public HookAttachment Output;

        public override Type[] ApplicableTypes => new Type[]
        {
            typeof (INextNode),
            typeof (IPrevNode),
        };

        public override int Depth => -1;

        public override void LoadFrom(IFlowchartElement source)
        {
            if (source is INextNode next)
            {
                Input.Initialize(next.PreviousHook, Color.white);
            }else
            {
                Destroy(Input.gameObject);
            }

            if (source is IPrevNode prev)
            {
                Output.Initialize(prev.NextHook, Color.white);
            } else
            {
                Destroy(Output.gameObject);
            }
        }
    }
}