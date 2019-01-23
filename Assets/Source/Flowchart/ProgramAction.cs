using Flowchart.Nodes.Interfaces;
using Lomztein.ProjectAI.Flowchart.Nodes;
using Lomztein.ProjectAI.Flowchart.Nodes.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Lomztein.ProjectAI.Flowchart {

    public class ProgramAction {

        public Action<InputInterface, OutputInterface> InternalAction { get; set; }

        public List<ActionIO> Inputs { get; set; }
        public List<ActionIO> Outputs { get; set; }

        public void Execute (InputInterface input, OutputInterface output) {
            InternalAction (input, output);
        }

        public ProgramAction AddInput (Type type, string name, string description) {
            if (Inputs == null)
                Inputs = new List<ActionIO> ();

            ActionIO newIO = new ActionIO (type, name, description);
            Inputs.Add (newIO);
            return this;
        }

        public ProgramAction AddOutput(Type type, string name, string description) {
            if (Outputs == null)
                Outputs = new List<ActionIO> ();

            ActionIO newIO = new ActionIO (type, name, description);
            Outputs.Add (newIO);
            return this;
        }

        public ProgramAction(Action<InputInterface, OutputInterface> _action) {
            InternalAction = _action;
            Inputs = new List<ActionIO> ();
            Outputs = new List<ActionIO> ();
        }

        public class ActionIO : INamed {

            public Type Type { get; private set; }
            public string Name { get; set; }
            public string Description { get; set; }

            public ActionIO (Type _type, string _name, string _description) {
                Type = _type;
                Name = _name;
                Description = _description;
            }

        }

    }
}
