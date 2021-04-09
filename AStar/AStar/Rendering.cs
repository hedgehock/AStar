using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;

namespace AStar
{
    class Rendering
    {
        public int windowSize;
        public int gridSize;
        public int gap;

        public List<Node> nodes = new List<Node>();
        public Node startNode = null;
        public Node endNode = null;

        public Rendering(int _windowSize, int _gridSize)
        {
            windowSize = _windowSize;
            gridSize = _gridSize;
            gap = windowSize / _gridSize;

            CreateList();
        }

        void CreateList()
        {
            for (int x = gridSize - 1; x >= 0; x--)
            {
                for (int y = gridSize - 1; y >= 0; y--)
                {
                    nodes.Add(new Node("Empty", x, y));
                }
            }
        }

        public void RenderContext()
        {
            Raylib.ClearBackground(Color.GRAY);

            // Nodes
            foreach (Node node in nodes)
            {
                if (node.type == "Empty")
                    Raylib.DrawRectangle(gap * node.x, gap * node.y, gap, gap, Color.LIGHTGRAY);
                else if (node.type == "Start")
                    Raylib.DrawRectangle(gap * node.x, gap * node.y, gap, gap, Color.ORANGE);
                else if (node.type == "End")
                    Raylib.DrawRectangle(gap * node.x, gap * node.y, gap, gap, Color.PURPLE);
                else if (node.type == "Obstacle")
                    Raylib.DrawRectangle(gap * node.x, gap * node.y, gap, gap, Color.DARKGRAY);
                else if (node.type == "Neighbor")
                    Raylib.DrawRectangle(gap * node.x, gap * node.y, gap, gap, Color.GREEN);
                else if (node.type == "Calculated")
                    Raylib.DrawRectangle(gap * node.x, gap * node.y, gap, gap, Color.RED);
                else if (node.type == "Path")
                    Raylib.DrawRectangle(gap * node.x, gap * node.y, gap, gap, Color.PINK);
            }

            // Grid
            for (int x = gridSize - 1; x > 0; x--)
            {
                Raylib.DrawLine(gap * x, 0, gap * x, windowSize, Color.WHITE);

                for (int y = gridSize - 1; y > 0; y--)
                {
                    Raylib.DrawLine(0, gap * y, windowSize, gap * y, Color.WHITE);
                }
            }
        }
    }
}
