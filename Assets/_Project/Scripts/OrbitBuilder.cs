using UnityEngine;

namespace _Project.Scripts
{
    public class OrbitBuilder
    {
        private float Radius { get; set; } = 1;
        private double Steps { get; set; } = 50;
        private float LineWidth { get; set; } = 0.35f;
        private Color Color { get; set; } = Color.white;

        public OrbitBuilder WithRadius(double radius)
        {
            Radius = (float)radius;
            return this;
        }

        public OrbitBuilder WithLineWidth(float width)
        {
            LineWidth = width;
            return this;
        }

        public OrbitBuilder WithColor(Color color)
        {
            Color = color;
            return this;
        }

        public OrbitBuilder WithRandomRadius(float min, float max)
        {
            Radius = Random.Range(min, max);
            return this;
        }

        public OrbitBuilder WithStepsCount(double steps)
        {
            Steps = steps;
            return this;
        }

        public GameObject Build()
        {
            const int steps = 50;
            var orbit = Object.Instantiate(GameController.Instance.LineRendererPrefab);
            DrawCircle(steps, Radius, orbit);

            orbit.startColor = Color;
            orbit.endColor = Color;

            orbit.startWidth = LineWidth;
            orbit.endWidth = LineWidth;

            return orbit.gameObject;
        }

        private void DrawCircle(int steps, float radius, LineRenderer lineRenderer)
        {
            lineRenderer.positionCount = steps + 1;

            var angle = 20f;
            for (int i = 0; (i < steps + 1); i++)
            {
                var x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
                var y = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;

                lineRenderer.SetPosition(i, new Vector3(x, 0, y));

                angle += (360f / steps);
            }
        }
    }
}