using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMeansGUI
{
    
    public class diem
    {

        public diem()
        {
            x = 0;
            y = 0;
            kc = 0;
            mau = "red";
        }

        public diem(double _x, double _y, double _kc, string _mau)
        {
            this.x = _x;
            this.y = _y;
            this.kc = _kc;
            this.mau = _mau;
        }

        string mau;

        public string Mau
        {
            get { return mau; }
            set { mau = value; }
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
    }
}
