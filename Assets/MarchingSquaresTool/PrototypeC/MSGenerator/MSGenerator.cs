using System;
using System.Runtime.CompilerServices;
using MarchingSquaresTool.PrototypeC.Core;
using MarchingSquaresTool.PrototypeC.FileLoader;
using MarchingSquaresTool.PrototypeC.MSGenerator.EdgeComp;
using MarchingSquaresTool.PrototypeC.MSGenerator.TriangleComp;
using UnityEngine;

namespace MarchingSquaresTool.PrototypeC.MSGenerator
{
    public class MSGenerator : MonoBehaviour
    {
        public BodyGrid Grid;

        private IBuildTriangles[] _triangleBuilders;
        private IBuildEdges[] _edgeBuilders;

        //Level editing
        private void OnValidate()
        {
            Init();
            Grid = new BodyGrid();
        }

        private void Awake()
        {
            
            Init();
        }

        private void Start()
        {
            Generate();
        }

        private void Init()
        {
            _triangleBuilders = GetComponents<IBuildTriangles>();
            _edgeBuilders = GetComponents<IBuildEdges>();
        }


        public void Generate()
        {
            //Start in the top left
            Vector2Int position = -Grid.Origin;
            Vector2Int size = Grid.Size;

            bool solid = false;

            for (int y = position.y; y < position.y + size.y - 1; y++)
            {
                for (int x = position.x; x < position.x + size.x - 1; x++)
                {
                    float[] cornerValues =
                    {
                        Grid[x,y].Terrain,
                        Grid[x + 1,y].Terrain,
                        Grid[x + 1,y + 1].Terrain,
                        Grid[x,y + 1].Terrain,
                    };
                    solid = solid || Grid[x, y].Solid;
                    solid = solid || Grid[x + 1, y].Solid;
                    solid = solid || Grid[x + 1, y + 1].Solid;
                    solid = solid || Grid[x, y + 1].Solid;


                    int index = 0;
                    for (int i = 0; i < 4; ++i)
                    {
                        index += (cornerValues[i] > 0 ? (1 << i) : 0);

                    }

                    //Zero values depend on surrounding values (easier to temporarily leave blank then update after)
                    Vector2[] vertices =
                    {
                        new Vector2(x,y),
                        Vector2.zero,
                        new Vector2(x + 1,y),
                        Vector2.zero,
                        new Vector2(x + 1,y + 1),
                        Vector2.zero,
                        new Vector2(x,y + 1),
                        Vector2.zero,
                    };

                    vertices[1] = MSHelperFunctions.findMidpointX(vertices[0], vertices[2], cornerValues[0], cornerValues[1]);
                    vertices[3] = MSHelperFunctions.findMidpointY(vertices[2], vertices[4], cornerValues[1], cornerValues[2]);
                    vertices[5] = MSHelperFunctions.findMidpointX(vertices[6], vertices[4], cornerValues[3], cornerValues[2]);
                    vertices[7] = MSHelperFunctions.findMidpointY(vertices[0], vertices[6], cornerValues[0], cornerValues[3]);

                    //Add triangle
                    for (int i = 0; MSHelperFunctions.triangleIndexTable[index, i] !=8; i += 3)
                    {
                        Triangle triangle = new Triangle();
                        
                        triangle.A = vertices[MSHelperFunctions.triangleIndexTable[index, i + 0]];
                        triangle.B = vertices[MSHelperFunctions.triangleIndexTable[index, i + 1]];
                        triangle.C = vertices[MSHelperFunctions.triangleIndexTable[index, i + 2]];

                        foreach (IBuildTriangles builder in _triangleBuilders)
                        {
                            builder.AddTriangle(triangle,new Vector2Int(x,y));
                        }
                    }
                    //Add edge
                    for (int i = 0; MSHelperFunctions.edgeIndexTable[index, i] != 8; i += 2)
                    {
                        Edge edge = new Edge();
                        
                        edge.A = vertices[MSHelperFunctions.edgeIndexTable[index, i + 0]];
                        edge.B = vertices[MSHelperFunctions.edgeIndexTable[index, i + 1]];

                        foreach (IBuildEdges builder in _edgeBuilders)
                        {
                            builder.AddEdge(edge,new Vector2Int(x,y));
                        }
                    }
                }
            }

            foreach (IBuildTriangles builder in _triangleBuilders)
            {
                builder.SetSolid(solid);
                builder.Build();
            }

            foreach (IBuildEdges builder in _edgeBuilders)
            {
                builder.SetSolid(solid);
                builder.Build();
            }
        }
    }
}