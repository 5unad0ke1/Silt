using Silt.PauseSystem.Debug;
using System;
using UnityEditor;
using UnityEngine;

namespace Silt.PauseSystem.Editor
{
    public class TrackingWindow : EditorWindow
    {

        [MenuItem("Window/PauseSystem Tracker")]
        public static void OpnenWindow()
        {
            GetWindow<TrackingWindow>("PauseSystem Tracker");
        }
        private void OnGUI()
        {

            GUILayout.Label("PauseSystem Tracking", EditorStyles.boldLabel);

            var trackings = PauseSystemTracking.Infos;

            if (trackings.Count == 0)
            {
                GUILayout.Label("No tracking info available.");
                return;
            }
            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
            foreach (var info in trackings)
            {
                if (info.Key == null)
                    continue;
                var value = info.Value;

                string name = string.IsNullOrWhiteSpace(value.Name) ? "default" : value.Name;

                GUILayout.BeginHorizontal();
                GUILayout.Label($"Name: {name}", GUILayout.Width(200));
                GUILayout.Label($"Type: {value.Type}", GUILayout.Width(200));
                GUILayout.Label($"Flag: {Convert.ToString(value.GetFlag(), 2).PadLeft(8, '0')}", GUILayout.Width(200));
                GUILayout.EndHorizontal();
            }

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