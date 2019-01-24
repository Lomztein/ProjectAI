using Lomztein.ProjectAI.Flowchart.Nodes;
using Lomztein.ProjectAI.Flowchart.Nodes.Interfaces;
using Lomztein.ProjectAI.Flowchart.Nodes.Interfaces.Hooks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.ProjectAI.Flowchart.Nodes.Interfaces
{
    public abstract class IOInterface<THook> : NodeInterface where THook : IVariableHook
    {
        public override List<IHook> InterfaceHooks { get; set; } = new List<IHook>();

        public List<THook> IOHooks { get => InterfaceHooks.Cast<THook>().ToList(); }

        public override void Delete()
        {
            InterfaceHooks.ForEach(x => x.DisconnectAll());
        }

        public override void Init(Node parent)
        {
            foreach (IHook hook in InterfaceHooks)
                hook.Init();
        }

        public IOInterface<THook> SetHooks(params THook[] inputs)
        {
            if (InterfaceHooks.Count > 0)
                throw new InvalidOperationException("Input has already been set!");

            InterfaceHooks.AddRange(inputs.Cast<IHook>());
            return this;
        }

        public THook GetHook(string name)
        {
            return IOHooks.Find (x => x.Name == name);
        }
    }


    public class InputInterface : IOInterface<InputHook> {
        public override Direction Direction { get => Direction.In; set => throw new InvalidOperationException("Can't change direction of a IO interface."); }

        public T Get<T>(string name)
        {
            return (T)GetHook (name).GetValue<T>();
        }


    }

    public class OutputInterface : IOInterface<OutputHook> {

        public override Direction Direction { get => Direction.Out; set => throw new InvalidOperationException("Can't change direction of a IO interface."); }

        public void Set(string name, object value)
        {
            GetHook (name).Value = value;
        }
    }

}
