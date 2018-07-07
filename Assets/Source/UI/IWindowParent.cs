using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lomztein.ProjectAI.UI {

    public interface IWindowParent {

        List<IWindow> Children { get; set; }

    }
}
