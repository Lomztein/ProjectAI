using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lomztein.ProjectAI {

    public static class ObjectExtensions {

        public static object ConvertTo(this object input, Type type) {

            object result = Convert.ChangeType (input, type);
            return result;
        }

    }
}
