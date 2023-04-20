using System;
using DG.Tweening;
using Helpers.ExtensionsAndDS;
using Unity.VisualScripting;
using UnityEngine;

namespace GameplayLogic.PreviewRectangle
{
    [Serializable]
    public class RectanglePreviewColorVisualizer
    {
        [SerializeField] private Color matchColor;
        [SerializeField] private Color mismatchColor;
        [SerializeField] private DoAnimationParams<Ease> changeColorParams;
        [SerializeField] private DoAnimationParams<Ease> hideParams;
        [SerializeField] private SpriteRenderer ScaleSprite;

        public void ResetColor()
        {
            ScaleSprite.color = mismatchColor;
        }

        public void SetMatchColor(bool match)
        {
            var color = match
                ? matchColor
                : mismatchColor;
            ScaleSprite
                .DOColor(color, changeColorParams.Duration)
                .SetEase(changeColorParams.Ease);
        }

        public void Hide()
        {
            var color = ScaleSprite.color.WithAlpha(0f);
            ScaleSprite
                .DOColor(color, hideParams.Duration)
                .SetEase(hideParams.Ease);
        }
    }
}
