using System;
using System.Collections.Generic;
using System.Text;

namespace 演绎_政权.Event {
    struct TimedEffect {
        readonly World.Country c;
        uint t;
        readonly string s;
        readonly decimal g;
        readonly decimal p;
        readonly decimal a;
        readonly decimal h;

        public TimedEffect(World.Country c, uint t, string s, decimal g, decimal p, decimal a, decimal h) {
            this.c = c;
            this.t = t;
            this.s = s;
            this.g = g;
            this.p = p;
            this.a = a;
            this.h = h;
            GV.screen.AddTE(c.name, s, t);
        }

        public bool check() {
            if (t == 0) {
                c.G += g;
                c.P += p;
                c.A += a;
                c.H += h;
                return true;
            }
            t -= 1;
            GV.screen.AddTE(c.name, s, t);
            return false;
        }
    }
}
