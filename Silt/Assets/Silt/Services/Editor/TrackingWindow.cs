using Silt.Services.Debug;
using UnityEditor;
using UnityEngine;

namespace Silt.Services.Editor
{
    public sealed class TrackingWindow : EditorWindow
    {

        [MenuItem("Window/ManualLocator Tracker")]
        public static void OpenWindow()
        {
            GetWindow<TrackingWindow>("ManualLocator Tracker");
        }
        private void OnGUI()
        {
            var trackings = ManualLocator.KeyValuePairs;

            if (trackings.Count == 0)
            {
                GUILayout.Label("No tracking info available.");
                return;
            }
            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);

            GUILayout.Label("Registered", EditorStyles.boldLabel);
            foreach (var item in trackings.Keys)
            {
                TrackingManager.KeyValuePairs.TryGetValue(item, out int injectedCount);

                GUILayout.BeginHorizontal();
                GUILayout.Label($"Name: {item.FullName}");
                GUILayout.Label($"Injected Count: {injectedCount}");
                GUILayout.EndHorizontal();
            }
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
