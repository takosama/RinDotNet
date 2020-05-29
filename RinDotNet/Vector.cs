using System;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using System.Runtime.InteropServices;

namespace RinDotNet
{
    public unsafe class Vector : IDisposable
    {
        int lng;
        float[] array;
        float* ptr;
        GCHandle gch;
        private bool disposedValue;

        public Vector(float[] arr)
        {
            this.gch = GCHandle.Alloc(arr, GCHandleType.Pinned);
            this.ptr = (float*)this.gch.AddrOfPinnedObject().ToPointer();
            this.array = arr;
            this.lng = array.Length;
        }

        static public float Dot(Vector v0, Vector v1)
        {
            if (v0.lng != v1.lng)
                throw new Exception();
            int lng = v0.lng;

            float* p0 = v0.ptr;
            float* p1 = v1.ptr;
            float* tmp = stackalloc float[8];

            if (lng < 8)
            {
                float sum = 0;
                for (int i = 0; i < lng; i++)
                    sum += p0[i] * p1[i];
                return sum;
            }
            if (lng < 64)
            {
                var sum0 = Vector256<float>.Zero;

                for (int i = 0; i <= lng - 8; i += 8)
                    sum0 = Fma.MultiplyAdd(Avx.LoadVector256(p0 + i), Avx.LoadVector256(p1 + i), sum0);

                Avx.Store(tmp, sum0);
                float sum = tmp[0] + tmp[1] + tmp[2] + tmp[3] + tmp[4] + tmp[5] + tmp[6] + tmp[7];

                for (int i = lng / 8 * 8; i < lng; i++)
                    sum += p0[i] * p1[i];
                return sum;
            }
            else
            {
                var sum = Vector256<float>.Zero;
                var sum0 = Vector256<float>.Zero;
                var sum1 = Vector256<float>.Zero;
                var sum2 = Vector256<float>.Zero;
                var sum3 = Vector256<float>.Zero;
                var sum4 = Vector256<float>.Zero;
                var sum5 = Vector256<float>.Zero;
                var sum6 = Vector256<float>.Zero;
                var sum7 = Vector256<float>.Zero;
                float* pp1 = p0;
                float* pp2 = p1;
                double dsum = 0;
                for (int i = 0; i < lng / 64; i++)
                {
                    sum0 = Fma.MultiplyAdd(Avx.LoadVector256(pp1 + 00), Avx.LoadVector256(pp2 + 00), sum0);
                    sum1 = Fma.MultiplyAdd(Avx.LoadVector256(pp1 + 08), Avx.LoadVector256(pp2 + 08), sum1);
                    sum2 = Fma.MultiplyAdd(Avx.LoadVector256(pp1 + 16), Avx.LoadVector256(pp2 + 16), sum2);
                    sum3 = Fma.MultiplyAdd(Avx.LoadVector256(pp1 + 24), Avx.LoadVector256(pp2 + 24), sum3);

                    sum4 = Fma.MultiplyAdd(Avx.LoadVector256(pp1 + 32), Avx.LoadVector256(pp2 + 32), sum4);
                    sum5 = Fma.MultiplyAdd(Avx.LoadVector256(pp1 + 40), Avx.LoadVector256(pp2 + 40), sum5);
                    sum6 = Fma.MultiplyAdd(Avx.LoadVector256(pp1 + 48), Avx.LoadVector256(pp2 + 48), sum6);
                    sum7 = Fma.MultiplyAdd(Avx.LoadVector256(pp1 + 56), Avx.LoadVector256(pp2 + 56), sum7);

                    pp1 += 64;
                    pp2 += 64;
                    //精度改善のためdoubleに結果を保存しておく
                    if (i % 1024 == 1023)
                    {
                        sum = Avx.Add(Avx.Add(Avx.Add(sum0, sum1), Avx.Add(sum2, sum3)), Avx.Add(Avx.Add(sum4, sum5), Avx.Add(sum6, sum7)));
                        Avx.Store(tmp, sum);
                        dsum += tmp[0] + tmp[1] + tmp[2] + tmp[3] + tmp[4] + tmp[5] + tmp[6] + tmp[7];
                        sum0 = Vector256<float>.Zero;
                        sum1 = Vector256<float>.Zero;
                        sum2 = Vector256<float>.Zero;
                        sum3 = Vector256<float>.Zero;
                        sum4 = Vector256<float>.Zero;
                        sum5 = Vector256<float>.Zero;
                        sum6 = Vector256<float>.Zero;
                        sum7 = Vector256<float>.Zero;
                    }
                }
                sum = Avx.Add(Avx.Add(Avx.Add(sum0, sum1), Avx.Add(sum2, sum3)), Avx.Add(Avx.Add(sum4, sum5), Avx.Add(sum6, sum7)));

                for (int i = lng / 64 * 64; i <= lng - 8; i += 8)
                    sum = Fma.MultiplyAdd(Avx.LoadVector256(p0 + i), Avx.LoadVector256(p1 + i), sum);

                Avx.Store(tmp, sum);
                dsum += tmp[0] + tmp[1] + tmp[2] + tmp[3] + tmp[4] + tmp[5] + tmp[6] + tmp[7];


                for (int i = lng / 8 * 8; i < lng; i++)
                    dsum += p0[i] * p1[i];
                return (float)dsum;
            }
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                this.gch.Free();
                disposedValue = true;
            }
        }

        ~Vector()
        {
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }

}
