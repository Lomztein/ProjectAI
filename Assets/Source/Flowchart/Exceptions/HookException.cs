using Lomztein.ProjectAI.Flowchart.Nodes.Interfaces.Hooks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lomztein.ProjectAI.Flowchart.Exceptions {

    public class HookException : FlowchartException {

        public IHook Hook { get { return Element as IHook; } protected set { Element = value; } }

        public HookException(string _message, IHook _hook) : this (_message, _hook, null) { }

        public HookException(string _message, IHook _hook, Exception _innerException) : base (_message, _hook, _innerException) { }

    }

}
