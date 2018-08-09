﻿#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Collections;

[ExecuteInEditMode]
public class PlayFromScene : EditorWindow {
    [SerializeField]
    string lastScene = "";
    [SerializeField]
    int targetScene = 0;
    [SerializeField]
    string waitScene = null;
    [SerializeField]
    bool hasPlayed = false;

    [MenuItem("Edit/Play From Scene %0")]
    public static void Run() {
        EditorWindow.GetWindow<PlayFromScene>();
    }
    static string[] sceneNames;
    static EditorBuildSettingsScene[] scenes;

    void OnEnable() {
        scenes = EditorBuildSettings.scenes;
        sceneNames = scenes.Select(x => AsSpacedCamelCase(Path.GetFileNameWithoutExtension(x.path))).ToArray();
    }

    void Update() {
        if (!EditorApplication.isPlaying) {
            if (null == waitScene && !string.IsNullOrEmpty(lastScene)) {
                EditorApplication.OpenScene(lastScene);
                lastScene = null;
            }
        }
    }

    void OnGUI() {
        if (EditorApplication.isPlaying) {
            if (EditorApplication.currentScene == waitScene) {
                waitScene = null;
            }
            return;
        }

        if (EditorApplication.currentScene == waitScene) {
            EditorApplication.isPlaying = true;
        }
        if (null == sceneNames) return;
        targetScene = EditorGUILayout.Popup(targetScene, sceneNames);
        if (GUILayout.Button("Play")) {
            lastScene = EditorApplication.currentScene;
            waitScene = scenes[targetScene].path;
            EditorApplication.SaveCurrentSceneIfUserWantsTo();
            EditorApplication.OpenScene(waitScene);
        }
    }

    public string AsSpacedCamelCase(string text) {
        return text;
    }
}
#endif