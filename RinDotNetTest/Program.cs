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

        unsafe static void Main(string[] args)
        {
            for (int i = 1; i < 10000000; i *= 2)
            {
                VectorBentimark b = new VectorBentimark(i);
                Console.WriteLine(i);
                Console.WriteLine(b.DotMathNetMKL());
                Console.WriteLine(b.RinDot());
                Console.WriteLine((float)b.NomalFor());
            }//
            //BenchmarkRunner.Run<VectorBentimark>();

            //  配列の生成
            float[] vec1 = Enumerable.Range(0, 10000).Select(x => (float)x).ToArray();
            float[] vec2 = Enumerable.Range(0, 10000).Select(x => (float)2 * x).ToArray();

            //Dot積を得る
            float dot = Vector.Dot(new Vector(vec1), new Vector(vec2));
            Console.WriteLine(dot);
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
