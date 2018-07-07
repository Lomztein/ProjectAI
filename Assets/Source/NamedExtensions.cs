using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lomztein.ProjectAI {

    public static class NamedExtensions {

        public static void CopyTo (this INamed from, INamed to) {
            to.Name = from.Name;
            to.Description = from.Description;
        }

    }
}
