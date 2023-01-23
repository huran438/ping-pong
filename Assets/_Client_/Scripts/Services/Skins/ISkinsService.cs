using System;
using SFramework.Core.Runtime;

namespace _Client_.Scripts.Services.Skins
{
    public interface ISkinsService : ISFService
    {
        event Action<string> OnSetActiveSkin;

        SkinData[] Skins { get; }
        SkinData GetSkinData(string name);
        SkinModel GetSkinModel(string name);
        string ActiveSkin { get; }
        void SetActiveSkin(string name);
    }
}