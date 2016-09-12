using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TakGame
{
    class Board
    {
        public int Dimension { get; private set; }

        public Board(int dimension)
        {
            Dimension = dimension;
        }
    }
}
