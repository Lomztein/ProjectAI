using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Lomztein.ProjectAI.UI {

    public static class UIExtensions {

        public static void CloseChildren  (this IWindowParent windowParent) {
            foreach (IWindow window in windowParent.Children)
                window.Close ();
        }

    }
}
