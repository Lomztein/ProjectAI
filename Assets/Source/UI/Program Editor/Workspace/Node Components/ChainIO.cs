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
        private ChainInterface component;

        public HookAttachment Input;
        public HookAttachment Output;

        public override Type[] ApplicableComponents => new Type[]
        {
            typeof (ChainInterface),
        };

        public override int Depth => -1;

        public override Position GetPosition()
        {
            return (Position)component.Direction;
        }

        public override void LoadFrom(INodeComponent source)
        {
            component = source as ChainInterface;

            if (component.Direction == Direction.In)
            {
                Input.Initialize(component.Hook, Color.white);
            }else
            {
                Destroy(Input.gameObject);
            }

            if (component.Direction == Direction.Out)
            {
                Output.Initialize(component.Hook, Color.white);
            } else
            {
                Destroy(Output.gameObject);
            }
        }
    }
}