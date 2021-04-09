using KMeansGUI;
using System;
using System.Collections.Generic;
using System.Threading;

namespace KMeansProject
{
    public delegate void OnUpdateProgress(object sender, KMeansEventArgs eventArgs);
    public class KMeans
    {
        private KhoangCach  _khoangcach;
        
        private int _k;
        private int _dotre;

        public event OnUpdateProgress UpdateProgress;
        protected virtual void OnUpdateProgress(KMeansEventArgs eventArgs)
        {
            if (UpdateProgress != null)
                UpdateProgress(this, eventArgs);
            Thread.Sleep(_dotre);
        }

        public KMeans(int k, KhoangCach khoangcach,int dotre)
        {
            _dotre = dotre;
            _k = k;
            _khoangcach = khoangcach;

        }

        public TamCum[] Run(double[][] dataSet)
        {
            List<TamCum> tamcumList = new List<TamCum>();

            for (int i=0;i<_k;i++)
            {
                TamCum centroid = new TamCum(dataSet, Misc.centroidColors[i]);
                tamcumList.Add(centroid);
            }

            OnUpdateProgress(new KMeansEventArgs(tamcumList, dataSet));

            while (true)
            {
                foreach (TamCum centroid in tamcumList)
                    centroid.Reset();

                for (int i = 0; i < dataSet.GetLength(0); i++)
                {
                    // tinh khoang cach giua tam cum va diem

                    double[] point = dataSet[i];
                    int closestIndex = -1;
                    double minDistance = Double.MaxValue;
                    for (int k = 0; k < tamcumList.Count; k++)
                    {
                        double distance = _khoangcach.tinhToan(tamcumList[k].Array, point);
                        if (distance < minDistance)
                        {
                            closestIndex = k;
                            minDistance = distance;
                        }
                    }
                    // them diem gan nhat vao tam cum cua no
                    tamcumList[closestIndex].addPoint(point);
                    
                }

                foreach (TamCum centroid in tamcumList)
                    centroid.MoveCentroid();

                OnUpdateProgress(new KMeansEventArgs(tamcumList, null));

                bool hasChanged = false;
                foreach (TamCum centroid in tamcumList)
                    if (centroid.HasChanged())
                    {
                        hasChanged = true;
                        break;
                    }
                if (!hasChanged)
                    break;
            }

            return tamcumList.ToArray();
        }

    
    }
}
