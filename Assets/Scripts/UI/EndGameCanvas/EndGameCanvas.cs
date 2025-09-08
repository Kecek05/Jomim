using KeceK.General;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace KeceK.UI
{
    public class EndGameCanvas : MonoBehaviour
    {
        [Title("References")]
        [SerializeField] [Required]
        private Button _mainMenuButton;
        [SerializeField] [Required] 
        private Button _videoFakeButton;

        private string _videoFakeURL = "https://www.youtube.com/watch?v=dQw4w9WgXcQ";
        
        private void Awake()
        {
            _mainMenuButton.gameObject.SetActive(false);

            _mainMenuButton.onClick.AddListener(() => { Loader.Load(Loader.Scene.MainMenu); });

            _videoFakeButton.onClick.AddListener(() =>
            {
                Application.OpenURL(_videoFakeURL);
                _mainMenuButton.gameObject.SetActive(true);
            });
        }
    }
}
