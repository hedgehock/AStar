using System;
using System.Collections.Generic;
using System.Text;

namespace AStar
{
    class Node
    {
        public string type;

        public int x;
        public int y;
        
        public float gscore = 0;
        public float fscore = 0;
        public float hscore = 0;

        public Node(string _type, int _x, int _y)
        {
            x = _x;
            y = _y;
            type = _type;
        }
    }
}
