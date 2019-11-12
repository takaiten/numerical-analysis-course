﻿using System;

namespace ComMethods
{
    public abstract class Preconditioner
    {
        public abstract void StartPreconditioner(Vector x, Vector res);
        public abstract void StartTvPreconditioner(Vector x, Vector res);
        public enum PreconditionerType
        {
            Diagonal = 1,
            ILU // TODO: Create class for LU
        }
    }

    class DiagonalPreconditioner : Preconditioner
    {
        private Vector diag { get; }
        public override void StartPreconditioner(Vector x, Vector res)
        {
            for (int i = 0; i < diag.Size; i++)
            {
                if (diag.Elem[i] < CONST.EPS)
                    throw new Exception("Division by zero");
                res.Elem[i] = x.Elem[i] / diag.Elem[i];
            }
        }
        public override void StartTvPreconditioner(Vector x, Vector res)
        { StartPreconditioner(x, res); }
        
        public DiagonalPreconditioner(Matrix A)
        {
            diag = new Vector(A.Row);
            for (int i = 0; i < A.Row; i++) 
                diag.Elem[i] = A.Elem[i][i];
        }
    }
    class IncompleteLUPreconditioner : Preconditioner
    {
        private Matrix LU { get; }
        public override void StartPreconditioner(Vector x, Vector res)
        {
            
        }
        public override void StartTvPreconditioner(Vector x, Vector res)
        { StartPreconditioner(x, res); }
        public IncompleteLUPreconditioner(Matrix A)
        {
            LUDecomposition LUDecomp = new LUDecomposition(A);
            LU = LUDecomp.LU;
        }
    }
}