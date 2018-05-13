using System.Collections;
using System.Collections.Generic;
using System;

namespace NeuroLib
{
	public static class Maths
	{
		public static float SumArray(float[] x, int start, int finish)
		{
			float X=0;

			for (int i=0;i<x.GetLength(1);i++){
				X += x [i];
				}
			return X;
		}
		public static float Logistic(float x)
		{
            return (float)(1 / (1 + Math.Pow(Math.E, -x)));
        }
		public static float LogisticInverse(float x)
		{
			return (float)(-Math.Log(1/x-1));
		}
		public static float Tanh(float x)
		{
			return 2*Logistic(2*x)-1;
		}
		public static float LogApprox(float x)
		{
			float X=0;
			for (int i=0;i< Math.Round(x, MidpointRounding.AwayFromZero); i++){
				X += 1/(float)i;
			}
			return X;
		}

	}
}
