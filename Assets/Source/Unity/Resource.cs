using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Lomztein.ProjectAI.Unity {

    public class Resource {

        protected string Path { get; private set; }
        protected UnityEngine.Object Object { get; set; }

        public UnityEngine.Object Get() {
            if (Object == null)
                Object = Resources.Load (Path);
            return Object;
        }

        public Resource (string _path) {
            Path = _path;
        }

    }

    public class Resource<T> : Resource where T : UnityEngine.Object {

        protected new T Object { get { return base.Object as T; } set { base.Object = value as T; } }

        public new T Get () {
            if (Object == null)
                Object = Resources.Load<T> (Path);
            return Object;
        }

        public Resource(string _path) : base (_path) { }

    }
}
