using System;
using System.Diagnostics;
using System.Numerics;


namespace NumericTest
{
    class Program
    {
        const int TestArraySize = 20000000;


        private static void SIMD()
        {
            var vecsize = Vector<uint>.Count; //SIMD instruction size
            Vector<uint>[] vectorized = new Vector<uint>[TestArraySize / vecsize];
            int count = TestArraySize / vecsize;

            for (int i = 0; i < count; i+= 1)
            {
                vectorized[i] =  Vector<uint>.One;
            }

            Stopwatch sw = new Stopwatch();
            sw.Start();

            var sum = Vector<uint>.Zero;
            for (int x = 0; x < 10; x++)
            {
                for (int i = 0; i < count; i++)
                {
                    sum += vectorized[i];
                }
            }

            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds + " ms SIMD " + sum);
        }


        private static void Plain()
        {
            uint[] Plainints = new uint[TestArraySize];
            int count = TestArraySize;

            for (int i = 0; i < count; i++)
            {
                Plainints[i] = 1;
            }

            
            Stopwatch sw = new Stopwatch();
            sw.Start();

            uint sum = 0;
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < count; y++)
                {
                    sum += Plainints[y];
                }
            }
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds + " ms Plain " + sum);
        }

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Simd Size: " + Vector<uint>.Count);

                SIMD();
                Plain();

                Console.WriteLine();

                //never trust a single first run from a JIT compiler
            }
        }
    }
}
