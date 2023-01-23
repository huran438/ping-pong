using System;
using _Client_.Scripts.Enums;
using _Client_.Scripts.Models;
using SFramework.Core.Runtime;
using TMPro;
using UnityEngine;

namespace _Client_.Scripts.Views
{
    public class ScoreView : SFView
    {
        [SFInject]
        private readonly SessionModel _sessionModel;

        [SerializeField]
        private PaddleType _type;

        [SerializeField]
        private TextMeshProUGUI _scoreText;

        protected override void Init()
        {
            _sessionModel.OnPaddleScoreUpdated += OnPaddleScoreUpdated;
        }

        private void OnPaddleScoreUpdated(PaddleType type, int score)
        {
            if (_type == type)
            {
                _scoreText.SetText(score.ToString());
            }
        }

        protected void OnDestroy()
        {
            _sessionModel.OnPaddleScoreUpdated -= OnPaddleScoreUpdated;
        }
    }
}