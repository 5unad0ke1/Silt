using Silt.Core.CollectionsPool.Debug;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TrackingWindow : EditorWindow
{

    [MenuItem("Silt/Tracking Window")]
    public static void OpnenWindow()
    {
        GetWindow<TrackingWindow>("CollectionsPool Tracking");
    }
    private void OnGUI()
    {
        _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);

        GUILayout.Label("Dictionary Pool Tracking", EditorStyles.boldLabel);
        OnGUIDictionaryCollections(DictionaryTrackingManager.Collections);
        GUILayout.Space(10);

        GUILayout.Label("HashSet Pool Tracking", EditorStyles.boldLabel);
        OnGUICollections(HashSetTrackingManager.Collections);
        GUILayout.Space(10);

        GUILayout.Label("List Pool Tracking", EditorStyles.boldLabel);
        OnGUICollections(ListTrackingManager.Collections);
        GUILayout.Space(10);

        GUILayout.Label("Queue Pool Tracking", EditorStyles.boldLabel);
        OnGUICollections(QueueTrackingManager.Collections);
        GUILayout.Space(10);

        GUILayout.Label("Stack Pool Tracking", EditorStyles.boldLabel);
        OnGUICollections(StackTrackingManager.Collections);

        EditorGUILayout.EndScrollView();
    }
    private void OnEnable()
    {
        // 毎フレーム呼ばれるイベントに登録
        EditorApplication.update += UpdateWindow;
    }

    private void OnDisable()
    {
        EditorApplication.update -= UpdateWindow;
    }
    private void OnGUICollections(IReadOnlyCollection<TrackingInfo> trackings)
    {
        if (trackings.Count == 0)
        {
            GUILayout.Label("No tracking info available.");
            return;
        }
        foreach (var info in trackings)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label($"Free: {info.GetFreeCount()}", GUILayout.Width(100));
            GUILayout.Label($"Busy: {info.GetBusyCount()}", GUILayout.Width(100));
            GUILayout.Label($"Type: {info.Type.Name}", GUILayout.Width(150));
            GUILayout.EndHorizontal();
        }
    }
    private void OnGUIDictionaryCollections(IReadOnlyCollection<DictionaryTrackingInfo> trackings)
    {
        if (trackings.Count == 0)
        {
            GUILayout.Label("No tracking info available.");
            return;
        }
        foreach (var info in trackings)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label($"Free: {info.GetFreeCount()}", GUILayout.Width(100));
            GUILayout.Label($"Busy: {info.GetBusyCount()}", GUILayout.Width(100));
            GUILayout.Label($"Key Type: {info.KeyType.Name}", GUILayout.Width(150));
            GUILayout.Label($"Value Type: {info.ValueType.Name}", GUILayout.Width(150));
            GUILayout.EndHorizontal();
        }
    }
    private void UpdateWindow()
    {
        _timer += Time.unscaledDeltaTime;
        if (_timer > 0.1f) // 1秒ごとに更新
        {
            _timer = 0f;
            Repaint(); // ウィンドウを再描画
        }
    }

    private float _timer = 0f;
    private Vector2 _scrollPos;
}