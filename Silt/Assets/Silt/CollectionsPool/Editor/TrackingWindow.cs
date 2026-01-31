using Silt.CollectionsPool.Debug;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Silt.CollectionsPool.Editor
{
    public class TrackingWindow : EditorWindow
    {

        [MenuItem("Window/CollectionsPool Tracker")]
        public static void OpenWindow()
        {
            GetWindow<TrackingWindow>("CollectionsPool Tracker");
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
            if (EditorApplication.timeSinceStartup - _timer >= 0.1)
            {
                _timer = EditorApplication.timeSinceStartup;
                Repaint();
            }
        }

        private double _timer = 0f;
        private Vector2 _scrollPos;
    }
}