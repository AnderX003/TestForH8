using System;
using Helpers.Springs;
using UnityEngine;

namespace GameplayObjects.PreviewRectangle
{
    [Serializable]
    public class RectanglePreviewSprings
    {
        [SerializeField] private float hideScaleDamping;
        [SerializeField] private float hideScaleFrequency;
        [SerializeField] private SpringVector2Handler scaleSpring;
        [SerializeField] private SpringVector3Handler positionSpring;
        [SerializeField] private Transform PositionObject;
        [SerializeField] private SpriteRenderer ScaleSprite;

        private float defaultScaleDamping;
        private float defaultScaleFrequency;

        public void Init()
        {
            defaultScaleDamping = scaleSpring.Damping;
            defaultScaleFrequency = scaleSpring.Frequency;
        }

        public void Update()
        {
            positionSpring.Value = PositionObject.localPosition;
            positionSpring.Update();
            PositionObject.localPosition = positionSpring.Value;

            scaleSpring.Value = ScaleSprite.size;
            scaleSpring.Update();
            ScaleSprite.size = scaleSpring.Value;
        }

        public void ResetOnShow()
        {
            positionSpring.Velocity = Vector3.zero;
            scaleSpring.Velocity = Vector2.zero;
            scaleSpring.Damping = defaultScaleDamping;
            scaleSpring.Frequency = defaultScaleFrequency;
            ScaleSprite.size = Vector2.zero;
        }

        public void Hide()
        {
            scaleSpring.Frequency = hideScaleFrequency;
            scaleSpring.Damping = hideScaleDamping;
            scaleSpring.GoalValue = Vector2.zero;
        }

        public void SetTargetPosition(Vector3 pos)
        {
            positionSpring.GoalValue = pos;
        }

        public void SetTargetScale(Vector2 scale)
        {
            scaleSpring.GoalValue = scale;
        }

        public void SetStartPosition()
        {
            PositionObject.localPosition = positionSpring.GoalValue;
        }
    }
}
