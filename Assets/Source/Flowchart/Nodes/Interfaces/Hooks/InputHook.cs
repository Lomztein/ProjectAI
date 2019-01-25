using Lomztein.ProjectAI.Flowchart.Exceptions;
using Lomztein.ProjectAI.Flowchart.Nodes.Connections;
using Lomztein.ProjectAI.Serialization;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Lomztein.ProjectAI.Flowchart.Nodes.Interfaces.Hooks {

    public class InputHook : Hook, IVariableHook, IJsonSerializable {

        private object constant;

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
            VariableConnection conn = new VariableConnection ().SetProgram (ParentProgram) as VariableConnection;
            conn.Init();
            return conn;
        }

        public override void Init()
        {
            SetDirection(Direction.In);
            SetMaxConnections(1);

            if (Nullable.GetUnderlyingType (ValueType) == null)
            {
                constant = Activator.CreateInstance(ValueType);
            }
        }

        public InputHook SetType (Type type)
        {
            ValueType = type;
            return this;
        }

        public JToken Serialize()
        {
            return new JValue (constant);
        }

        public void Deserialize(JToken source)
        {
            constant = source.ToObject(ValueType);
        }
    }
}
