using System;
using System.Linq;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Attributes;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Iced.Intel;
using System.Security.Cryptography;
using System.Threading;
using MathNet.Numerics.Providers.Common.Mkl;
using RinDotNet;

namespace PG
{
    class Program
    {

        static unsafe void Main(string[] args)
        {
            //  for (int i = 1; i < 10000000; i *= 2)
            //  {
            //      VectorBentimark b = new VectorBentimark(i);
            //      Console.WriteLine(i);
            //      Console.WriteLine(b.DotMathNetMKL());
            //      Console.WriteLine(b.RinDot());
            //      Console.WriteLine((float)b.NomalFor());
          //  }//


            Mat m0 = new Mat(new float[8 * 8], 8, 8);
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    m0.Set(i, j, i + j);
            Mat m1 = new Mat(new float[8 * 8], 8, 8);
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    m1.Set(i, j, i + j);
            var rtn = Mat.Dot3(m0, m1);

            var mat0 = MathNet.Numerics.LinearAlgebra.CreateMatrix.Dense<float>(8, 8);
            var mat1 = MathNet.Numerics.LinearAlgebra.CreateMatrix.Dense<float>(8, 8);
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    mat0[i, j] = i + j;
                    mat1[i, j] = i + j;
                }



   var mat2=         mat0.Multiply(mat1);

           var m2= rtn.GetArray;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                    Console.Write(m2[i][j] + " ");
                Console.WriteLine();
            }


            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                    Console.Write(mat2[i,j] + " ");
                Console.WriteLine();
            }



            BenchmarkRunner.Run<MatBentimark>();

            //  配列の生成
            //  var vec1 = Enumerable.Range(0, 10000).Select(x => (float) x).ToArray();
            //  var vec2 = Enumerable.Range(0, 10000).Select(x => (float) 2 * x).ToArray();
            // 
            //  //Dot積を得る
            //  float dot = Vector.Dot(new Vector(vec1), new Vector(vec2));
            //  Console.WriteLine(dot);
        }
    }


    public class MatBentimark
    {
        Mat m0;
        Mat m1;
        Matrix<float> mat0;
        Matrix<float> mat1;
        int size = 512;
        public MatBentimark()
        {
            m0 = new Mat(new float[size * size], size, size);
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    m0.Set(i, j, i + j);
            m1 = new Mat(new float[size * size], size, size);
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    m1.Set(i, j, i + j);
            var rtn = Mat.Dot(m0, m1);

            mat0 = MathNet.Numerics.LinearAlgebra.CreateMatrix.Dense<float>(size, size);
            mat1 = MathNet.Numerics.LinearAlgebra.CreateMatrix.Dense<float>(size, size);
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                {
                    mat0[i, j] = i + j;
                    mat1[i, j] = i + j;
                }
        }

        public Mat DotRin()
        {
      return     Mat.Dot(m0, m1);
        }
        public Mat DotRin2()
        {
            return Mat.Dot2(m0, m1);
        }
        [Benchmark]
        public Mat DotRin3()
        {
            return Mat.Dot3(m0, m1);
        }
        [Benchmark]
        public Matrix<float> DotMKL()
        {
          return  mat0.Multiply(mat1);
        }

    }

    public class VectorBentimark
    {
        int size = 100000000;
        float[] vec1;
        float[] vec2;
        Vector<float> v1; 
        Vector<float> v2;

        Vector mv1;
        Vector mv2;
        public VectorBentimark()
        {
            vec1 = Enumerable.Range(0, size).Select(x => (float)x).ToArray();
            vec2 = Enumerable.Range(0, size).Select(x => (float)x).ToArray();
            this.v1 = CreateVector.DenseOfArray(vec1);
            this.v2 = CreateVector.DenseOfArray(vec2);
            this.mv1 = new Vector(vec1);
            this.mv2 = new Vector(vec2);
        }

        public VectorBentimark(int size)
        {
            this.size = size;
            Random r = new Random(0);

            vec1 = Enumerable.Range(0, size).Select(x => (float)r.Next()).ToArray();
            vec2 = Enumerable.Range(0, size).Select(x => (float)r.Next()).ToArray();
            this.v1 = CreateVector.DenseOfArray(vec1);
            this.v2 = CreateVector.DenseOfArray(vec2);
            this.mv1 = new Vector(vec1);
            this.mv2 = new Vector(vec2);
        }
        [Benchmark]
        public double NomalFor()
        {
            if (vec1.Length != vec2.Length)
                throw new Exception();
            double sum = 0;
            for (int i = 0; i < vec1.Length; i++)
                sum += (double)vec1[i] * (double)vec2[i];
            return sum;
        }

        [Benchmark]
        public float RinDot()
        {
            return Vector.Dot(mv1, mv2);
        }
        [Benchmark]

        public float DotMathNetMKL()
        {
            return v1.DotProduct(v2);
        }
    }

}
