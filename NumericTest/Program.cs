using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
 

using System.Text;
using System.Threading.Tasks;

namespace NumericTest
{
    class Program
    {
        const int realcount = 20000000;


        private static void SIMD()
        {

            var vecsiz = Vector<uint>.Count;
            Vector<uint>[] vectorints = new Vector<uint>[realcount / vecsiz];
            int count = realcount / vecsiz;

            for (int i = 0; i < count; i+= 1)
            {
                vectorints[i] =  Vector<uint>.One;
            }

            Stopwatch sw = new Stopwatch();
            sw.Start();

            var sum = Vector<uint>.Zero;
            for (int x = 0; x < 10; x++)
            {
                for (int i = 0; i < count; i++)
                {
                    sum += vectorints[i];
                }
            }
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds + " ms SIMD " + sum);
        }


        private static void Straight()
        {
            uint[] straightints = new uint[realcount];
            int count = realcount;

            for (int i = 0; i < count; i++)
            {
                straightints[i] = 1;
            }

            
            Stopwatch sw = new Stopwatch();
            sw.Start();

            uint sum = 0;
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < count; y++)
                {
                    sum += straightints[y];
                }
            }
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds + " ms Strg " + sum);
        }

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Simd Size: " + Vector<uint>.Count);

                SIMD();
                Straight();

                Console.WriteLine();
            }
        }
    }
}
