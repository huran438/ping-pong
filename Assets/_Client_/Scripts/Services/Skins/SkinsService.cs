using System;
using System.Collections.Generic;
using System.Linq;
using SFramework.Core.Runtime;
using UnityEngine;

namespace _Client_.Scripts.Services.Skins
{
    public class SkinsService : ISkinsService
    {
        [SFInject]
        private readonly SkinsDatabase _skinsDatabase;

        private SkinData[] _skinsData;
        private List<SkinModel> _skinModels = new();
        private string _activeSkin;
        private const string _skinModelsPrefsKey = "SKIN_MODELS";
        private const string _activeSkinPrefsKey = "ACTIVE_SKIN";

        [SFInject]
        private void Init()
        {
            _skinsData = new SkinData[_skinsDatabase.Nodes.Length];
            _skinModels = JsonUtility.FromJson<List<SkinModel>>(PlayerPrefs.GetString(_skinModelsPrefsKey,
                JsonUtility.ToJson(new List<SkinModel>())));

            for (var i = 0; i < _skinsData.Length; i++)
            {
                var skinData = (SkinData)_skinsDatabase.Nodes[i];
                _skinsData[i] = skinData;

                if (_skinModels.Exists(m => m.Name == skinData.Name)) continue;
                var skinModel = new SkinModel(skinData.Name);

                skinModel.Unlocked = true; // For Debug

                _skinModels.Add(skinModel);
            }

            if (_skinsData.Length > 0)
            {
                ActiveSkin = PlayerPrefs.GetString(_activeSkinPrefsKey, _skinsData[0].Name);

                if (!_skinModels.Exists(sm => sm.Name == _activeSkin))
                {
                    ActiveSkin = _skinsData[0].Name;
                }
            }
        }

        public event Action<string> OnSetActiveSkin = _ => { };
        public SkinData[] Skins => _skinsData;

        public SkinData GetSkinData(int index)
        {
            index = Mathf.Clamp(index, 0, _skinsData.Length - 1);
            return _skinsData[index];
        }

        public SkinData GetSkinData(string name)
        {
            for (int i = 0; i < _skinsData.Length; i++)
            {
                if (_skinsData[i].Name == name)
                {
                    return _skinsData[i];
                }
            }
            
            return new SkinData();
        }

        public SkinModel GetSkinModel(string name)
        {
            if (!_skinModels.Exists(sm => sm.Name == _activeSkin)) return new SkinModel("");
            return _skinModels.First(sm => sm.Name == name);
        }

        public string ActiveSkin
        {
            get => _activeSkin;

            private set
            {
                if (_skinModels.Exists(sm => sm.Name == value && sm.Unlocked))
                {
                    _activeSkin = value;
                    PlayerPrefs.SetString(_activeSkinPrefsKey, _activeSkin);
                    OnSetActiveSkin.Invoke(_activeSkin);
                }
            }
        }

        public void SetActiveSkin(string name)
        {
            ActiveSkin = name;
        }
    }
}