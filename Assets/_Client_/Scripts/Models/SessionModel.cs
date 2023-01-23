using System;
using _Client_.Scripts.Enums;
using UnityEngine;

namespace _Client_.Scripts.Models
{
    public class SessionModel
    {
        public event Action<PaddleType, int> OnPaddleScoreUpdated = (_, _) => { };
        public event Action<int> OnBestScoreUpdated = (_) => { };

        private const string _bestScorePrefsKey = "BEST_SCORE";

        public SessionModel()
        {
            _bestScore = PlayerPrefs.GetInt(_bestScorePrefsKey, 0);
        }

        public int BottomPaddleScore
        {
            get => _bottomPaddleScore;

            private set
            {
                _bottomPaddleScore = value;
                OnPaddleScoreUpdated.Invoke(PaddleType.Bottom, value);
            }
        }

        public int TopPaddleScore
        {
            get => _topPaddleScore;
            private set
            {
                _topPaddleScore = value;
                OnPaddleScoreUpdated.Invoke(PaddleType.Top, value);
            }
        }

        public int BestScore
        {
            get => _bestScore;
            private set
            {
                if (value > _bestScore)
                {
                    _bestScore = value;
                    PlayerPrefs.SetInt(_bestScorePrefsKey, value);
                    OnBestScoreUpdated.Invoke(value);
                }
            }
        }

        private int _bottomPaddleScore;
        private int _topPaddleScore;
        private int _bestScore;

        public void IncrementScore(PaddleType paddleType)
        {
            switch (paddleType)
            {
                case PaddleType.Bottom:
                    BottomPaddleScore += 1;
                    BestScore = BottomPaddleScore;
                    break;
                case PaddleType.Top:
                    TopPaddleScore += 1;
                    break;
            }
        }
        
        public void Reset()
        {
            BottomPaddleScore = 0;
            TopPaddleScore = 0;
        }
    }
}