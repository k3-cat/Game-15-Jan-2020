using System;
using System.Collections.Generic;
using System.Linq;

namespace 演绎_政权.World {
    class Map {
        readonly List<Country> map;
        readonly AI ai;
        int p;

        public Map(Profile.Loader l) {
            p = 0;
            GV.screen.ShowInfo($"地图版本: {l.GetVersionString()}");

            var i = 1;
            var camps = new List<string>();
            var order = new List<int>();
            foreach (var p in l.profile) {
                camps.Add($"{i} - {p.head[0]}");
                order.Add(i-1);
                i += 1;
            }
            GV.screen.InitCampCount(camps.Count);
            GV.screen.ShowTable(camps, "Camps", 4);
            var s = GV.screen.Ask("请输入AI控制的国家编号: ").Trim();
            int[] selection;
            if (s.Length > 0) {
                selection = Array.ConvertAll(s.Split(',').Distinct().ToArray(), s => int.Parse(s));
            } else {
                selection = Array.Empty<int>();
            }
            Shuffle(order);
            map = new List<Country>();
            var npc = new List<Country>();
            i = 1;
            foreach (var j in order) {
                var box = (!selection.Contains(j)) ? map : npc;
                var p = l.profile[j];
                box.Add(new Country(
                            id: (uint)i,
                            name: p.head[0],
                            g: Convert.ToDecimal(p.body["g"]),
                            p: Convert.ToDecimal(p.body["p"]),
                            a: Convert.ToDecimal(p.body["a"]),
                            h: Convert.ToDecimal(p.body["h"]),
                            taxRate: p.body["tR"],
                            isCountry: Convert.ToBoolean(p.head[1])
                            ));
                i += 1;
            }

            ai = new AI(npc);
            GV.mapL = map.Count;
        }

        void Shuffle<T>(List<T> map) {
            var n = map.Count;
            while (n > 1) {
                n--;
                int k = GV.rng.Next(n + 1);
                var value = map[k];
                map[k] = map[n];
                map[n] = value;
            }
        }

        public Country Next() {
            if (map[p].m == 0) {
                p += 1;
                if (p > GV.mapL - 1) {
                    p = 0;
                    Turn();
                }
            }
            return map[p];
        }

        public Country Select(int id) {
            if (id > GV.mapL) {
                return ai.Select(id - GV.mapL);
            }
            return map[id - 1];
        }

        void Turn() {
            GV.screen.Clean();
            GV.timeLine.Update();
            AIAction();
            foreach (var c in map){
                c.Update();
            }
        }

        void AIAction() {
            decimal g = 0;
            decimal p = 0;
            decimal a = 0;
            foreach (var c in map) {
                g += c.G;
                p += c.P;
                a += c.A;
            }
            var g_bar = g / GV.mapL;
            var p_bar = p / GV.mapL;
            var a_bar = a / GV.mapL;
            ai.Auto(g_bar, p_bar, a_bar);
        }
    }
}
