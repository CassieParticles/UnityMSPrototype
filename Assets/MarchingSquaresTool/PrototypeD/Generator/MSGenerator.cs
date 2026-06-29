using System;
using System.Collections;
using System.Collections.Generic;
using MarchingSquaresTool.PrototypeD.Core;
using UnityEngine;

namespace MarchingSquaresTool.PrototypeD.Generator
{
    public class MSGenerator : MonoBehaviour
    {
        [SerializeField]
        public BodyGrid grid;
        
        private List<ATriangleBuilder> _triangleBuilders = new List<ATriangleBuilder>();
        private List<AEdgeBuilder> _edgeBuilders = new List<AEdgeBuilder>();

        protected void Awake()
        {
            InitializeComponents(false);
        }
        
        protected void OnValidate()
        {
            InitializeComponents(true);
        }

        public void InitializeComponents(bool editor)
        {
            _triangleBuilders.Clear();
            _edgeBuilders.Clear();
            
            ATriangleBuilder[] tri = GetComponents<ATriangleBuilder>();
            AEdgeBuilder[] edge = GetComponents<AEdgeBuilder>();

            //Add only relevant builders
            foreach (ATriangleBuilder builder in tri)
            {
                if (editor && builder.IsEditor() || !editor && builder.IsGame())
                {
                    _triangleBuilders.Add(builder);
                }
            }
            foreach (AEdgeBuilder builder in edge)
            {
                if (editor && builder.IsEditor() || !editor && builder.IsGame())
                {
                    _edgeBuilders.Add(builder);
                }
            }
            
        }

        private void Start()
        {
            Rebuild();
        }
        
        public void Rebuild()
        {
            Clear();
            SeparateGrid();
            grid.AddBorder();
            Generate();
        }
        
        private void SeparateGrid()
        {
            //Add border (ensures +ve values aren't on edge)
            grid.AddBorder();
            
            Vector2Int[] neighbors = new Vector2Int[8]
            {
                new Vector2Int( 1, 0),
                new Vector2Int( 1, 1),
                new Vector2Int( 0, 1),
                new Vector2Int(-1, 1),
                new Vector2Int(-1, 0),
                new Vector2Int(-1,-1),
                new Vector2Int( 0,-1),
                new Vector2Int( 1,-1)
            };
            
            Vector2Int position =  grid.Position;
            Vector2Int size = grid.Size;

            List<BodyGrid> grids =  new  List<BodyGrid>();

            for (int x = position.x; x < size.x + position.x; ++x)
            {
                for (int y = position.y; y < size.y + position.y; ++y)
                {
                    float value = grid[x, y];
                    if (value < 0)
                    {
                        continue; 
                    }
                    
                    //Start traversing positive values
                    BodyGrid newGrid = new BodyGrid();
                    Stack<Vector2Int> checkList = new Stack<Vector2Int>();
                    checkList.Push(new Vector2Int(x, y));

                    while (checkList.Count > 0)
                    {
                        //Move value to copy
                        Vector2Int current = checkList.Pop();

                        //Node already visited
                        if (grid[current.x, current.y] < 0)
                        {
                            continue;
                        }
                        
                        newGrid[current.x,current.y] = grid[current.x,current.y];
                        grid[current.x, current.y] = -1.0f;

                        //Check neighbors
                        for (int i = 0; i < 8; ++i)
                        {
                            Vector2Int neighbor = current + neighbors[i];
                            
                            if (grid[neighbor.x, neighbor.y] > 0)
                            {
                                checkList.Push(neighbor);
                            }
                        }
                    }
                    
                    //Add the new grid to the grids
                    grids.Add(newGrid);
                }
            }
            //If grids[] is empty, there is no solid ground, just delete the object
            if (grids.Count == 0)
            {
                Destroy(gameObject);
                return;
            }
            
            //Set the current grid to 
            grid = grids[0];

            for (int i = 1; i < grids.Count; ++i)
            {
                BodyGrid grid = grids[i];
                
                //Create new object, set it to contain grid
                GameObject obj = Instantiate(gameObject);
                obj.GetComponent<MSGenerator>().grid = grid;
            }
        }

        public void Generate()
        {
            //Start in the top left
            Vector2Int position = grid.Position;
            Vector2Int size = grid.Size;

            for (int y = position.y; y < position.y + size.y - 1; y++)
            {
                for (int x = position.x; x < position.x + size.x - 1; x++)
                {
                    float[] cornerValues =
                    {
                        grid[x,y],
                        grid[x + 1,y],
                        grid[x + 1,y + 1],
                        grid[x,y + 1],
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

                    //Add triangle
                    for (int i = 0; MSHelperFunctions.triangleIndexTable[index, i] !=8; i += 3)
                    {
                        Triangle triangle = new Triangle();
                        
                        triangle.A = vertices[MSHelperFunctions.triangleIndexTable[index, i + 0]];
                        triangle.B = vertices[MSHelperFunctions.triangleIndexTable[index, i + 1]];
                        triangle.C = vertices[MSHelperFunctions.triangleIndexTable[index, i + 2]];

                        foreach (ATriangleBuilder builder in _triangleBuilders)
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

                        foreach (AEdgeBuilder builder in _edgeBuilders)
                        {
                            builder.AddEdge(edge,new Vector2Int(x,y));
                        }
                    }
                }
            }

            foreach (ATriangleBuilder builder in _triangleBuilders)
            {
                builder.SetSolid(false);
                builder.Build();
            }

            foreach (AEdgeBuilder builder in _edgeBuilders)
            {
                builder.SetSolid(false);
                builder.Build();
            }
        }
        
        public void Clear()
        {
            foreach (ATriangleBuilder builder in _triangleBuilders)
            {
                builder.Clear();
            }

            foreach (AEdgeBuilder builder in _edgeBuilders)
            {
                builder.Clear();
            }
        }
    }
}