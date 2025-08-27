using System;
using KeceK.Game;
using UnityEngine;

namespace KeceK.UI
{
    public class MainMenuManager : MonoBehaviour
    {
        private void Start()
        {
            PauseManager.SetCanChangePauseState(true);
        }
    }
}
