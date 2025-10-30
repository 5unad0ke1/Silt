#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine.UIElements;

[CustomEditor(typeof(Tests))]
public sealed class TestsEditor : Editor
{
    public override VisualElement CreateInspectorGUI()
    {
        var root = new VisualElement();
        var obj = (Tests)target;



        root.Add(ConstructButton("Save", obj.Save));
        root.Add(ConstructButton("Load", obj.Load));
        root.Add(ConstructButton("Increase", obj.Incre));

        return root;
    }
    private Button ConstructButton(string name,Action action)
    {
        var outputButton = new Button(action);
        outputButton.text = name;
        outputButton.style.width = 70;
        return outputButton;
    }
}
#endif