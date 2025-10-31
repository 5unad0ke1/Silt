#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[CustomEditor(typeof(PauseTester))]
public class PauseTesterEditor : Editor
{
    public override VisualElement CreateInspectorGUI()
    {
        var root = new VisualElement();
        InspectorElement.FillDefaultInspector(root, serializedObject, this);
        var obj = (PauseTester)target;



        foreach (var item in Enum.GetValues(typeof(PauseTester.AAA)))
        {
            var subRoot = new VisualElement();
            subRoot.style.flexDirection = FlexDirection.Row;

            var value = (PauseTester.AAA)item;
            var outputButton = new Button(() => obj.Add(value));
            outputButton.text = "í‚é~ " + value.ToString();
            outputButton.style.width = 200;
            subRoot.Add(outputButton);

            outputButton = new Button(() => obj.Remove(value));
            outputButton.text = "âèú " + value.ToString();
            outputButton.style.width = 200;
            subRoot.Add(outputButton);

            root.Add(subRoot);
        }
        return root;
    }
}
#endif