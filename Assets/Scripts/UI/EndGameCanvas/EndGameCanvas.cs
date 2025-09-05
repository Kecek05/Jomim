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
        [SerializeField] [Required]
        private Button _videoRealButton;

        private string _videoFakeURL = "https://www.youtube.com/watch?v=dQw4w9WgXcQ";
        
        private string _videoRealURL = "https://youtu.be/3UoCLMiCF54?si=TuKG4m80QqCJ1FQ4";

        private void Awake()
        {
            _mainMenuButton.gameObject.SetActive(false);
            _videoRealButton.gameObject.SetActive(false);

            _mainMenuButton.onClick.AddListener(() => { Loader.Load(Loader.Scene.MainMenu); });

            _videoFakeButton.onClick.AddListener(() =>
            {
                Application.OpenURL(_videoFakeURL);
                _videoRealButton.gameObject.SetActive(true);
            });

            _videoRealButton.onClick.AddListener(() =>
            {
                Application.OpenURL(_videoRealURL);
                _mainMenuButton.gameObject.SetActive(true);
            });
    }
    }
}
