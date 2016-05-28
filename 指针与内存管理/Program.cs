using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 指针与内存管理
{
    class Program
    {
        static unsafe void Main(string[] args)
        {
            //int* ip;
            //int x = 10;
            //ip = &x;
            //*ip = 100;
            //Console.WriteLine(*ip);
            //Console.WriteLine(sizeof(uint));
            //checked { uint y = (uint)ip; }
            //Console.WriteLine((uint)ip);
            //int* kp = (int*)y;
            //Console.WriteLine((int)kp);
            //Console.WriteLine(*kp);
            //byte aByte = 8;
            //byte bByte = 6;
            //Console.WriteLine(aByte.ToString("X4"));
            //Console.WriteLine((ulong)&aByte);
            //Console.WriteLine((ulong)&bByte);

            //byte* pByte1 = &aByte, pByte2 = &bByte;
            //pByte1 -= 8;
            //Console.WriteLine(*pByte1);

            //int* pInts = stackalloc int[20];
            //pInts[22] = 1;
            //Console.WriteLine(pInts[22]);
            int[] p = new int[20];
            p[20] = 1;
            Console.WriteLine(p[20]);

        }
    }

    class A
    {
        public A()
        {
            Console.WriteLine("Creating A");
        }
        ~A()
        {
            Console.WriteLine("Destroying A");
        }
    }

    class B : A
    {

        public B()
        {
            Console.WriteLine("Creating B");
        }
        ~B()
        {
            Console.WriteLine("Destroying B");
        }
        public C c = new C();
    }
    class C
    {
        public C()
        {
            Console.WriteLine("Creating C");
        }

        ~C()
        {
            Console.WriteLine("Destroying C");
        }
    }
}
