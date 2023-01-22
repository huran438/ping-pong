using SFramework.Core.Runtime;
using UnityEngine;

namespace _Client_.Scripts.Views
{
    public class ScalePlaneView : SFView
    {
        protected override void Init()
        {
            var verticalSize   = Camera.main.orthographicSize * 2f;
            var horizontalSize = verticalSize * Screen.width / Screen.height;
            transform.localScale = new Vector3(horizontalSize, 1f, verticalSize) / 10f;
        }
    }
}