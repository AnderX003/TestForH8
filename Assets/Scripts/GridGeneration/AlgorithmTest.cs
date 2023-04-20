using UnityEngine;

namespace GridGeneration
{
    public class AlgorithmTest : MonoBehaviour
    {
        [SerializeField] private RectangleGenerationLimits generationLimits;

        private void Start()
        {
            for (int i = 0; i < 20; i++)
            {
                var field = new Grid(
                    new Vector2Int(Random.Range(4, 9), Random.Range(4, 9)));
                field.DivideToRectangles(generationLimits);
                Debug.Log(field.ToString());
            }
        }
    }
}
