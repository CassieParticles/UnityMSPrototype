using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace PrototypeB.MarchingSquaresTool.PrototypeB.Components.Builders
{
    public class EdgeGroup
    {
        LinkedList<Vector2> edges = new LinkedList<Vector2>();
        public LinkedList<Vector2> Edges => edges;
        public Vector2 First => edges.First.Value;  //A
        public Vector2 Last => edges.Last.Value;    //B
        
        public EdgeGroup(Edge edge)
        {
            edges.AddLast(edge.A);
            edges.AddLast(edge.B);
        }


        public void AddToFront(Edge edge)
        {
            if (First != edge.B)
            {
                return;
            }
            edges.AddFirst(edge.A);
        }

        public void AddToEnd(Edge edge)
        {
            if (Last != edge.A)
            {
                return;
            }
            edges.AddLast(edge.B);
        }

        public bool CombineGroups(EdgeGroup other)
        {
            if (other == this)
            {
                return false;
            }

            foreach (var item in other.edges)
            {
                edges.AddLast(item);
            }
            return true;
        }
    }
    
    [RequireComponent(typeof(Rigidbody2D))]
    public class EdgeColliderBuilder : MonoBehaviour, IBuildEdges
    {
        private List<EdgeGroup> edgeGroups = new List<EdgeGroup>();
        public void AddEdge(Edge edge, Vector2Int cellPosition)
        {
            EdgeGroup frontGroup = null;
            EdgeGroup endGroup = null;

            foreach (EdgeGroup group in edgeGroups)
            {
                //Should the edge be added to the front
                if (group.First == edge.B)
                {
                    frontGroup = group;
                }
                //Should the edge be added to the back
                if (group.Last == edge.A)
                {
                    endGroup = group;
                }
            }
            
            //Edge connected to both
            if (frontGroup != null && endGroup != null)
            {
                frontGroup.AddToFront(edge);
                //Combine the groups, if different groups, remove the back one (no longer needed)
                if (frontGroup.CombineGroups(endGroup))
                {
                    edgeGroups.Remove(endGroup);
                }
            }
            else if (frontGroup != null)
            {
                frontGroup.AddToFront(edge);
            }
            else if (endGroup != null)
            {
                endGroup?.AddToEnd(edge);
            }
            else
            {
                edgeGroups.Add(new EdgeGroup(edge));
            }
        }
        public void Build()
        {
            PolygonCollider2D collider = gameObject.AddComponent<PolygonCollider2D>();
            collider.pathCount = edgeGroups.Count;
            for (int i = 0; i < edgeGroups.Count; ++i)
            {
                edgeGroups[i].Edges.RemoveLast();
                
                List<Vector2> edges = new List<Vector2>();
                edges.AddRange(edgeGroups[i].Edges);

                
                collider.SetPath(i, edges);
            }

        }
    }
}