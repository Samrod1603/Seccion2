using System;
using System.Collections.Generic;
using System.Text;

namespace Seccion2
{
    class SearchPath
    {
        class box // "nodo"
        {
            public bool IsExplored = false;
            public box isExploredFrom;
            public int X { get; set; }
            public int Y { get; set; }

            public box(int x, int y) //constructor
            {
                X = x;
                Y = y;
            }
        }
        struct Value //para guardar la referencia de la posicion de la clase box 
        {
            public int X { get; set; }
            public int Y { get; set; }
            public Value(int x, int y)
            {
                X = x;
                Y = y;
            }
        }

        public class searchPath
        {
            private static box _startingPoint = new box(0, 0);
            private static box _endingPoint = new box(2, -2);
            private static box _searchingPoint;                           // Current node we are searching
            private static bool _isExploring = true;

            static int[][] directions = new int[4][]; //jagged array para las direcciones 

            private static Dictionary<Value, box> storage = new Dictionary<Value, box>(); // almacenamiento de cada nodo del terreno
            private static List<box> path = new List<box>();                              // almacenamiento del path   
            private static Queue<box> tail = new Queue<box>();


            public static void CreateBlocks()
            {
                storage.Add(new Value(0, 0), _startingPoint); //*
                storage.Add(new Value(1, 0), new box(1, 0));
                storage.Add(new Value(2, 0), new box(2, 0));
                storage.Add(new Value(3, 0), new box(3, 0));
                storage.Add(new Value(0, -1), new box(0, -1));
                storage.Add(new Value(1, -1), new box(1, -1));
                storage.Add(new Value(2, -1), new box(2, -1));
                storage.Add(new Value(3, -1), new box(3, -1));
                storage.Add(new Value(0, -2), new box(0, -2));
                storage.Add(new Value(2, -2), _endingPoint); //*
                storage.Add(new Value(3, -2), new box(3, -2));
                storage.Add(new Value(0, -3), new box(0, -3));
                storage.Add(new Value(1, -3), new box(1, -3));
                storage.Add(new Value(3, -3), new box(3, -2));
                // no añadí los huecos en el diccionario 
            }

            // BFS; For finding the shortest path
            public static void BFS()
            {
                tail.Enqueue(_startingPoint);
                while (tail.Count > 0 && _isExploring)
                {
                    _searchingPoint = tail.Dequeue();
                    OnReachingEnd();
                    ExploreNeighbourNodes();
                }
            }
            // To check if we've reached the Ending point
            private static void OnReachingEnd()
            {
                if (_searchingPoint == _endingPoint)
                {
                    _isExploring = false;
                }
                else
                {
                    _isExploring = true;
                }
            }
            private static void ExploreNeighbourNodes() 
            {
                if (!_isExploring) { return; }

                directions[0] = new int[] { 0, -1 }; //abajo
                directions[1] = new int[] { 0, 1 }; //arriba
                directions[2] = new int[] { 1, 0 }; //derecha
                directions[3] = new int[] { -1, 0 }; //izquierda

                for (int i = 0; i < 4; i++)
                {
                    int neighbourPosX = _searchingPoint.X + directions[i][0];
                    int neighbourPosY = _searchingPoint.Y + directions[i][1];
                    Value neighbourPos = new Value(neighbourPosX, neighbourPosY); // estructura de vector usada para recorrer los vecinos

                    if (storage.ContainsKey(neighbourPos))
                    {
                        box box = storage[neighbourPos];
                        if (!box.IsExplored)
                        {
                            tail.Enqueue(box);                       
                            box.IsExplored = true;
                            box.isExploredFrom = _searchingPoint;     
                        }
                    }

                }
            }
            // For adding nodes to the path
            private static void SetPath(box node)
            {
                path.Add(node);
            }
            public static void CreatePath()
            {
                SetPath(_endingPoint);
                box previousNode = _endingPoint.isExploredFrom;

                while (previousNode != _startingPoint)
                {
                    SetPath(previousNode);
                    previousNode = previousNode.isExploredFrom;
                }

                SetPath(_startingPoint);
                path.Reverse();

            }
            public static void solution()
            {
                Console.WriteLine("el camino mas corto es:");
                Console.WriteLine();
                foreach (var i in path)
                {
                    Console.WriteLine(i.X + "," + i.Y);

                }
                Console.WriteLine();

            }




        }
    }
}
