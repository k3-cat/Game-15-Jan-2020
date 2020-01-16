using System;
using System.Collections.Generic;
using System.Text;
using DynamicExpresso;

namespace 演绎_政权.Event {
    class Effect {
        readonly string name;
        public static Lambda Null;

        readonly uint m;
        readonly Lambda g;
        readonly Lambda p;
        readonly Lambda a;
        readonly Lambda h;

        readonly uint _t;
        readonly Lambda _g;
        readonly Lambda _p;
        readonly Lambda _a;
        readonly Lambda _h;

        public Effect(string name, uint m, uint _t, Dictionary<string, Lambda> d) {
            this.name = name;
            this.m = m;
            this._t = _t;

            g = (d.ContainsKey("g")) ? d["g"] : Null;
            p = (d.ContainsKey("p")) ? d["p"] : Null;
            a = (d.ContainsKey("a")) ? d["a"] : Null;
            h = (d.ContainsKey("h")) ? d["h"] : Null;

            if (_t > 0) {
                _g = (d.ContainsKey("_g")) ? d["_g"] : Null;
                _p = (d.ContainsKey("_p")) ? d["_p"] : Null;
                _a = (d.ContainsKey("_a")) ? d["_a"] : Null;
                _h = (d.ContainsKey("_h")) ? d["_h"] : Null;
            }
        }

        public void Apply(World.Country c, uint amount) {
            if (c.m < m) {
                //todo
                return;
            }
            c.m -= m;
            c.G += Convert.ToDecimal(g.Invoke(amount));
            c.P += Convert.ToDecimal(p.Invoke(amount));
            c.A += Convert.ToDecimal(a.Invoke(amount));
            c.H += Convert.ToDecimal(h.Invoke(amount));
            if (_t > 0) {
                GV.timeLine.Add(new TimedEffect(
                    c: c,
                    t: _t,
                    s: name,
                    g: Convert.ToDecimal(_g.Invoke(amount)),
                    p: Convert.ToDecimal(_p.Invoke(amount)),
                    a: Convert.ToDecimal(_a.Invoke(amount)),
                    h: Convert.ToDecimal(_h.Invoke(amount))
                    )
                );
            }
        }
    }
}
