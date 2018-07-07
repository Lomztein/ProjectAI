using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Lomztein.ProjectAI.UI.Editor.ProgramEditor {

    public static class TypeColors {

        public static TypeColor[] Colors = new TypeColor[] {
            new TypeColor (typeof (string), Color.white),
            new TypeColor (typeof (int), Color.blue),
            new TypeColor (typeof (long), Color.blue),
            new TypeColor (typeof (float), Color.yellow),
            new TypeColor (typeof (double), Color.yellow),
            new TypeColor (typeof (bool), Color.red),
        };

        public static Color GetColor (Type type) {
            TypeColor tc = Colors.FirstOrDefault (x => x.Type == type || x.Type.IsSubclassOf (type));
            if (tc != null)
                return tc.Color;
            return Color.white;
        }

        public class TypeColor {

            public Type Type { get; private set; }
            public Color Color { get; private set; }

            public TypeColor (Type _type, Color _color) {
                Type = _type;
                Color = _color;
            }

        }

    }
}
