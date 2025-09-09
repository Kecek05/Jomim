using UnityEngine;
using UnityEditor;
using System.IO;
using System;

namespace KeceK.EditorWindows
{
    public class CameraScreenshotTool : EditorWindow
    {
        private Camera targetCamera;
        private string saveFolder = "Screenshots";
        private int resolutionWidth = 1920;
        private int resolutionHeight = 1080;
        private bool useCustomResolution = false;
        private bool includeTimestamp = true;
        private string filenamePrefix = "Screenshot";
        
        [MenuItem("KeceK/Camera Screenshot Tool")]
        public static void ShowWindow()
        {
            GetWindow<CameraScreenshotTool>("Camera Screenshot Tool");
        }
        
        private void OnGUI()
        {
            GUILayout.Label("Camera Screenshot Settings", EditorStyles.boldLabel);
            
            targetCamera = EditorGUILayout.ObjectField("Target Camera", targetCamera, typeof(Camera), true) as Camera;
            
            EditorGUILayout.Space();
            
            EditorGUILayout.LabelField("Save Location", EditorStyles.boldLabel);
            EditorGUILayout.BeginHorizontal();
            saveFolder = EditorGUILayout.TextField("Save Folder", saveFolder);
            if (GUILayout.Button("Browse...", GUILayout.Width(80)))
            {
                string path = EditorUtility.OpenFolderPanel("Select Screenshot Folder", "", "");
                if (!string.IsNullOrEmpty(path))
                {
                    // Convert to relative path if possible
                    if (path.StartsWith(Application.dataPath))
                    {
                        path = "Assets" + path.Substring(Application.dataPath.Length);
                    }
                    saveFolder = path;
                }
            }
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.Space();
            
            EditorGUILayout.LabelField("File Settings", EditorStyles.boldLabel);
            filenamePrefix = EditorGUILayout.TextField("Filename Prefix", filenamePrefix);
            includeTimestamp = EditorGUILayout.Toggle("Include Timestamp", includeTimestamp);
            
            EditorGUILayout.Space();
            
            EditorGUILayout.LabelField("Resolution Settings", EditorStyles.boldLabel);
            useCustomResolution = EditorGUILayout.Toggle("Use Custom Resolution", useCustomResolution);
            
            GUI.enabled = useCustomResolution;
            resolutionWidth = EditorGUILayout.IntField("Width", resolutionWidth);
            resolutionHeight = EditorGUILayout.IntField("Height", resolutionHeight);
            GUI.enabled = true;
            
            EditorGUILayout.Space();
            
            if (GUILayout.Button("Take Screenshot"))
            {
                TakeScreenshot();
            }
        }
        
        private void TakeScreenshot()
        {
            if (targetCamera == null)
            {
                EditorUtility.DisplayDialog("Error", "Please assign a camera first!", "OK");
                return;
            }
            
            // Ensure directory exists
            string directoryPath = saveFolder;
            if (!saveFolder.Contains(":") && !saveFolder.StartsWith("/"))
            {
                // Relative path, make it absolute
                directoryPath = Path.Combine(Application.dataPath, saveFolder);
            }
            
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            
            // Generate filename
            string timestamp = includeTimestamp ? $"_{DateTime.Now:yyyyMMdd_HHmmss}" : "";
            string filename = $"{filenamePrefix}{timestamp}.png";
            string filePath = Path.Combine(directoryPath, filename);
            
            // Create render texture
            RenderTexture renderTexture = null;
            RenderTexture prevTargetTexture = targetCamera.targetTexture;
            
            try
            {
                int width = useCustomResolution ? resolutionWidth : (int)Handles.GetMainGameViewSize().x;
                int height = useCustomResolution ? resolutionHeight : (int)Handles.GetMainGameViewSize().y;
                
                renderTexture = new RenderTexture(width, height, 24);
                targetCamera.targetTexture = renderTexture;
                
                // Render to the texture
                targetCamera.Render();
                
                // Read pixels from the render texture
                RenderTexture.active = renderTexture;
                Texture2D screenshot = new Texture2D(width, height, TextureFormat.RGB24, false);
                screenshot.ReadPixels(new Rect(0, 0, width, height), 0, 0);
                screenshot.Apply();
                RenderTexture.active = null;
                
                // Save to disk
                byte[] bytes = screenshot.EncodeToPNG();
                File.WriteAllBytes(filePath, bytes);
                
                AssetDatabase.Refresh();
                
                Debug.Log($"Screenshot saved to: {filePath}");
                EditorUtility.RevealInFinder(filePath);
            }
            catch (Exception e)
            {
                Debug.LogError($"Error taking screenshot: {e.Message}");
                EditorUtility.DisplayDialog("Error", $"Failed to take screenshot: {e.Message}", "OK");
            }
            finally
            {
                // Clean up
                targetCamera.targetTexture = prevTargetTexture;
                if (renderTexture != null)
                {
                    renderTexture.Release();
                    DestroyImmediate(renderTexture);
                }
            }
        }
    }
}
