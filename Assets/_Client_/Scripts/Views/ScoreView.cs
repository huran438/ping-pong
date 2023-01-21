using Client.Enum;
using Client.Models;
using SFramework.Core.Runtime;
using TMPro;
using UnityEngine;

namespace Client.Views
{
    public class ScoreView : SFView
    {
        [SFInject]
        private readonly SessionModel _sessionModel;
        
        [SerializeField]
        private PaddleType _type;

        [SerializeField]
        private TextMeshProUGUI _scoreText;

        private void Update()
        {
            if (_type == PaddleType.Bottom)
            {
                _scoreText.SetText(_sessionModel.PlayerBottomScore.ToString());
            }
            else
            {
                _scoreText.SetText(_sessionModel.PlayerTopScore.ToString());
            }

           
        }
    }
}