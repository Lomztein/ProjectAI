using System;
using System.Collections;
using System.Collections.Generic;
using Lomztein.ProjectAI.Flowchart.Nodes;
using Lomztein.ProjectAI.Flowchart.Nodes.Interfaces;
using Lomztein.ProjectAI.UI.Editor.ProgramEditor.Workspace.Attachments;
using UnityEngine;

namespace Lomztein.ProjectAI.UI.Editor.ProgramEditor.Workspace.NodeComponents
{
    public class ChainIO : NodeWidgetComponent
    {
        public HookAttachment Input;
        public HookAttachment Output;

        public override Type[] ApplicableComponents => new Type[]
        {
            typeof (ChainInterface),
        };

        public override int Depth => -1;

        public override void LoadFrom(Node source)
        {
            ChainInterface inInterface = source.GetComponent<ChainInterface>(x => x.Direction == Direction.In);
            ChainInterface outInterface = source.GetComponent<ChainInterface>(x => x.Direction == Direction.Out);

            if (inInterface != null)
            {
                Input.Initialize(inInterface.Hook, Color.white);
            }else
            {
                Destroy(Input.gameObject);
            }

            if (outInterface != null)
            {
                Output.Initialize(outInterface.Hook, Color.white);
            } else
            {
                Destroy(Output.gameObject);
            }
        }
    }
}