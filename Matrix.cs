using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System;

namespace NeuroLib
{
	public static class Matrix
	{
		public static float [,] Inner(float[,] A, float[,] B)
		{
			float [,] C = new float[A.GetLength(0),B.GetLength(1)];
			if (A.GetLength(1)!=B.GetLength(0))
				Debug.Print("Mismatch");
			else{ 
				for (int i=0;i<A.GetLength(0);i++){
					for (int j=0;j<B.GetLength(1);j++){
						for (int k=0;k<A.GetLength(1);k++){
							C[i,j]+=A[i,k]*B[k,j];
						}
					}
				}
			}
			return C;
		}
        public static float Sum(float[,] A)
        {
            float C = 0f;
            for (int i = 0; i < A.GetLength(0); i++)
            {
                for (int j = 0; j < A.GetLength(1); j++)
                {
                    C += A[i, j];
                }
            }
            return C;
        }
        public static float [,] Transpose(float[,] A)
		{
			float [,] C = new float[A.GetLength(1),A.GetLength(0)];
			for (int i=0;i<A.GetLength(1);i++){
				for (int j=0;j<A.GetLength(0);j++){
					C [i, j] = A [j, i];
				}
			}
			return C;
		}
        public static float[,] Slice(float[,] A, int I1, int I2, int J1, int J2)
        {
            float[,] C = new float[Math.Abs(I1-I2), Math.Abs(J1 -J2)];
            for (int i = I1; i < I2; i++)
            {
                for (int j = J1; j < J2; j++)
                {
                    C[i-I1, j-J1] = A[i,j];
                }
            }
            return C;
        }
        public static float[,] Concatenate(float[,] A, float[,]B, int axis)
        {
            float[,] C = new float[A.GetLength(0) + B.GetLength(0), A.GetLength(1)]; ;
            if (axis == 0 && A.GetLength(1)== B.GetLength(1))
            {
                C = new float[A.GetLength(0)+ B.GetLength(0), A.GetLength(1)];
                for (int i = 0; i < A.GetLength(0); i++)
                {
                    for (int j = 0; j < A.GetLength(1); j++)
                    {
                        C[i, j] = A[i, j];
                    }
                }
                for (int i = A.GetLength(0); i < A.GetLength(0)+ B.GetLength(0); i++)
                {
                    for (int j = 0; j < B.GetLength(1); j++)
                    {
                        C[i, j] = B[i- A.GetLength(0), j];
                    }
                }
            }
            else if (axis == 1 && A.GetLength(0) == B.GetLength(0))
            {
                C = new float[A.GetLength(0), A.GetLength(1) + B.GetLength(1)];
                for (int i = 0; i < A.GetLength(0); i++)
                {
                    for (int j = 0; j < A.GetLength(1); j++)
                    {
                        C[i, j] = A[i, j];
                    }
                    for (int j = A.GetLength(1); j < A.GetLength(1)+ B.GetLength(1); j++)
                    {
                        C[i, j] = B[i, j- A.GetLength(1)];
                    }
                }
                
            }
            
            return C;
        }
        public static float[,] Add(float[,] A, float[,] B, float multiplier)
        {
            float[,] C = new float[A.GetLength(0), A.GetLength(1)];
            if (A.GetLength(0) != B.GetLength(0) || A.GetLength(1) != B.GetLength(1))
                Debug.Print("Mismatch");
            else
            {
                for (int i = 0; i < A.GetLength(1); i++)
                {
                    for (int j = 0; j < A.GetLength(0); j++)
                    {
                        C[i, j] = multiplier * (A[i, j] + B[i,j]);
                    }
                }
            }
            
            return C;
        }
        public static float[,] Increment(float[,] A, float[,] B, float step)
        {
            float[,] C = new float[A.GetLength(0), A.GetLength(1)];
            if (A.GetLength(0) != B.GetLength(0) || A.GetLength(1) != B.GetLength(1))
                Debug.Print("Mismatch");
            else
            {
                for (int i = 0; i < A.GetLength(1); i++)
                {
                    for (int j = 0; j < A.GetLength(0); j++)
                    {
                        C[i, j] = A[i, j] + (B[i, j]*step);
                    }
                }
            }

            return C;
        }
        public static float [,] Outer(float[,] A, float[,] B)
		{
			float [,] C = new float[A.GetLength(1),B.GetLength(0)]; 
			if (A.GetLength (0) != B.GetLength (1)) {
				Debug.Print ("Mismatch");
			}
			else{
				for (int i=0;i<A.GetLength(0);i++){
					for (int j=0;j<B.GetLength(1);j++){
						C[i,j]=A[i,j]*B[j,i];
					}
				}

			}return C;
		}
		public static float[,] Hadamard(float[,] A, float[,] B)
		{
			float [,] C = new float[A.GetLength(0),A.GetLength(1)]; 
			if (A.GetLength(0)!=B.GetLength(0)||A.GetLength(1)!=B.GetLength(1))
				Debug.Print("Mismatch");
			else{
				for (int i=0;i<A.GetLength(0);i++){
					for (int j=0;j<B.GetLength(1);j++){
							C[i,j]=A[i,j]*B[i,j];
					}
				}
					
			}return C;
		}
		public static float[,] Map(float[,] A, string functionName)
		{
			float [,] C = new float[A.GetLength(0), A.GetLength(1)];
			switch (functionName) {
			    case "Logistic":
				    for (int i=0;i<A.GetLength(0);i++){
                        for (int j = 0; j < A.GetLength(1); j++)
                        {
                            C[i,j] = Maths.Logistic(A[i,j]);
                        }
                    }
				break;
			    case "dLogistic":
				    for (int i=0;i<A.GetLength(0);i++){
                        for (int j = 0; j< A.GetLength(1); j++)
                        {
                            C[i,j] = A[i,j] * (1 - A[i,j]);
                        }
                    }
				break;
			    case "LogisticInverse":
				    for (int i=0;i<A.GetLength(0);i++){
                        for (int j = 0; j < A.GetLength(1); j++)
                        {
                            C[i,j] = Maths.LogisticInverse(A[i,j]);
                        }
                    }
				break;
			    case "Tanh":
				    for (int i=0;i<A.GetLength(0);i++){
                        for (int j = 0; j < A.GetLength(1); j++)
                        {
                            C[i, j] = Maths.Tanh(A[i, j]);
                        }
                    }
				break;
			    case "dTanh":
				    for (int i=0;i<A.GetLength(0);i++){
                        for(int j = 0; j < A.GetLength(1); j++)
                        {
                            C[i, j] = 1 - A[i, j] * A[i, j];
                        }
                    }
				break;
			    case "LogApprox":
				    for (int i=0;i<A.GetLength(0);i++){
                        for (int j = 0; j < A.GetLength(1); j++)
                        {
                            C[i, j] = Maths.LogApprox(A[i, j]);
                        }
                    }
				break;
                case "dLog":
                    for (int i = 0; i < A.GetLength(0); i++)
                    {
                        for (int j = 0; j < A.GetLength(1); j++)
                        {
                            C[i, j] = 1 / A[i, j];
                        }
                    }
                break;
                case "SoftMax":

                    float Z = 0;
                    for (int i = 0; i < A.GetLength(0); i++)
                    {
                        for (int j = 0; j < A.GetLength(1); j++)
                        {
                            float b = (float)Math.Pow(Math.E, A[i, j]);
                            C[i, j] = b;
                            Z += b;
                        }
                    }
                    for (int i = 0; i < A.GetLength(0); i++)
                    {
                        for (int j = 0; j < A.GetLength(1); j++)
                        {
                            C[i, j] = C[i, j]/Z;
                        }
                    }
                break;
                //case "dSoftMax":
                //    for (int i = 0; i < A.GetLength(0); i++)
                //    {
                //        for (int j = 0; j < A.GetLength(1); j++)
                //        {
                //            C[i, j] = A[i, j] - A[i, j] * A[i, j];
                //        }
                //    }
                //    break;

            }
            return C;	
		}
        public static float Error(float[,] A, float[,] B, string functionName)
        {
            float C = 0;
            switch (functionName)
            {
                case "MeanSquare":
                    for (int i = 0; i < A.GetLength(0); i++)
                    {
                        for (int j = 0; j < A.GetLength(1); j++)
                        {
                            C += (float)Math.Pow(A[i,j]-B[i,j],2);
                        }
                    }
                    break;
                case "dMeanSquare":

                    for (int i = 0; i < A.GetLength(0); i++)
                    {
                        for (int j = 0; j < A.GetLength(1); j++)
                        {
                            C += (A[i, j]-B[i,j]);
                        }
                    }
                    break;
                //case "XEnt":
                //    for (int i = 0; i < A.GetLength(0); i++)
                //    {
                //        for (int j = 0; j < A.GetLength(1); j++)
                //        {
                //            C += (float)(-B[i,j] * Math.Log(A[i, j]));
                //        }
                //    }
                //    break;
                //case "dXEnt":
                //    for (int i = 0; i < A.GetLength(0); i++)
                //    {
                //        for (int j = 0; j < A.GetLength(1); j++)
                //        {
                //            C = Maths.Tanh(A[i, j]);
                //        }
                //    }
                //    break;
                    //case "dTanh":
                    //    for (int i = 0; i < A.GetLength(0); i++)
                    //    {
                    //        for (int j = 0; j < A.GetLength(1); j++)
                    //        {
                    //            C[i, j] = 1 - A[i, j] * A[i, j];
                    //        }
                    //    }
                    //    break;
                    //case "LogApprox":
                    //    for (int i = 0; i < A.GetLength(0); i++)
                    //    {
                    //        for (int j = 0; j < A.GetLength(1); j++)
                    //        {
                    //            C[i, j] = Maths.LogApprox(A[i, j]);
                    //        }
                    //    }
                    //    break;
                    //case "dLog":
                    //    for (int i = 0; i < A.GetLength(0); i++)
                    //    {
                    //        for (int j = 0; j < A.GetLength(1); j++)
                    //        {
                    //            C[i, j] = 1 / A[i, j];
                    //        }
                    //    }
                    //    break;
                    //case "MeanSquare":
                    //    for (int i = 0; i < A.GetLength(0); i++)
                    //    {
                    //        for (int j = 0; j < A.GetLength(1); j++)
                    //        {
                    //            C[i, j] = 1 / A[i, j];
                    //        }
                    //    }
                    //    break;

            }
            return C;
        }
        public static float[,] Identity(int n)
		{
			float[,] I = new float [n,n];
			for (int i=0;i<n;i++){
				for (int j=0;j<n;j++){
					if (i==j)
						I[i,j]=1;
					else I[i,j]=0;

				}
			}
			return I;
		}
		public static float[,] UniformRandomMatrix(int n, int m, float hi, float lo)
		{
            float[,] R = new float [n,m];
			for (int i=0;i<n;i++){
				for (int j=0;j<m;j++){
                    R[i, j] = (float)(RNG.GenerateFromUniform(hi,lo));
                    //if (R[i,j]<= (float)((hi - lo) * random.NextDouble() + lo))
                    //{
                    //    R[i, j] += (float)(random.NextDouble()*(hi - lo) * RNG.GenerateFromUniform(1,-1) + lo);
                    //}
                }
			}
			return R;
		}
		public static float[,] GaussianRandomMatrix(int n, int k, float m, float s)
		{
			float[,] R = new float [n,k];
			for (int i=0;i<n;i++){
				for (int j=0;j<k;j++){
					R [i, j] = RNG.GenerateFromGaussian(m,s);
				}
			}
			return R;
		}
	}
}
