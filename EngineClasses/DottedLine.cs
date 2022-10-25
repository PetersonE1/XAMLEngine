using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;

namespace XAMLEngine
{
    public class DottedLine
    {
        public Vector2D[] points;
        public int dots;
        public double length = 0;
        public bool autofill = false;
        public bool solid = true;
        public SolidColorBrush color;

        private List<Shape> placedShapes;

        public DottedLine(Vector2D[] points, SolidColorBrush color, int dots = 5, bool autofill = false, bool solid = true)
        {
            this.points = points;
            this.dots = dots;
            this.autofill = autofill;
            this.solid = solid;
            this.color = color;
            placedShapes = new List<Shape>();
            UpdateLine();
        }

        public void SetPoint(int point, Vector2D coords)
        {
            try
            {
                points[point] = coords;
                UpdateLine();
            }
            catch (IndexOutOfRangeException)
            {
                Debug.WriteLine($"Point {coords} does not exist");
            }
        }

        public void UpdateLine()
        {
            if (placedShapes.Count > 0)
                Manager.canvas.Children.RemoveRange(placedShapes.ToArray());
            placedShapes.Clear();

            length = 0d;
            Segment[] segments = new Segment[points.Length - 1];
            for (int i = 0; i < points.Length - 1; i++)
            {
                segments[i] = new Segment() { point1 = points[i], point2 = points[i + 1], length = MathD.Distance(points[i], points[i + 1])};
                length += segments[i].length;
            }

            int space = dots;
            if (autofill)
            {
                space = (int)Math.Floor(length / (double)dots);
            }

            for (int s = 0; s < space; s++)
            {
                if (solid)
                    placedShapes.Add(new Ellipse() { Width = 5, Height = 5, Fill = color });
                else
                    placedShapes.Add(new Ellipse() { Width = 5, Height = 5, Stroke = color });
            }

            double p = 0d;
            double factor = 1d / (double)(space - 1);
            for (int i = 0; i < space; i++)
            {
                double placement = p * length;
                for (int l = 0; true; l++)
                {
                    if (p >= 1d)
                    {
                        Vector2D pos = segments[segments.Length - 1].point2;
                        Canvas.SetLeft(placedShapes[i], pos.x);
                        Canvas.SetTop(placedShapes[i], pos.y);
                        break;
                    }

                    if (placement > segments[l].length)
                    {
                        placement -= segments[l].length;
                        continue;
                    }

                    placement = MathD.RLerp(0, segments[l].length, placement);
                    Vector2D spawnPos = MathD.VLerp(segments[l].point1, segments[l].point2, placement);

                    Canvas.SetLeft(placedShapes[i], spawnPos.x);
                    Canvas.SetTop(placedShapes[i], spawnPos.y);

                    break;
                }
                p += factor;
            }
            Manager.canvas.Children.AddRange(placedShapes.ToArray());
        }

        public void Remove()
        {
            Manager.canvas.Children.RemoveRange(placedShapes.ToArray());
        }
    }

    public class Segment
    {
        public double length;
        public Vector2D point1;
        public Vector2D point2;
    }
}
