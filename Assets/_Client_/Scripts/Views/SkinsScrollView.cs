using _Client_.Scripts.Services.Skins;
using SFramework.Core.Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace _Client_.Scripts.Views
{
    public class SkinsScrollView : SFView
    {
        [SFInject]
        private readonly ISkinsService _skinsService;

        [SerializeField]
        private SkinElementView _skinElementPrefab;

        [SerializeField]
        private ScrollRect _scrollRect;

        protected override void Init()
        {
            for (var i = 0; i < _skinsService.Skins.Length; i++)
            {
                var skinData = _skinsService.Skins[i];
                var skinElementView = Instantiate(_skinElementPrefab, _scrollRect.content);
                skinElementView.Setup(_skinsService, skinData);
            }
        }
    }
}