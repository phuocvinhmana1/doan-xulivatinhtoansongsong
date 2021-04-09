using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMeansGUI
{
    public class tamdiemcu
    {
        public tamdiemcu()
        {
            x= 0;
            y= 0;
            mau = "Red";
            kc= 0;
            dem = 0;
        }
        public tamdiemcu(double _x, double _y, double _kc, string _mau, double _dem)
        {
            this.x = _x;
            this.y = _y;
            this.kc = _kc;
            this.mau = _mau;
            this.dem = _dem;
        }
        public tamdiemcu(double _x, double _y)
        {
            this.x = _x;
            this.y = _y;
       
        }
        double dem;

        public double Dem
        {
            get { return dem; }
            set { dem = value; }
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
        double kc;

        public double Kc
        {
            get { return kc; }
            set { kc = value; }
        }
        string mau;

        public string Mau
        {
            get { return mau; }
            set { mau = value; }
        }

    }
}
