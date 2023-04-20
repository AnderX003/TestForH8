using System;
using Helpers.Springs;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameplayLogic.PreviewRectangle
{
    [Serializable]
    public class RectanglePreviewSprings
    {
        [SerializeField] private float hideScaleDamping;
        [SerializeField] private float hideScaleFrequency;
        [SerializeField] private SpringVector2Handler scaleSpring;
        [SerializeField] private SpringVector3Handler positionSpring;
        [SerializeField] private Transform positionObject;
        [SerializeField] private SpriteRenderer scaleSprite;

        private float defaultScaleDamping;
        private float defaultScaleFrequency;

        public void Init()
        {
            defaultScaleDamping = scaleSpring.Damping;
            defaultScaleFrequency = scaleSpring.Frequency;

            positionSpring.Velocity = Vector3.zero;
            positionObject.localPosition = Vector3.zero;
            scaleSpring.Velocity = Vector2.zero;
            scaleSprite.size = Vector2.zero;
        }

        public void Update()
        {
            positionSpring.Value = positionObject.localPosition;
            positionSpring.Update();
            positionObject.localPosition = positionSpring.Value;

            scaleSpring.Value = scaleSprite.size;
            scaleSpring.Update();
            scaleSprite.size = scaleSpring.Value;
        }

        public void ResetOnShow()
        {
            positionSpring.Velocity = Vector3.zero;
            scaleSpring.Velocity = Vector2.zero;
            scaleSpring.Damping = defaultScaleDamping;
            scaleSpring.Frequency = defaultScaleFrequency;
            scaleSprite.size = Vector2.zero;
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
            positionObject.localPosition = positionSpring.GoalValue;
        }
    }
}
