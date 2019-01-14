using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Lomztein.ProjectAI
{
    public static class Preconditions
    {
        public static void IsType<T>(object input, out T output)
        {
            Type type = typeof(T);
            if (type.IsInstanceOfType (input))
            {
                output = (T)input;
                return;
            }
            throw new InvalidCastException("Object " + input.ToString() + " was not a " + type.Name);
        }

        public static bool IsAnyType(object input, params Type[] types)
        {
            if (types.Any (x => x.IsInstanceOfType (input))) {
                return true;
            }
            throw new InvalidCastException("Object was none of the given types.");
        }
    }
}