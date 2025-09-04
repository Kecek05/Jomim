using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace KeceK.General
{
    public class Propose : MonoBehaviour
    {
        [SerializeField] [Required] private AudioSource _audioSource;
        [SerializeField] [Required] private GameObject _proposeCanvas;
        
        private bool _isEnabled = false;
        private bool _canTrigger = true;
        
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            _proposeCanvas.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.Y) && _canTrigger)
            {
                _canTrigger = false;
                _isEnabled = !_isEnabled;
                TogglePropose(_isEnabled);
                StartCoroutine(TriggerCooldown());
            }
        }
        
        private IEnumerator TriggerCooldown()
        {
            yield return new WaitForSeconds(2f);
            _canTrigger = true;
        }

        private void TogglePropose(bool enable)
        {
            if (enable)
            {
                _proposeCanvas.gameObject.SetActive(true);
                
                if (_audioSource.isPlaying)
                    _audioSource.Stop();
                
                _audioSource.Play();
            }
            else
            {
                _proposeCanvas.gameObject.SetActive(false);
                _audioSource.Stop();
            }
        }
    }
}
