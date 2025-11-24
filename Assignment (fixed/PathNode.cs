using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Assignment__fixed
{
    public class PathNode
    {
        private Coordinate _location;
        private int _cost = 0; //heuristics
        private int _score = 0; //terrain
        private PathNode? _predecessor = null;
    }
}
