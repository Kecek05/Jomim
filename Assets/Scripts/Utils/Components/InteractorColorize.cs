using System;
using System.Collections.Generic;
using KeceK.General;
using Sirenix.OdinInspector;
using UnityEngine;

namespace KeceK.Utils.Components
{
    public class InteractorColorize : MonoBehaviour
    {
        [Serializable]
        private class InteractorColorizeData
        {
            [Title("References")] 
            [Required]
            public SpriteRenderer InteractorIcon;
        
            [Title("Settings")]
            public InteractorColor InteractorColor = InteractorColor.White;
        }
        
        [Title("Settings")]
        [SerializeField] private List<InteractorColorizeData> _interactorColorizeData;

        private void Start()
        {
            _interactorColorizeData.ForEach(
                data => data.InteractorIcon.color = GeneralInfo.GetColorByInteractorColor(data.InteractorColor)
                );
        }
    }
}
