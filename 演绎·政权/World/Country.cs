using System;

namespace 演绎_政权.World {
    class Country {
        public uint id;
        public string name;
        public uint m;
        decimal g;
        decimal p;
        decimal a;
        decimal h;
        decimal va;
        decimal taxRate;
        public bool isCountry;
        public bool isExile;
        public bool inWar;

        public Country(uint id, string name, decimal g, decimal p, decimal a, decimal h, string taxRate, bool isCountry) {
            this.id = id;
            this.name = name;
            G = g;
            P = p;
            A = a;
            H = h;
            V = 0;
            修改税率(taxRate);
            this.isCountry = isCountry;
            m = (isCountry) ? 3u : 2u;
            isExile = false;
            inWar = false;

            GV.screen.AddS(this);
        }

        public decimal G {
            get { return g; }
            set { g = SpRoundDown(value); }
        }

        public decimal P {
            get { return p; }
            set { p = SpRoundUp(value); }
        }

        public decimal A {
            get { return a; }
            set { a = SpRoundDown(value); }
        }

        public decimal V {
            get { return va; }
            set { va = SpRoundDown(value); }
        }

        public decimal H {
            get { return h; }
            set {
                if (value < 0) {
                    h = 0;
                } else if (value > 10) {
                    h = 10;
                } else {
                    h = SpRoundDown(value);
                }
            }
        }

        public void Update() {
            m = (isCountry) ? 3u : 2u;
            G += taxRate * P - 0.25m * A;
            P += 0.1m * P;

            GV.screen.AddS(this);
        }

        decimal SpRoundUp(decimal value) {
            var i = decimal.Truncate(value);
            var d = value - i;
            if (d == 0) {
                return i + 0.0m;
            }
            return i + ((d <= 0.5m) ? 0.5m : 1.0m);
        }

        decimal SpRoundDown(decimal value) {
            var i = decimal.Truncate(value);
            var d = value - i;
            return i + ((d < 0.5m) ? 0.0m : 0.5m);
        }

        public void 招义勇军(string amount_) {
            var amount = Convert.ToUInt32(amount_);
            if (!inWar) {
                //todo
                return;
            }
            m -= 1;
            P -= amount;
            V += amount;
        }

        public void 修改税率(string newRate) {
            m -= 1;
            var rate = newRate.Split('\\');
            taxRate = Convert.ToDecimal(rate[1]) / Convert.ToDecimal(rate[0]);
        }

        public void 开始战争() {
            inWar = true;
        }

        public void 结束战争() {
            inWar = false;
            V = 0;
        }

        public void 建国(string name) {
            if (name.Length > 0) {
                this.name = name;
            }
            isCountry = true;
        }

        public void 亡国(string name) {
            if (name.Length > 0) {
                this.name = name;
            }
            isCountry = false;
        }

        public void 开始流亡() {
            isExile = true;
        }

        public void 结束流亡() {
            isExile = false;
        }

        public void 改国号(string name) {
            this.name = name;
        }
    }
}
