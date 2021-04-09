using KMeansProject;
using System.Collections.Generic;

namespace KMeansGUI
{
    public class KMeansEventArgs
    {
        // khai bao 1 list tam cum
        private List<TamCum> _tamCumList;

        public List<TamCum> tamCumList
        {
            get { return _tamCumList; }
        }
        

        private double[][] _dataset;
        public double[][] Dataset
        {
            get { return _dataset; }
        }

        public KMeansEventArgs(List<TamCum> tamCumList,double[][] dataset)
        {
            _tamCumList = tamCumList;
            _dataset = dataset;
        }
    }
}
