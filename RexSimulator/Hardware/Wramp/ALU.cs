﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RexSimulator.Hardware.Wramp
{
    /// <summary>
    /// The Arithmetic Logic Unit for the WRAMP processor.
    /// 
    /// How to use:
    /// 1) Set the inputs to the ALU (Rs, Rt, Func)
    /// 2) Read result (Result)
    /// Easy!
    /// </summary>
    public class ALU
    {
        #region Member Vars
        private uint mRs, mRt;
        private IR.Function mFunc;
        #endregion

        #region Inputs
        /// <summary>
        /// The first source register.
        /// </summary>
        public uint Rs { get { return mRs; } set { mRs = value; } }
        /// <summary>
        /// The second source register.
        /// </summary>
        public uint Rt { get { return mRt; } set { mRt = value; } }
        /// <summary>
        /// The function to perform.
        /// </summary>
        public IR.Function Func { get { return mFunc; } set { mFunc = value; } }
        #endregion

        #region Internal
        /// <summary>
        /// A signed version of Rs
        /// </summary>
        private int mRss { get { return (int)mRs; } }
        /// <summary>
        /// A signed version of Rt
        /// </summary>
        private int mRts { get { return (int)mRt; } }
        #endregion

        #region Outputs
        /// <summary>
        /// The current result.
        /// </summary>
        public uint Result
        {
            get
            {
                switch (mFunc)
                {
                    //Arithmethc
                    case IR.Function.add: return (uint)(mRss + mRts);
                    case IR.Function.addu: return mRs + mRt;
                    case IR.Function.sub: return (uint)(mRss - mRts);
                    case IR.Function.subu: return mRs - mRt;
                    case IR.Function.mult: return (uint)(mRss * mRts);
                    case IR.Function.multu: return mRs * mRt;
                    case IR.Function.div: return (uint)(mRss / mRts);
                    case IR.Function.divu: return mRs / mRt;
                    case IR.Function.rem: return (uint)(mRss % mRts);
                    case IR.Function.remu: return mRs % mRt;
                    case IR.Function.sll: return mRs << mRts;
                    case IR.Function.and: return mRs & mRt;
                    case IR.Function.srl: return mRs >> mRts;
                    case IR.Function.or: return mRs | mRt;
                    case IR.Function.sra: return (uint)(mRss >> mRts);
                    case IR.Function.xor: return mRs ^ mRt;

                    //Test
                    case IR.Function.slt: return (mRss < mRts) ? 1u : 0u;
                    case IR.Function.sltu: return (mRs < mRt) ? 1u : 0u;
                    case IR.Function.sgt: return (mRss > mRts) ? 1u : 0u;
                    case IR.Function.sgtu: return (mRs > mRt) ? 1u : 0u;
                    case IR.Function.sle: return (mRss <= mRts) ? 1u : 0u;
                    case IR.Function.sleu: return (mRs <= mRt) ? 1u : 0u;
                    case IR.Function.sge: return (mRss >= mRts) ? 1u : 0u;
                    case IR.Function.sgeu: return (mRs >= mRt) ? 1u : 0u;
                    case IR.Function.seq: return (mRss == mRts) ? 1u : 0u;
                    case IR.Function.sequ: return (mRs == mRt) ? 1u : 0u;
                    case IR.Function.sne: return (mRss != mRts) ? 1u : 0u;
                    case IR.Function.sneu: return (mRs != mRt) ? 1u : 0u;

                    //Misc
                    case IR.Function.lhi: return mRt << 16;
                    case IR.Function.inc: return mRs + 1;
                    default: throw new InvalidOperationException("Unknown Function!");
                }
            }
        }
        #endregion
    }
}
