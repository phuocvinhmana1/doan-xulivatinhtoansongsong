
using System;

namespace KMeansProject
{
    public class TinhKhoangCach : KhoangCach
    {
        // tinh khoang cach giua 2 diem 
        public double tinhToan(double[] diem1, double[] diem2)  // 1,2   3,2
        {
            double ketqua = 0;
            for (int i = 0; i < diem1.Length; i++)
            {
                ketqua = ketqua + Math.Pow(diem1[i] - diem2[i], 2);
            }
            return Math.Sqrt(ketqua); // tra ve khoang cach giua diem1 va diem2
        }
    }
}
