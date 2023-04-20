using System;
using System.Collections.Generic;
using Helpers.ExtensionsAndDS;
using UnityEngine;

namespace GameplayLogic.PlacedRectangles
{
    [Serializable]
    public class RectanglesColors
    {
        [SerializeField] private List<Color> availableColors;
        private List<Color> currentSequence;
        private int counter;

        public void Init()
        {
            currentSequence = new List<Color>(availableColors.Count);
            foreach (var color in availableColors)
            {
                currentSequence.Add(color);
            }
            currentSequence.Shuffle();
        }

        public Color Next()
        {
            int count = currentSequence.Count;
            if (count == 0) return Color.white;
            var color = currentSequence[counter % count];
            counter++;
            return color;
        }
    }
}
