using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Raylib_cs;

namespace AStar
{
    class Input
    {
        Rendering rendering;
        Alghorithm alghorithm;

        public Input(Rendering _rendering, Alghorithm _alghorithm)
        {
            rendering = _rendering;
            alghorithm = _alghorithm;
        }

        public void HandleInput()
        {
            // Mouse
            if (Raylib.IsMouseButtonDown(MouseButton.MOUSE_LEFT_BUTTON))
            {
                foreach (Node node in rendering.nodes)
                    if (node.x == (Raylib.GetMouseX() / rendering.gap) && node.y == (Raylib.GetMouseY() / rendering.gap))
                        if (node.type == "Empty")
                        {
                            if (rendering.startNode == null)
                            {
                                node.type = "Start";
                                rendering.startNode = node;
                            }
                            else if (rendering.endNode == null)
                            {
                                node.type = "End";
                                rendering.endNode = node;
                            }
                            else
                                node.type = "Obstacle";
                        }
            }
            else if (Raylib.IsMouseButtonDown(MouseButton.MOUSE_RIGHT_BUTTON))
            {
                foreach (Node node in rendering.nodes)
                    if (node.x == (Raylib.GetMouseX() / rendering.gap) && node.y == (Raylib.GetMouseY() / rendering.gap))
                    {
                        if (node.type == "Start")
                            rendering.startNode = null;
                        else if (node.type == "End")
                            rendering.endNode = null;

                        node.type = "Empty";
                    } 
            }

            // Key
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
            {
                Thread alghorithmThread = new Thread(new ThreadStart(alghorithm.StartAlghorithm));
                alghorithmThread.Start();
            }
        }
    }
}
