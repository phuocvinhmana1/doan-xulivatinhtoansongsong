using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;


namespace KMeansProject
{
    public class TamCum
    {
        private double[] _array;
        public double[] Array
        {
            get { return _array; }
        }

        public Color _color;

        // ham ve tam cum
        public void DrawMe(PaintEventArgs e)
        {
            // ve tam cum
            Graphics g = e.Graphics;
            g.FillEllipse(
            new SolidBrush(_color),
            (float)_array[0],(float)_array[1], 15, 15);

            double[] point = new double[2];
            for (int i = 0; i < _closestPointsList.Count; i++)
            {

             
                point = _closestPointsList[i];

                if (point == null)
                {
                    return;
                }         
               
                g.DrawEllipse(new Pen(_color, 1), (float)point[0], (float)point[1], 10, 10);

            }
        }

        private List<double[]> _oldPointsList;

        private List<double[]> _closestPointsList;
        public void addPoint(double[] closestArray)
        {
            _closestPointsList.Add(closestArray);
        }

        private static Random random = new Random();

        public TamCum(double[][] dataSet, Color color)
        {
            // khoi tao ngau nhien tam cum

            _color = color;

            List<Tuple<double, double>> minMaxPoints = Misc.GetMinMaxPoints(dataSet);
           

            _array = new double[minMaxPoints.Count];
            int i = 0;
            foreach (Tuple<double, double> tuple in minMaxPoints)
            {
                double minimum = tuple.Item1;
                double maximum = tuple.Item2;
                double element = random.NextDouble() * (maximum - minimum) + minimum;
                _array[i] = element;
                i++;
            }
           
            _oldPointsList = new List<double[]>();
            _closestPointsList = new List<double[]>();
        }

        // ham tinh khoang cach giua tam cum va cach diem, tra ve vi tri tam cum moi
        public void MoveCentroid()
        {
            // di chuyen tam cum

            List<double> resultVector = new List<double>();

            if (_closestPointsList.Count == 0) return;

            for(int j = 0; j < _closestPointsList[0].GetLength(0); j++)
            {
                double sum = 0.0;
                for(int i = 0; i < _closestPointsList.Count; i++)
                {
                    // cong gia tri x va y cua tung diem
                    sum += _closestPointsList[i][j];
                }
                // lay khoang cach trung binh giua cac diem
                sum /= _closestPointsList.Count;

                // tra ve gia tri tam cum moi
                resultVector.Add(sum);
            }

            _array = resultVector.ToArray();
        }

        public bool HasChanged()
        {
            // kiem tra con su thay doi hay khong
            bool result = true;

            if (_oldPointsList.Count != _closestPointsList.Count) return true;
            if (_oldPointsList.Count == 0 || _closestPointsList.Count == 0) return false;

            for(int i=0; i < _closestPointsList.Count; i++)
            {
                double[] oldPoit = _oldPointsList[i];
                double[] currentPoint = _closestPointsList[i];

                for(int j=0;j<oldPoit.Length;j++)
                    if (oldPoit[j] != currentPoint[j])
                    {
                        result = false;
                        break;
                    }
            }

            return !result;
        }

        public void Reset()
        {
            // clear vong tron

            _oldPointsList = Misc.Clone(_closestPointsList);
            _closestPointsList.Clear();
        }

        public override string ToString()
        {
            return String.Join(",", _array);
        }
    }
}
