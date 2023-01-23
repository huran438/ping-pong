using System;
using _Client_.Scripts.Services.Skins;
using SFramework.Core.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Client_.Scripts.Views
{
    public class SkinElementView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private TextMeshProUGUI _experienceText;

        [SerializeField]
        private Image _skinImage;

        [SerializeField]
        private Image _skinStatus;

        [SerializeField]
        private Slider _experienceSlider;

        private SkinData _skinData;
        private ISkinsService _skinsService;

        public void Setup(ISkinsService skinsService, SkinData skinData)
        {
            _skinData = skinData;
            _skinsService = skinsService;

            _skinImage.color = _skinData.Color;
            _experienceSlider.value = 0f;
            _experienceText.SetText($"0/{_skinData.RequiredExperience.ToString()}");

            var skinModel = skinsService.GetSkinModel(skinData.Name);

            if (skinModel.Unlocked)
            {
                _experienceSlider.gameObject.SetActive(false);
                _skinStatus.color = skinsService.ActiveSkin == skinData.Name ? Color.green : Color.yellow;
            }
            else
            {
                _skinStatus.color = Color.red;
            }

            _skinsService.OnSetActiveSkin += OnSetActiveSkin;
        }


        private void OnSetActiveSkin(string skinName)
        {
            if (_skinData.Name == skinName)
            {
                _skinStatus.color = Color.green;
            }
            else
            {
                var skinModel = _skinsService.GetSkinModel(_skinData.Name);
                _skinStatus.color = skinModel.Unlocked ? Color.yellow : Color.red;
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _skinsService.SetActiveSkin(_skinData.Name);
        }

        private void OnDestroy()
        {
            _skinsService.OnSetActiveSkin -= OnSetActiveSkin;
        }
    }
}