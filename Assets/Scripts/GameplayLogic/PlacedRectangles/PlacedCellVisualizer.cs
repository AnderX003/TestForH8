using System;
using System.Collections;
using Helpers.Springs;
using UnityEngine;

namespace GameplayLogic.PlacedRectangles
{
    [Serializable]
    public class PlacedCellVisualizer
    {
        [SerializeField] private float scaleHideDamping;
        [SerializeField] private float scaleHideFrequency;
        [SerializeField] private float scalePunchVelocity;
        [SerializeField] private SpringFloatHandler scaleSpring;
        [SerializeField] private Transform scaleObject;

        private float scaleDefaultDamping;
        private float scaleDefaultFrequency;

        public void Init()
        {
            scaleDefaultDamping = scaleSpring.Damping;
            scaleDefaultFrequency = scaleSpring.Frequency;
        }

        public void Update()
        {
            scaleSpring.Value = scaleObject.localScale.x;
            scaleSpring.Update();
            scaleObject.localScale = scaleSpring.Value * Vector3.one;
        }

        public void ResetOnEnable()
        {
            scaleObject.localScale = Vector3.zero;
            scaleSpring.GoalValue = 0f;
            scaleSpring.Velocity = 0f;
            scaleSpring.Damping = scaleDefaultDamping;
            scaleSpring.Frequency = scaleDefaultFrequency;
        }

        public void Show()
        {
            scaleSpring.GoalValue = 1f;
        }

        public void PunchScale()
        {
            scaleSpring.Velocity = scalePunchVelocity;
        }

        public IEnumerator Hide()
        {
            scaleSpring.GoalValue = 0f;
            scaleSpring.Damping = scaleHideDamping;
            scaleSpring.Frequency = scaleHideFrequency;

            do
            {
                yield return null;
            } while (scaleSpring.Velocity != 0f);
        }
    }
}
