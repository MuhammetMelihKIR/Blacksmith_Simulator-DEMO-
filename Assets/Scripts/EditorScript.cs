using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DealerOrderButton))]
public class EditorScript : Editor
{
    // public override void OnInspectorGUI()
    // {
    //     DealerOrderButton dealerOrderButton = (DealerOrderButton)target;
    //     if (GUILayout.Button("Increase"))
    //     {
    //         dealerOrderButton.PieceIncrease();
    //     }
    //
    //     if (GUILayout.Button("Decrease"))
    //     {
    //         dealerOrderButton.PieceDecrease();
    //     }
    // }
}