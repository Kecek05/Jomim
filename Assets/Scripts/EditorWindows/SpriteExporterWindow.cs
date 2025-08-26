#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.IO;

namespace KeceK.EditorWindows
{
    public class SpriteExporterWindow : EditorWindow
    {
        private SpriteRenderer _targetRenderer;
        private int _resolutionMultiplier = 1;
        private string _saveFolder = "";

        [MenuItem("KeceK/Sprite Exporter")]
        public static void ShowWindow()
        {
            GetWindow<SpriteExporterWindow>("Sprite Exporter");
        }

        private void OnGUI()
        {
            GUILayout.Label("Export SpriteRenderer as PNG", EditorStyles.boldLabel);

            _targetRenderer = (SpriteRenderer)EditorGUILayout.ObjectField(
                "Sprite Renderer", _targetRenderer, typeof(SpriteRenderer), true
            );

            _resolutionMultiplier = Mathf.Max(1,
                EditorGUILayout.IntField("Resolution Multiplier", _resolutionMultiplier)
            );

            EditorGUILayout.Space();
            GUILayout.Label("Save Folder", EditorStyles.label);
            using (new EditorGUILayout.HorizontalScope())
            {
                _saveFolder = EditorGUILayout.TextField(_saveFolder);
                if (GUILayout.Button("…", GUILayout.MaxWidth(30)))
                {
                    string initial = string.IsNullOrEmpty(_saveFolder) ? Application.dataPath : _saveFolder;
                    string chosen = EditorUtility.OpenFolderPanel(
                        "Select save folder",
                        initial,
                        ""
                    );
                    if (!string.IsNullOrEmpty(chosen))
                        _saveFolder = chosen;
                }
            }

            EditorGUILayout.Space();
            GUI.enabled = _targetRenderer != null 
                          && _targetRenderer.sprite != null 
                          && !string.IsNullOrEmpty(_saveFolder);

            if (GUILayout.Button("Export"))
                ExportSprite();

            GUI.enabled = true;
        }

        private void ExportSprite()
        {
            Sprite spriteToExport = _targetRenderer.sprite;
            Color colorToMultiply = _targetRenderer.color;
            Texture textureToExport = spriteToExport.texture;
            Rect textureRect = spriteToExport.textureRect;

            Texture2D readable = EnsureReadable(textureToExport);

            Color[] pixels = readable.GetPixels(
                (int)textureRect.x, (int)textureRect.y,
                (int)textureRect.width, (int)textureRect.height
            );

            for (int i = 0; i < pixels.Length; i++)
                pixels[i] *= colorToMultiply;

            int width = Mathf.RoundToInt(textureRect.width) * _resolutionMultiplier;
            int height = Mathf.RoundToInt(textureRect.height) * _resolutionMultiplier;

            Texture2D output = new Texture2D(width, height, TextureFormat.RGBA32, false);
            output.SetPixels(pixels);

            if (_resolutionMultiplier != 1)
                output = ScaleTexture(output, width, height);

            output.Apply();
            
            string fileName = $"{_targetRenderer.gameObject.name}.png";
            string fullPath = Path.Combine(_saveFolder, fileName);

            File.WriteAllBytes(fullPath, output.EncodeToPNG());
            Debug.Log($"Sprite exported to: {fullPath}");
            AssetDatabase.Refresh();
        }

        private Texture2D EnsureReadable(Texture testTexture)
        {
            if (testTexture is Texture2D texture2D && texture2D.isReadable)
                return texture2D;

            RenderTexture temporaryRenderTexture = RenderTexture.GetTemporary(
                testTexture.width, testTexture.height, 0,
                RenderTextureFormat.Default,
                RenderTextureReadWrite.Linear
            );
            Graphics.Blit(testTexture, temporaryRenderTexture);
            RenderTexture preview = RenderTexture.active;
            RenderTexture.active = temporaryRenderTexture;

            Texture2D destinationTexture2D = new Texture2D(testTexture.width, testTexture.height, TextureFormat.RGBA32, false);
            destinationTexture2D.ReadPixels(new Rect(0, 0, temporaryRenderTexture.width, temporaryRenderTexture.height), 0, 0);
            destinationTexture2D.Apply();

            RenderTexture.active = preview;
            RenderTexture.ReleaseTemporary(temporaryRenderTexture);
            return destinationTexture2D;
        }

        private Texture2D ScaleTexture(Texture2D sourceTexture2D, int width, int height)
        {
            Texture2D destinationTexture2D = new Texture2D(width, height, sourceTexture2D.format, false);
            for (int yPositionInTexture = 0; yPositionInTexture < height; yPositionInTexture++)
                for (int xPositionInTexture = 0; xPositionInTexture < width; xPositionInTexture++)
                {
                    float horizontalPositionInUV = xPositionInTexture / (float)width;
                    float verticalPositionInUV = yPositionInTexture / (float)height;
                    destinationTexture2D.SetPixel(xPositionInTexture, yPositionInTexture, sourceTexture2D.GetPixelBilinear(horizontalPositionInUV, verticalPositionInUV));
                }
            destinationTexture2D.Apply();
            return destinationTexture2D;
        }
    }
}

#endif
