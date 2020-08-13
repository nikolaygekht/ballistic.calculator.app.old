using System;
using System.Collections.Generic;
using System.Text;

namespace MathEx.ExternalBallistic.JBM
{
    public enum DragTable
    {
        G1,
        G2,
        G5,
        G6,
        G7,
        G8,
        GL,
        GI
    }

    public interface IDragFunction
    {
        DragTable Table
        {
            get;
        }


        double calculate(double match);
    }

    public static class DragFunctionFactory
    {
        public static IDragFunction create(DragTable table)
        {
            IDragFunction function = null;
            switch (table)
            {
                case DragTable.G1:
                    function = new DragFunctionG1();
                    break;
                case DragTable.G2:
                    function = new DragFunctionG2();
                    break;
                case DragTable.G5:
                    function = new DragFunctionG5();
                    break;
                case DragTable.G6:
                    function = new DragFunctionG6();
                    break;
                case DragTable.G7:
                    function = new DragFunctionG7();
                    break;
                case DragTable.G8:
                    function = new DragFunctionG8();
                    break;
                case DragTable.GL:
                    function = new DragFunctionGL();
                    break;
                case DragTable.GI:
                    function = new DragFunctionGI();
                    break;
            }
            return function;
        }
    }

    abstract class DragFunction : IDragFunction
    {
        private DragTable mTableId;

        public DragTable Table
        {
            get
            {
                return mTableId;
            }
        }

        protected DragFunction(DragTable id)
        {
            mTableId = id;
        }

        public abstract double calculate(double mach);
    }

    class DragFunctionG1 : DragFunction
    {
        internal DragFunctionG1()
            : base(DragTable.G1)
        {
        }

        override public double calculate(double mach)
        {
            if (mach > 2.0)
                return 0.9482590 + mach * (-0.248367 + mach * 0.0344343);
            else if (mach > 1.40)
                return 0.6796810 + mach * (0.0705311 - mach * 0.0570628);
            else if (mach > 1.10)
                return -1.471970 + mach * (3.1652900 - mach * 1.1728200);
            else if (mach > 0.85)
                return -0.647392 + mach * (0.9421060 + mach * 0.1806040);
            else if (mach >= 0.55)
                return 0.6224890 + mach * (-1.426820 + mach * 1.2094500);
            else
                return 0.2637320 + mach * (-0.165665 + mach * 0.0852214);
        }
    }

    class DragFunctionG2 : DragFunction
    {
        internal DragFunctionG2()
            : base(DragTable.G2)
        {

        }

        override public double calculate(double mach)
        {
            if (mach > 2.5)
                return 0.4465610 + mach * (-0.0958548 + mach * 0.00799645);
            else if (mach > 1.2)
                return 0.7016110 + mach * (-0.3075100 + mach * 0.05192560);
            else if (mach > 1.0)
                return -1.105010 + mach * (2.77195000 - mach * 1.26667000);
            else if (mach > 0.9)
                return -2.240370 + mach * 2.63867000;
            else if (mach >= 0.7)
                return 0.9099690 + mach * (-1.9017100 + mach * 1.21524000);
            else
                return 0.2302760 + mach * (0.000210564 - mach * 0.1275050);
        }
    }


    class DragFunctionG5 : DragFunction
    {
        internal DragFunctionG5()
            : base(DragTable.G5)
        {
        }

        override public double calculate(double mach)
        {
            if (mach > 2.0)
                return 0.671388 + mach * (-0.185208 + mach * 0.0204508);
            else if (mach > 1.1)
                return 0.134374 + mach * (0.4378330 - mach * 0.1570190);
            else if (mach > 0.9)
                return -0.924258 + mach * 1.24904;
            else if (mach >= 0.6)
                return 0.654405 + mach * (-1.4275000 + mach * 0.998463);
            else
                return 0.186386 + mach * (-0.0342136 - mach * 0.035691);
        }
    }


    class DragFunctionG6 : DragFunction
    {
        internal DragFunctionG6()
            : base(DragTable.G6)
        {
        }

        override public double calculate(double mach)
        {
            if (mach > 2.0)
                return 0.746228 + mach * (-0.255926 + mach * 0.0291726);
            else if (mach > 1.1)
                return 0.513638 + mach * (-0.015269 - mach * 0.0331221);
            else if (mach > 0.9)
                return -0.908802 + mach * 1.25814;
            else if (mach >= 0.6)
                return 0.366723 + mach * (-0.458435 + mach * 0.337906);
            else
                return 0.264481 + mach * (-0.157237 + mach * 0.117441);
        }
    }


    class DragFunctionG7 : DragFunction
    {
        internal DragFunctionG7()
            : base(DragTable.G7)
        {
        }

        override public double calculate(double mach)
        {
            if (mach > 1.9)
                return 0.439493 + mach * (-0.0793543 + mach * 0.00448477);
            else if (mach > 1.05)
                return 0.642743 + mach * (-0.2725450 + mach * 0.049247500);
            else if (mach > 0.90)
                return -1.69655 + mach * 2.03557;
            else if (mach >= 0.60)
                return 0.353384 + mach * (-0.69240600 + mach * 0.50946900);
            else
                return 0.119775 + mach * (-0.00231118 + mach * 0.00286712);
        }
    }

    class DragFunctionG8 : DragFunction
    {
        internal DragFunctionG8()
            : base(DragTable.G8)
        {
        }

        override public double calculate(double mach)
        {
            if (mach > 1.1)
                return 0.639096 + mach * (-0.197471 + mach * 0.0216221);
            else if (mach >= 0.925)
                return -12.9053 + mach * (24.9181 - mach * 11.6191);
            else
                return 0.210589 + mach * (-0.00184895 + mach * 0.00211107);
        }
    }


    class DragFunctionGL : DragFunction
    {
        internal DragFunctionGL()
            : base(DragTable.GL)
        {
        }

        override public double calculate(double mach)
        {
            if (mach > 1.0)
                return 0.286629 + mach * (0.3588930 - mach * 0.0610598);
            else if (mach >= 0.8)
                return 1.59969 + mach * (-3.9465500 + mach * 2.831370);
            else
                return 0.333118 + mach * (-0.498448 + mach * 0.474774);
        }
    }

    class DragFunctionGI : DragFunction
    {
        internal DragFunctionGI()
            : base(DragTable.GI)
        {
        }

        override public double calculate(double mach)
        {
            if (mach > 1.65)
                return 0.845362 + mach * (-0.143989 + mach * 0.0113272);
            else if (mach > 1.2)
                return 0.630556 + mach * 0.00701308;
            else if (mach >= 0.7)
                return 0.531976 + mach * (-1.28079 + mach * 1.17628);
            else
                return 0.2282;
        }
    }

    public class BallisticCoefficitent
    {
        private double mValue;
        private IDragFunction mFunction;

        public double Value
        {
            get
            {
                return mValue;
            }
        }

        public IDragFunction DragFunction
        {
            get
            {
                return mFunction;
            }
        }

        public DragTable DragTable
        {
            get
            {
                return mFunction.Table;
            }
        }

        private const double BC_PIR = 2.08551e-04;

        public double getDrag(double mach)
        {
            return BC_PIR * mFunction.calculate(mach) / mValue;
        }

        public BallisticCoefficitent(double bc, DragTable table)
        {
            mValue = bc;
            mFunction = DragFunctionFactory.create(table);
        }
    }
}
