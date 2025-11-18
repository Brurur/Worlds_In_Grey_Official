namespace Oicaimang.WindEffectTree
{
    using System;
#if UNITY_EDITOR
    using UnityEditor;
#endif
    using UnityEngine;

    public class CheckRangeOfVertex : MonoBehaviour
    {
        public void Check()
        {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            if (sr == null || sr.sprite == null)
            {
                Debug.LogError("SpriteRenderer be null");
                return;
            }
            Sprite sprite = sr.sprite;
            Vector3[] vertices = Array.ConvertAll(sprite.vertices, v => (Vector3)v);

            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i] = transform.TransformPoint(vertices[i]);
            }

            Vector3 min = vertices[0];
            Vector3 max = vertices[0];

            foreach (Vector3 v in vertices)
            {
                min = Vector3.Min(min, v);
                max = Vector3.Max(max, v);
            }

            Debug.Log($"Min vertex: {min}");
            Debug.Log($"Max vertex: {max}");
        }
    }
#if UNITY_EDITOR
    [CustomEditor(typeof(CheckRangeOfVertex))]
    [CanEditMultipleObjects]
    public class CheckRangeOfVertexEditor : Editor
    {
        CheckRangeOfVertex checkRangeOfVertex;

        private void Init()
        {
            checkRangeOfVertex = target as CheckRangeOfVertex;
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            Init();
            if (GUILayout.Button("CheckMinMaxVerticle", GUILayout.Height(24)))
            {
                checkRangeOfVertex.Check();
            }

        }
    }
#endif
}