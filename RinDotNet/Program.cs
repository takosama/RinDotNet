using System;
using System.Linq;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace RinDotNet
{
    class Program
    {
        static void Main(string[] args)
        {
            //  配列の生成
            float[] vec1 = Enumerable.Range(0, 10000).Select(x => (float)x).ToArray();
            float[] vec2 = Enumerable.Range(0, 10000).Select(x => (float)2 * x).ToArray();

            //Dot積を得る
            float dot = Vector.Dot(vec1, vec2);
            Console.WriteLine(dot);
        }
    }

    class Vector
    {
     static   unsafe public float Dot(float[] vec1, float[] vec2)
        {
            if (vec1.Length != vec2.Length)
                throw new Exception("配列の要素数が違うため内積を求められません" + "vec1 =" + vec1.Length + " vec2=" + vec2.Length);
            if (vec1.Length % 8 != 0)
                throw new Exception("配列の要素数は8の倍数でなければなりません(256bit)" + "vec1 =" + vec1.Length + " vec2=" + vec2.Length);

            fixed (float* p1 = vec1)
            fixed (float* p2 = vec2)
            {
                var sum0 = Vector256<float>.Zero;
                var sum1 = Vector256<float>.Zero;
                var sum2 = Vector256<float>.Zero;
                var sum3 = Vector256<float>.Zero;
                float* tmp = stackalloc float[8];
                float* pp1 = p1;
                float* pp2 = p2;
                for (int i = 0; i < vec1.Length / 64; i++)
                {
                    sum0 = Fma.MultiplyAdd(Avx.LoadVector256(pp1 + 00), Avx.LoadVector256(pp2 + 00), sum0);
                    sum0 = Fma.MultiplyAdd(Avx.LoadVector256(pp1 + 08), Avx.LoadVector256(pp2 + 08), sum0);
                    sum1 = Fma.MultiplyAdd(Avx.LoadVector256(pp1 + 16), Avx.LoadVector256(pp2 + 16), sum1);
                    sum1 = Fma.MultiplyAdd(Avx.LoadVector256(pp1 + 24), Avx.LoadVector256(pp2 + 24), sum1);

                    sum2 = Fma.MultiplyAdd(Avx.LoadVector256(pp1 + 32), Avx.LoadVector256(pp2 + 32), sum2);
                    sum2 = Fma.MultiplyAdd(Avx.LoadVector256(pp1 + 40), Avx.LoadVector256(pp2 + 40), sum2);
                    sum3 = Fma.MultiplyAdd(Avx.LoadVector256(pp1 + 48), Avx.LoadVector256(pp2 + 48), sum3);
                    sum3 = Fma.MultiplyAdd(Avx.LoadVector256(pp1 + 56), Avx.LoadVector256(pp2 + 56), sum3);

                    pp1 += 64;
                    pp2 += 64;
                }
                sum0 = Avx.Add(Avx.Add(sum0, sum1), Avx.Add(sum2, sum3));

                for (int i = vec1.Length / 64 * 64; i < vec1.Length; i += 8)
                    sum0 = Fma.MultiplyAdd(Avx.LoadVector256(p1 + i), Avx.LoadVector256(p2 + i), sum0);


                Avx.Store(tmp, sum0);

                return tmp[0] + tmp[1] + tmp[2] + tmp[3] + tmp[4] + tmp[5] + tmp[6] + tmp[7];
            }
        }

    }

}
