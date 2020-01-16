using System;
using System.Collections.Generic;

namespace 演绎_政权.World {
    class Map {
        readonly List<Country> m;
        readonly AI ai;
        int p;

        public Map(Profile.Loader l) {
            m = new List<Country>();
            GV.screen.ShowInfo($"地图版本: {l.GetVersionString()}");

            var i = 1;
            var camps = new List<string>();
            foreach (var p in l.profile) {
                camps.Add($"{i} - {p.head[0]}");
                m.Add(new Country(
                    name: p.head[0],
                    g: Convert.ToDecimal(p.body["g"]),
                    p: Convert.ToDecimal(p.body["p"]),
                    a: Convert.ToDecimal(p.body["a"]),
                    h: Convert.ToDecimal(p.body["h"]),
                    taxRate: p.body["tR"],
                    isCountry: Convert.ToBoolean(p.head[1])
                    )
                );
                i += 1;
            }
            GV.screen.ShowTable(camps, "Camps", 4);
            var id_ = Array.ConvertAll(GV.screen.Ask("请输入AI控制的国家编号: ").Split(','),
                                       s => int.Parse(s) - 1);
            Array.Sort(id_);
            Array.Reverse(id_);
            var npc = new List<Country>();
            foreach (var id in id_) {
                npc.Add(m[id]);
                m.RemoveAt(id);
            }
            Shuffle();
            var j = 1u;
            foreach (var c in m) {
                c.id = j;
                j += 1;
            }
            foreach (var c in npc) {
                c.id = j;
                j += 1;
            }
            ai = new AI(npc);
            p = m.Count - 1;
        }

        void Shuffle() {
            var n = m.Count;
            while (n > 1) {
                n--;
                int k = GV.rng.Next(n + 1);
                var value = m[k];
                m[k] = m[n];
                m[n] = value;
            }
        }

        public Country Next() {
            if (p >= m.Count - 1) {
                p = 0;
                Turn();
                return m[0];
            }
            if (m[p].m == 0) {
                p += 1;
            }
            return m[p];
        }

        public Country Select(int id) {
            if (id > m.Count) {
                return ai.Select(id - m.Count);
            }
            return m[id - 1];
        }

        void Turn() {
            GV.screen.Clean();
            GV.timeLine.Update();
            AIAction();
            foreach (var c in m){
                c.Update();
            }
        }

        void AIAction() {
            decimal g = 0;
            decimal p = 0;
            decimal a = 0;
            foreach (var c in m) {
                g += c.G;
                p += c.P;
                a += c.A;
            }
            var g_bar = g / m.Count;
            var p_bar = p / m.Count;
            var a_bar = a / m.Count;
            ai.Auto(g_bar, p_bar, a_bar);
        }
    }
}
