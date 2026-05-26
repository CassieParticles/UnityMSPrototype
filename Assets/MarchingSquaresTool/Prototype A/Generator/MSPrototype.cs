using System;
using System.Collections.Generic;
using UnityEngine;

namespace MarchingSquaresTool.Scripts
{
    [RequireComponent(typeof(VoxelGridComp))]
    public class MSPrototype : MonoBehaviour
    {
        private VoxelGridComp grid;
        private EdgeCollider2D edgeCollider2D;

        private IBuildTriangles[] triangleBuilders;
        private IBuildEdges[] edgeBuilders;

        private void Awake()
        {
            grid = GetComponent<VoxelGridComp>();
            edgeCollider2D = GetComponent<EdgeCollider2D>();

            triangleBuilders = GetComponents<IBuildTriangles>();
            edgeBuilders = GetComponents<IBuildEdges>();
        }

        private void Start()
        {
            Generate();
        }

        public void Generate()
        {
            List<Vector2> edgeVertices = new List<Vector2>();
            
            int width = grid.grid.GetSize().x;
            int height = grid.grid.GetSize().y;

            for (int y = 0; y < height - 1; ++y)
            {
                for (int x = 0; x < width - 1; ++x)
                {
                    float[] cornerValues =
                    {
                        grid.grid[x,y],
                        grid.grid[x + 1,y],
                        grid.grid[x + 1,y + 1],
                        grid.grid[x,y + 1],
                    };

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
                    
                    //Triangle assembly
                    for (int i = 0; MSHelperFunctions.triangleIndexTable[index,i] != 8; i += 3)
                    {
                        //Assemble triangle
                        Triangle triangle = new Triangle();
                        
                        triangle.A = vertices[MSHelperFunctions.triangleIndexTable[index,i + 0]];
                        triangle.B = vertices[MSHelperFunctions.triangleIndexTable[index,i + 1]];
                        triangle.C = vertices[MSHelperFunctions.triangleIndexTable[index,i + 2]];
                        
                        //TODO: Use array of abstract triangle builders
                        foreach (var builder in triangleBuilders)
                        {
                            builder.AddTriangle(triangle,new Vector2Int(x,y));
                        }
                    }
                    
                    
                    //Edge assembly
                    for (int i = 0; MSHelperFunctions.edgeIndexTable[index,i] != 8; i += 2)
                    {
                        Edge edge = new  Edge();
                        edge.A = vertices[MSHelperFunctions.edgeIndexTable[index,i + 0]];
                        edge.B = vertices[MSHelperFunctions.edgeIndexTable[index,i + 1]];

                        foreach (var builder in edgeBuilders)
                        {
                            builder.AddEdge(edge,new Vector2Int(x,y));
                        }
                    }
                }
            }
            
            foreach (var builder in triangleBuilders)
            {
                builder.Build();
            }

            foreach (var builder in edgeBuilders)
            {
                builder.Build();
            }
        }
    }
}