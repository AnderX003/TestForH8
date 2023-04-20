﻿using System;
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
        [SerializeField] private float scaleHideVelocity;
        [SerializeField] private float scalePunchVelocity;
        [SerializeField] private SpringFloatHandler scaleSpring;
        [SerializeField] private Transform scaleObject;
        [SerializeField] private MeshRenderer renderer;

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

        public void Show(Color color)
        {
            scaleSpring.GoalValue = 1f;
            renderer.material.color = color;
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
            scaleSpring.Velocity += scaleHideVelocity;

            const float epsilon = 0.01f;
            do
            {
                yield return null;
            } while (Mathf.Abs(scaleSpring.Velocity) > epsilon);
        }
    }
}
