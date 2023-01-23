using SFramework.Core.Runtime;
using UnityEngine;

namespace _Client_.Scripts.Services.Skins
{
    [CreateAssetMenu(fileName = "dtb_skins", menuName = "Client/Database/Skins")]
    public class SkinsDatabase : SFDatabase
    {
        public override string Title => "Skins";
        public override ISFDatabaseNode[] Nodes => _skinsData;

        [SerializeField]
        private SkinData[] _skinsData;
    }
}