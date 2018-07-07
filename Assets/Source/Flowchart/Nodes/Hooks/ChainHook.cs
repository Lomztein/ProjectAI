using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lomztein.ProjectAI.Flowchart.Nodes.Connections;

namespace Lomztein.ProjectAI.Flowchart.Nodes.Hooks {

    public class ChainHook : Hook {

        public override IConnection CreateConnection() {
            return new ChainConnection (ParentProgram);
        }

        public ChainHook(Program _parentProgram, Node _parent, Direction _direction) : base (_parentProgram, _parent, _direction, 0) {

            Direction = _direction;

            switch (_direction) {
                case Direction.In:
                    Name = "Chain Input";
                    Description = "Hook a chain output up to this, in order to chain those two together.";
                    break;

                case Direction.Out:
                    Name = "Chain Output";
                    Description = "Hook this up to a chain input in order to further the chain.";
                    break;

                case Direction.Uni:
                    Name = "Universal Chain";
                    Description = "Hook either an input or output to this.";
                    break;

                default:
                    Name = "Unsupported";
                    Description = "This hook direction is unsupported and shouldn't exist, go tell the dev that he fucked up.";
                    break;
            }

        }
    }
}
