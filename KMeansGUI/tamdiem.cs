using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMeansGUI
{
    public class tamdiem
    {

        public tamdiem()
        {
            x = 0;
            y = 0;
            mausac = "do";
            kc = 0;
            dem = 0;
        }
        public tamdiem(double _x, double _y, string mau, double _kc, double _dem)
        {
            x = _x;
            y = _y;
            mausac = mau;
            kc = _kc;
            dem = _dem;

        }


        double dem;

        public double Dem
        {
            get { return dem; }
            set { dem = value; }
        }
        double kc;

        public double Kc
        {
            get { return kc; }
            set { kc = value; }
        }
        double x;

        public double X
        {
            get { return x; }
            set { x = value; }
        }
        double y;

        public double Y
        {
            get { return y; }
            set { y = value; }
        }
        string mausac;

        public string Mausac
        {
            get { return mausac; }
            set { mausac = value; }
        }
    }
}
