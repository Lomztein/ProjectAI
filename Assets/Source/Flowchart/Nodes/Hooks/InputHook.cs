using Lomztein.ProjectAI.Flowchart.Exceptions;
using Lomztein.ProjectAI.Flowchart.Nodes.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Lomztein.ProjectAI.Flowchart.Nodes.Hooks {

    public class InputHook : Hook, IVariableHook {

        public OutputHook ConnectedOutput { get {
                try {
                    return Connections.Single ().From as OutputHook;
                } catch (InvalidOperationException exception) {
                    throw new HookException ("Input isn't connected to an output.", this, exception);
                }
            }
        }

        public object Value { get { return GetValue (); } set { Debug.LogError ("You cannot set the value of an output hook."); } }
        public Type ValueType { get; set; }

        public object GetValue() {

            object value = ConnectedOutput.Value;

            if (ConnectedOutput.ValueType != ValueType) {
                value = value.ConvertTo (ValueType);
            }

            return value;
        }

        public T GetValue<T>() {
            try {
                return (T)GetValue ();
            } catch (NullReferenceException exception) {
                throw new HookException ("This value has not yet been set, please check execution chain.", ConnectedOutput, exception);
            }
        }

        public override IConnection CreateConnection() {
            return new VariableConnection ().SetProgram (ParentProgram) as VariableConnection;
        }

        public override void Init()
        {
            SetDirection(Direction.In);
            SetMaxConnections(1);
        }

        public InputHook SetType (Type type)
        {
            ValueType = type;
            return this;
        }
    }
}
