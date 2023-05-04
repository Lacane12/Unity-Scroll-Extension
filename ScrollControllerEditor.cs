using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ScrollController))]

public class ScrollControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ScrollController scrollController = (ScrollController)target;   

        if (GUILayout.Button("Update"))
        {
            scrollController.UpdateScroll();
        }

        DrawDefaultInspector();
    }
}
