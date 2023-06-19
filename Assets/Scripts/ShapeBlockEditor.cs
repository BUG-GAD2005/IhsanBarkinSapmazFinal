using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ShapeBlockEditor : Editor
{
    [CustomEditor(typeof(ShapeBlock))]
    public class BlockEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Update Preview"))
            {
                ShapeBlock shapeGenerator = (ShapeBlock)target;

                shapeGenerator.UpdateBlockPiecesPreview();
            }
        }
    }
}
