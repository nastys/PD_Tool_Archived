﻿namespace KKdBaseLib
{
    public interface IKeyFrame<TKey, TVal>
    {
        IKeyFrame<TKey, TVal> Check();
        IKeyFrame<TKey, TVal> ToKeyFrameT0();
        IKeyFrame<TKey, TVal> ToKeyFrameT1();
        IKeyFrame<TKey, TVal> ToKeyFrameT2();
        IKeyFrame<TKey, TVal> ToKeyFrameT3();
        string ToString();
        string ToString(bool Brackets);
    }
    
    public struct KeyFrameT0<TKey, TVal> : IKeyFrame<TKey, TVal>
    {
        public TKey Frame;

        public IKeyFrame<TKey, TVal> ToKeyFrameT0() =>
            new KeyFrameT0<TKey, TVal> { Frame = Frame };
        public IKeyFrame<TKey, TVal> ToKeyFrameT1() =>
            new KeyFrameT1<TKey, TVal> { Frame = Frame };
        public IKeyFrame<TKey, TVal> ToKeyFrameT2() =>
            new KeyFrameT2<TKey, TVal> { Frame = Frame };
        public IKeyFrame<TKey, TVal> ToKeyFrameT3() =>
            new KeyFrameT3<TKey, TVal> { Frame = Frame };

        public IKeyFrame<TKey, TVal> Check() => this;

        public override string ToString() =>
            Extensions.ToString(Frame);
        public string ToString(bool Brackets) =>
            Extensions.ToString(Frame);
    }
    
    public struct KeyFrameT1<TKey, TVal> : IKeyFrame<TKey, TVal>
    {
        public TKey Frame;
        public TVal Value;

        public IKeyFrame<TKey, TVal> ToKeyFrameT0() =>
            new KeyFrameT0<TKey, TVal> { Frame = Frame };
        public IKeyFrame<TKey, TVal> ToKeyFrameT1() =>
            new KeyFrameT1<TKey, TVal> { Frame = Frame, Value = Value };
        public IKeyFrame<TKey, TVal> ToKeyFrameT2() =>
            new KeyFrameT2<TKey, TVal> { Frame = Frame, Value = Value };
        public IKeyFrame<TKey, TVal> ToKeyFrameT3() =>
            new KeyFrameT3<TKey, TVal> { Frame = Frame, Value = Value };

        public IKeyFrame<TKey, TVal> Check() =>
            Value.Equals(default(TVal)) ? ToKeyFrameT0() : (this);

        public override string ToString() => ToString(true);
        public string ToString(bool Brackets) =>
            (Brackets ? "(" : "") + Extensions.ToString(Frame) + "," +
                Extensions.ToString(Value) + (Brackets ? ")" : "");
    }
    
    public struct KeyFrameT2<TKey, TVal> : IKeyFrame<TKey, TVal>
    {
        public TKey Frame;
        public TVal Value;
        public TVal Interpolation;

        public IKeyFrame<TKey, TVal> ToKeyFrameT0() =>
            new KeyFrameT0<TKey, TVal> { Frame = Frame };
        public IKeyFrame<TKey, TVal> ToKeyFrameT1() =>
            new KeyFrameT1<TKey, TVal> { Frame = Frame, Value = Value };
        public IKeyFrame<TKey, TVal> ToKeyFrameT2() =>
            new KeyFrameT2<TKey, TVal> { Frame = Frame, Value = Value,
                Interpolation  = Interpolation};
        public IKeyFrame<TKey, TVal> ToKeyFrameT3() =>
            new KeyFrameT3<TKey, TVal> { Frame = Frame, Value = Value,
                Interpolation1 = Interpolation, Interpolation2 = Interpolation };

        public IKeyFrame<TKey, TVal> ToKeyFrameT3(IKeyFrame<TKey, TVal> Previous) =>
            Previous is KeyFrameT2<TKey, TVal> PreviousT2 ? 
            new KeyFrameT3<TKey, TVal> { Frame = Frame, Value = Value,
                Interpolation1 = PreviousT2.Interpolation, Interpolation2 = Interpolation } :
            new KeyFrameT3<TKey, TVal> { Frame = Frame, Value = Value,
                Interpolation1 =            Interpolation, Interpolation2 = Interpolation };

        public IKeyFrame<TKey, TVal> Check()
        {
                 if (Value.Equals(default(TVal)) && Interpolation.Equals(default(TVal))) return ToKeyFrameT0();
            else if (                               Interpolation.Equals(default(TVal))) return ToKeyFrameT1();
            return this;
        }

        public override string ToString() => ToString(true);
        public string ToString(bool Brackets) =>
            (Brackets ? "(" : "") + Extensions.ToString(Frame) + "," + Extensions.
            ToString(Value) + "," + Extensions.ToString(Interpolation) + (Brackets ? ")" : "");
    }
    
    public struct KeyFrameT3<TKey, TVal> : IKeyFrame<TKey, TVal>
    {
        public TKey Frame;
        public TVal Value;
        public TVal Interpolation1;
        public TVal Interpolation2;

        public IKeyFrame<TKey, TVal> ToKeyFrameT0() =>
            new KeyFrameT0<TKey, TVal> { Frame = Frame };
        public IKeyFrame<TKey, TVal> ToKeyFrameT1() =>
            new KeyFrameT1<TKey, TVal> { Frame = Frame, Value = Value };
        public IKeyFrame<TKey, TVal> ToKeyFrameT2() =>
            new KeyFrameT2<TKey, TVal> { Frame = Frame, Value = Value, Interpolation = Interpolation1 };
        public IKeyFrame<TKey, TVal> ToKeyFrameT3() =>
            new KeyFrameT3<TKey, TVal> { Frame = Frame, Value = Value,
                Interpolation1 = Interpolation1, Interpolation2 = Interpolation2 };

        public IKeyFrame<TKey, TVal> Check()
        {
                 if (Value.Equals(default(TVal)) && Interpolation1.Equals(default(TVal)) &&
            Interpolation2.Equals(default(TVal))) return ToKeyFrameT0();
            else if (Interpolation1.Equals(default(TVal)) &&
            Interpolation2.Equals(default(TVal))) return ToKeyFrameT1();
            else if (Interpolation1.Equals(Interpolation2))
                                                  return ToKeyFrameT2();
            return this;
        }

        public override string ToString() => ToString(true);
        public string ToString(bool Brackets) =>
            (Brackets ? "(" : "") + Extensions.ToString(Frame) + "," + Extensions.ToString(Value) + "," +
            Extensions.ToString(Interpolation1) + "," + Extensions.ToString(Interpolation2) + (Brackets ? ")" : "");

        public IKeyFrame<TKey, TVal> ToKeyFrameT2(IKeyFrame<TKey, TVal>
            Previous, out IKeyFrame<TKey, TVal> Current)
        {
            Current = Previous is KeyFrameT2<TKey, TVal> PreviousT2
                ? new KeyFrameT2<TKey, TVal> { Frame = PreviousT2.Frame,
                    Value = PreviousT2.Value, Interpolation = Interpolation1 }
                : new KeyFrameT2<TKey, TVal> { Frame = Frame,
                    Value = Value, Interpolation = Interpolation1 };
            return new KeyFrameT2<TKey, TVal> { Frame = Frame,
                Value = Value, Interpolation = Interpolation2 };
        }
    }
}
