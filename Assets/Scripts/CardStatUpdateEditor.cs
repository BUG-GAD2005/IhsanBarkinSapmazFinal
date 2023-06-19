using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[CustomEditor(typeof(Card))]
public class CardStatUpdateEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Update Card Information"))
        {
            Card cardUpdater = (Card)target;

            cardUpdater.UpdateStatsAboutCardOnUI();
        }
    }
}
