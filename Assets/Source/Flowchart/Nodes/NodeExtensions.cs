using Lomztein.ProjectAI.Flowchart.Nodes.Flow;
using Lomztein.ProjectAI.Flowchart.Nodes.Hooks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lomztein.ProjectAI.Flowchart.Nodes {
    public static class NodeExtensions {

        public static T Get<T>(this IHasInput node, string name) {
            return (T)GetInput (node, name).GetValue<T> ();
        }

        public static void Set(this IHasOutput node, string name, object value) {
            GetOutput (node, name).Value = value;
        }

        public static InputHook GetInput(this IHasInput node, string name) {
            return FindHookByName (node.InputHooks, name) as InputHook;
        }

        public static OutputHook GetOutput(this IHasOutput node, string name) {
            return FindHookByName (node.OutputHooks, name) as OutputHook;
        }

        private static IHook FindHookByName (IEnumerable<Hook> hooks, string name) {
            foreach (var hook in hooks) {
                if (hook.Name == name)
                    return hook;
            }

            return null;
        }

        public static IHasInput SetInputs(this IHasInput inputNode, params InputHook[] inputs) {

            if (inputNode.InputHooks != null)
                throw new InvalidOperationException("Input has already been set!");

            inputNode.InputHooks = inputs;
            (inputNode as Node).AddHooks(inputs);

            return inputNode;
        }

        public static IHasOutput SetOutputs(this IHasOutput outputNode, params OutputHook[] outputs) {

            if (outputNode.OutputHooks != null)
                throw new InvalidOperationException("Output has already been set!");

            outputNode.OutputHooks = outputs;
            (outputNode as Node).AddHooks(outputs);
            return outputNode;
        }

        public static ChainHook GetHook (this IFlowNode node, string name) {
            return node.PossibleRoutes.First (x => x.Name == name);
        }
    }
}
