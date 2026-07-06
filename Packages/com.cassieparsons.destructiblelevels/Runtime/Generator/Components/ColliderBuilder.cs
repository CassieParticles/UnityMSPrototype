using System.Collections.Generic;
using Core;
using Generator;
using UnityEngine;

namespace MarchingSquaresTool.PrototypeD.Generator.Components
{
    [RequireComponent(typeof(PolygonCollider2D))]
    public class ColliderBuilder:  AEdgeBuilder
    {
        private List<LinkedList<Vector2>> _loops;
        
        private PolygonCollider2D _collider2D;

        private void Awake()
        {
            _loops = new List<LinkedList<Vector2>>();
            
            _collider2D = GetComponent<PolygonCollider2D>();
        }

        protected new void OnValidate()
        {
            base.OnValidate();
            
            _loops = new List<LinkedList<Vector2>>();
            
            _collider2D = GetComponent<PolygonCollider2D>();
        }

        public override void AddEdge(Edge edge, Vector2Int cellPosition, bool solid)
        {
            LinkedList<Vector2> attachedToFront = null;
            LinkedList<Vector2> attachedToEnd = null;
            
            //Check if edge belongs to any existing loops
            foreach (LinkedList<Vector2> loop in _loops)
            {
                //Check if the edge goes on the front of the loop
                if (loop.First.Value == edge.B)
                {
                    loop.AddBefore(loop.First, edge.A);
                    attachedToFront = loop;
                }
                //Check if the edge goes on the end of the loop
                else if (loop.Last.Value == edge.A)
                {
                    loop.AddAfter(loop.Last, edge.B);
                    attachedToEnd = loop;
                }
            }

            //Wasn't attached to either, add new list
            if (attachedToFront == null && attachedToEnd == null)
            {
                LinkedList<Vector2> newLoop = new LinkedList<Vector2>();
                newLoop.AddLast(edge.A);
                newLoop.AddLast(edge.B);
                _loops.Add(newLoop);
            }
            
            //Attached to 2 loops, remove the vertex from one loop, then combine the 2 lists
            else if (attachedToFront != null && attachedToEnd != null)
            {
                //Remove full edge from front list (so it's not duplicated)
                attachedToFront.RemoveFirst();
                attachedToFront.RemoveFirst();

                //Move all vertices to the end list (order: e0,e1....en-2, f0,f1....fm)
                foreach (Vector2 vertex in attachedToFront)
                {
                    attachedToEnd.AddLast(vertex);
                }

                _loops.Remove(attachedToFront);
            }
        }
        
        

        public override void Build()
        {
            _collider2D.pathCount = _loops.Count;
            int i = 0;
            
            //Create the loops
            foreach (LinkedList<Vector2> loop in _loops)
            {
                //Ensure loop is properly constructed
                if (loop.First.Value != loop.Last.Value)
                {
                    Debug.LogError("ERROR: COLLIDER NOT SUCCESSFULLY CREATED, VERTICES DO NOT MAKE FULL LOOP");
                    return;
                }
                
                //Put vertices into an array, to be used by the collider2D
                List<Vector2> loopArray = new List<Vector2>(loop.Count);
                foreach (Vector2 vertex in loop)
                {
                    loopArray.Add(vertex);
                }
                
                _collider2D.SetPath(i++, loopArray);
            }
        }
        public override void Clear()
        {
            _loops.Clear();
            _collider2D.pathCount = 0;
        }

        public override bool IsEditor()
        {
            return false;
        }
        public override bool IsGame()
        {
            return true;
        }
    }
}