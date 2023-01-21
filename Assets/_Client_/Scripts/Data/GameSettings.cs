using UnityEngine;

namespace Client.Data
{
    [CreateAssetMenu(fileName = "dta_settings_game", menuName = "Client/Data/Game Settings", order = 0)]
    public class GameSettings : ScriptableObject
    {
        public float InitialPaddleYOffset => _initialPaddleYOffset;

        public GameObject PaddlePrefab => _paddlePrefab;

        public GameObject AIPaddlePrefab => _aiPaddlePrefab;

        [SerializeField]
        private float _initialPaddleYOffset = 8f;

        [SerializeField]
        private GameObject _paddlePrefab, _aiPaddlePrefab;
    }
}