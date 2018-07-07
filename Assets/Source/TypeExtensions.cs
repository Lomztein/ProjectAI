using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lomztein.ProjectAI {

    public static class TypeExtensions {

        public static object CreateObject (this Type type) {
            return type.IsValueType ? Activator.CreateInstance (type) : null;
        }

        public static bool IsConvertibleTo(this Type type, Type otherType) {
            if (type == otherType)
                return true;

            if (type.IsSubclassOf (otherType))
                return true;

            object convertTest = CreateObject (type);

            try {
                convertTest.ConvertTo (otherType);
                return true;
            } catch (Exception) {
                return false;
            }

        }

    }
}
