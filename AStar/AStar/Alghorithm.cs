using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AStar
{
    class Alghorithm
    {
        Rendering rendering;

        bool foundEnd = false;
        bool foundStart = false;

        public Alghorithm(Rendering _rendering)
        {
            rendering = _rendering;
        }

        void AddNeighbors(Node _node)
        {
            // Down
            if (_node.x < rendering.gridSize)
                foreach (Node node in rendering.nodes)
                    if (node.x == (_node.x + 1) && node.y == _node.y)
                        if (node.type == "Empty")
                            node.type = "Neighbor";
                        else if (node.type == "End")
                            foundEnd = true;

            // Up
            if (_node.x > 0)
                foreach (Node node in rendering.nodes)
                    if (node.x == (_node.x - 1) && node.y == _node.y)
                        if (node.type == "Empty")
                            node.type = "Neighbor";
                        else if (node.type == "End")
                            foundEnd = true;

            // Right
            if (_node.y < rendering.gridSize) 
                foreach (Node node in rendering.nodes)
                    if (node.y == (_node.y + 1) && node.x == _node.x)
                        if (node.type == "Empty")
                            node.type = "Neighbor";
                        else if (node.type == "End")
                            foundEnd = true;

            // Left
            if (_node.y > 0)
                foreach (Node node in rendering.nodes)
                    if (node.y == (_node.y - 1) && node.x == _node.x)
                        if (node.type == "Empty")
                            node.type = "Neighbor";
                        else if (node.type == "End")
                            foundEnd = true;
        }

        void Calculate()
        {
            List<float> fscores = new List<float>();
            List<float> hscores = new List<float>();

            Console.WriteLine("//////////////////////////");
            Console.WriteLine("Start of calculating");
            Console.WriteLine("//////////////////////////");

            foreach (Node node in rendering.nodes)
            {
                if (node.type == "Neighbor")
                {
                    float gscore = MathF.Abs(node.x - rendering.startNode.x) + MathF.Abs(node.y - rendering.startNode.y); //MathF.Sqrt(MathF.Pow(rendering.startNode.y - node.y, 2) + MathF.Pow(rendering.startNode.x - node.x, 2));
                    float hscore = MathF.Abs(node.x - rendering.endNode.x) + MathF.Abs(node.y - rendering.endNode.y);//MathF.Sqrt(MathF.Pow(rendering.endNode.y - node.y, 2) + MathF.Pow(rendering.endNode.x - node.x, 2));
                    float fscore = gscore + hscore;

                    node.fscore = fscore;
                    node.hscore = hscore;
                    node.gscore = gscore;

                    fscores.Add(node.fscore);
                    //hscores.Add(hscore);

                    // Debug
                    Console.WriteLine("X: " + node.x);
                    Console.WriteLine("Y: " + node.y);
                    //Console.WriteLine("StartNode X: " + rendering.startNode.x);
                    //Console.WriteLine("StartNode Y: " + rendering.startNode.y);
                    //Console.WriteLine("EndNode X: " + rendering.endNode.x);
                    //Console.WriteLine("EndNode Y: " + rendering.endNode.y);
                    //Console.WriteLine("G:" + gscore);
                    //Console.WriteLine("H:" + hscore);
                    Console.WriteLine("F:" + fscore);
                    Console.WriteLine("////////////");
                }
            }

            Console.WriteLine("//////////////////////////");
            Console.WriteLine("End of calculating");
            Console.WriteLine("//////////////////////////");

            fscores.Sort();

            foreach (Node node in rendering.nodes)
                if (node.fscore == fscores[0] && node.type == "Neighbor")
                    hscores.Add(node.hscore);
            
            hscores.Sort();

            foreach (Node node in rendering.nodes)
                if (node.hscore == hscores[0])
                {
                    node.type = "Calculated";
                    Console.WriteLine("Winner: " + node.x + "," + node.y);
                    Console.WriteLine(node.type);
                }
                    
        }

        void CalculatePath()
        {
            foreach (Node node in rendering.nodes)
            {
                if (node.type == "Calculated")
                {
                    float gscore = MathF.Sqrt(MathF.Pow(rendering.startNode.y - node.y, 2) + MathF.Pow(rendering.startNode.x - node.x, 2));
                    node.gscore = gscore;
                }
            }
        }

        void MakePath(Node _node)
        {
            Console.WriteLine("MAKEPATH INIT");

            List<Node> validNeighbors = new List<Node>();
            List<float> gscores = new List<float>();

            // Down
            if (_node.x < rendering.gridSize)
                foreach (Node node in rendering.nodes)
                    if (node.x == (_node.x + 1) && node.y == _node.y)
                        if (node.type == "Calculated")
                            validNeighbors.Add(node);
                        else if (node.type == "Start")
                        {
                            foundStart = true;
                            Console.WriteLine("Found Start!");
                        }
                            

            // Up
            if (_node.x > 0)
                foreach (Node node in rendering.nodes)
                    if (node.x == (_node.x - 1) && node.y == _node.y)
                        if (node.type == "Calculated")
                            validNeighbors.Add(node);
                        else if (node.type == "Start")
                        {
                            foundStart = true;
                            Console.WriteLine("Found Start!");
                        }

            // Right
            if (_node.y < rendering.gridSize)
                foreach (Node node in rendering.nodes)
                    if (node.y == (_node.y + 1) && node.x == _node.x)
                        if (node.type == "Calculated")
                            validNeighbors.Add(node);
                        else if (node.type == "Start")
                        {
                            foundStart = true;
                            Console.WriteLine("Found Start!");
                        }

            // Left
            if (_node.y > 0)
                foreach (Node node in rendering.nodes)
                    if (node.y == (_node.y - 1) && node.x == _node.x)
                        if (node.type == "Calculated")
                            validNeighbors.Add(node);
                        else if (node.type == "Start")
                        {
                            foundStart = true;
                            Console.WriteLine("Found Start!");
                        }

            foreach (Node node in validNeighbors)
            {
                gscores.Add(node.gscore);
            }

            gscores.Sort();

            Console.WriteLine("SORTING");

            foreach (Node node in validNeighbors)
            {
                if (node.gscore == gscores[0] && foundStart == false)
                {
                    node.type = "Path";
                    //Thread.Sleep(50);
                    MakePath(node);
                }
            }
        }

        public void StartAlghorithm()
        {
            AddNeighbors(rendering.startNode);

            Calculate();

            while (foundEnd == false)
            {
                foreach (Node node in rendering.nodes)
                {
                    if (node.type == "Calculated")
                    {
                        AddNeighbors(node);
                    }
                }

                if (foundEnd == false)
                {
                    Calculate();

                    //Thread.Sleep(10);
                }
            }

            CalculatePath();
            MakePath(rendering.endNode);

        }
    }
}
