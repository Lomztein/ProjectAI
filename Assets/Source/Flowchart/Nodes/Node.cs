using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.ProjectAI.Flowchart.Nodes {

    public abstract class Node : FlowchartElement, INamed, IDeletable {

        public virtual string Name { get; set; }
        public virtual string Description { get; set; }

        public Vector2 Position { get; set; }
        public uint LastActiveTick { get; set; }

        public event OnDeletedEvent OnDeleted;

        public virtual void Delete() {
            if (OnDeleted != null)
                OnDeleted ();

            ParentProgram.RemoveNode (this);
        }

        public Node (Program _parentProgram) : base (_parentProgram) {
            if (ParentProgram)
                ParentProgram.AddNode (this);
        }
    }
}