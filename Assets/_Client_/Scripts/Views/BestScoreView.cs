using System;
using _Client_.Scripts.Models;
using SFramework.Core.Runtime;
using TMPro;
using UnityEngine;

namespace _Client_.Scripts.Views
{
    public class BestScoreView : SFView
    {
        [SFInject]
        private readonly SessionModel _sessionModel;
        
        [SerializeField]
        private TextMeshProUGUI _bestScoreText;
        
        protected override void Init()
        {
            _sessionModel.OnBestScoreUpdated += OnBestScoreUpdated;
            _bestScoreText.SetText($"BEST SCORE: {_sessionModel.BestScore}");
        }

        private void OnBestScoreUpdated(int value)
        {
            _bestScoreText.SetText($"BEST SCORE: {value.ToString()}");
        }

        private void OnDestroy()
        {
            _sessionModel.OnBestScoreUpdated -= OnBestScoreUpdated;
        }
    }
}