using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Problem
{
    // ***************
    // DON'T CHANGE CLASS OR FUNCTION NAME
    // YOU CAN ADD FUNCTIONS IF YOU NEED TO
    // ***************
    public static class IntegerMultiplication
    {
        #region YOUR CODE IS HERE

        //Your Code is Here:
        //==================
        /// <summary>
        /// Multiply 2 large integers of N digits in an efficient way [Karatsuba's Method]
        /// </summary>
        /// <param name="X">First large integer of N digits [0: least significant digit, N-1: most signif. dig.]</param>
        /// <param name="Y">Second large integer of N digits [0: least significant digit, N-1: most signif. dig.]</param>
        /// <param name="N">Number of digits (power of 2)</param>
        /// <returns>Resulting large integer of 2xN digits (left padded with 0's if necessarily) [0: least signif., 2xN-1: most signif.]</returns>
        static public byte[] IntegerMultiply(byte[] X, byte[] Y, int N)
        {
            //REMOVE THIS LINE BEFORE START CODING
            //throw new NotImplementedException();
            byte[] funsum(byte[] t, byte[] r)
            {
                if (r.Length > t.Length)
                {
                    t = pad(t, r.Length - t.Length);
                }
                else if (t.Length > r.Length)
                {
                    r = pad(r, t.Length - r.Length);
                }
                int size = r.Length;
                byte[] summmi = new byte[size];
                int carry = 0;
                int s = 0;
                for (int i = 0; i < r.Length; i++)
                {
                    s = t[i] + r[i] + carry;
                   summmi[i]=((byte)(s % 10));
                    carry = s / 10;
                }
                if (carry != 0)
                {
                    byte[] tmp = new byte[size+1] ;
                    tmp[size] = (byte)carry;
                    Array.Copy(summmi, tmp, summmi.Length);
                    summmi = tmp;
                }
                return summmi;
            }
            byte[] funappendrigth(byte[] t, int k)
            {
                int size = k + t.Length;
                int counter = 0;
                byte[] arr = new byte[size];
                for (int i = 0; i < size; i++)
                {
                    if (i < k)
                    {
                        arr[i] = 0;
                    }
                    else
                    {
                        arr[i] = t[counter];
                        counter += 1;
                    }
                }
                return arr;
            }
            byte[] functSubtrace(byte[] a, byte[] b)
            {
                if (a.Length > b.Length)
                {
                   b = pad(b, a.Length - b.Length);
                }
                else if(b.Length > a.Length)
                {
                    a = pad(a, b.Length - a.Length);
                }
                int max = Math.Max(a.Length, b.Length);
                int borrow = 0;
                byte[] ZED = new byte[max];
                for (int i = 0; i < max; i++)
                {
                    int subb = a[i] - b[i]- borrow;
                    if (subb < 0)
                    {
                        //for (int j = i + 1; j < max; j++)
                        //{
                        //    if (a[j] > 0)
                        //    {
                        //        a[j] -= 1;
                        //        break;
                        //    }
                        //}
                        subb += 10;
                        borrow = 1;
                    }
                    else
                    {
                        borrow = 0;
                    }
                    
                    ZED[i] = (byte)subb;
                }
                return ZED;
            }
            byte[] funccombine(byte[] m2, byte[] resofsub, byte[] m1)
            {
                if (m1.Length != m2.Length)
                {
                    m1 = pad(m1, m2.Length - m1.Length);
                }
                if (resofsub.Length != m2.Length)
                {
                    resofsub = pad(resofsub, m2.Length - resofsub.Length);
                }
                int s = 0, co = 0;
                int size = m1.Length;
                byte[] summ = new byte[size];
                for (int i = 0; i < m2.Length; i++)
                {
                    s = m2[i] + resofsub[i] + m1[i] + co;
                    summ[i]=((byte)(s % 10));
                    co = s / 10;
                }
                if (co != 0)
                {
                    byte[] tmp = new byte[size + 1];
                    tmp[size] = (byte)co;
                    Array.Copy(summ, tmp, summ.Length);
                    summ = tmp;
                }
                return summ;
            }
            byte[] pad(byte[] a, int k)
            {
                int size = a.Length + k;
                byte[] arra = new byte[size];
                for (int i = 0; i < size; i++)
                {
                    if (i < a.Length)
                    {
                        arra[i] = a[i];
                    }
                    else
                    {
                        arra[i] = 0;
                    }
                }
                return arra;
            }
            if (N<= 32)
            {
                int carry = 0;
                //int counter = 0;
                byte[] arr = new byte[2*N];
                for(int i = 0; i < N; i++)
                {
                    for(int j = 0; j < N; j++)
                    {
                        int multi = (X[i] * Y[j]) + arr[i+j]+carry;
                         carry = multi / 10;
                        arr[i+j] = (byte)(multi%10);
                        //counter += 1;
                    }
                    if (carry != 0)
                    {
                        arr[i + N] = (byte)carry;
                    }
                    carry = 0;
                    //if (carry != 0)
                    //{

                    //    byte[] tmp = new byte[(N) + 1];
                    //    tmp[i +N] = (byte)carry;
                    //    Array.Copy(arr, tmp, arr.Length);
                    //    arr = tmp;
                    //}
                    
                }

                return arr;
            }
            else
            {
                if (N % 2 != 0)
                {
                    N += 1;
                }
                byte[] A = new byte[N / 2];
                byte[] C = new byte[N / 2];
                int SIZE = N/ 2;
                for (int i = 0; i < N / 2; i++)
                {
                    A[i] = X[i];
                    C[i] = Y[i];
                }
                //for (int i = 0; i < N; i++)
                //{
                //    if (i < N / 2)
                //    {
                //        A[i] = X[i];
                //        C[i] = Y[i];
                //    }
                //    else
                //    {
                //        B[i - SIZE] = X[i];
                //        D[i - SIZE] = Y[i];
                //    }
                //}
                byte[] B = X.Skip(N / 2).ToArray();
                byte[] D = Y.Skip(N / 2).ToArray();
                // FIRST SUM
                byte[] AB = funsum(A, B);
                // SECOND SUM
                byte[] CD = funsum(C, D);
                // SELECT MAX
                int MAX = Math.Max(AB.Length, CD.Length);
                byte[] M1 =new byte[A.Length];
                byte[] M2 = new byte[B.Length];
                //RECRSUION 
                Task t1 = Task.Run(() => M1 = IntegerMultiply(A, C, A.Length));
                Task t2 = Task.Run(() => M2 = IntegerMultiply(B, D, B.Length));
                Task.WaitAll(t1,t2);
                if (AB.Length < MAX)
                {
                    AB = pad(AB, MAX - AB.Length);

                }
                if (CD.Length < MAX)
                {
                    CD = pad(CD, MAX - CD.Length);
                }
                byte[] ZZ = IntegerMultiply(AB, CD, MAX);
                // subtract
                byte[] fsub = functSubtrace(ZZ,M1);
                byte[] secsub = functSubtrace(fsub, M2);
                //  fIRST APPEND
                byte[] NewM2 = funappendrigth(M2, N);
                // SECOND APPEND 
                byte[] RESULTOFSUB = funappendrigth(secsub, (N / 2));
                // RESULT
                //byte[] fsum = funsum(NewM2, RESULTOFSUB);
                //byte[] Ssum = funsum(fsum, M1);
                byte[] res = funccombine( NewM2, RESULTOFSUB, M1);
                return  res;
            }
        }
        #endregion
    }
}