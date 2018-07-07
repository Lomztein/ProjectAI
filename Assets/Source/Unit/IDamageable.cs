using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lomztein.ProjectAI.Unit {
    interface IDamageable : IHasHealth {

        void Damage(float damage);

    }
}
