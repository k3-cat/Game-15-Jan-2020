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
            map = new List<Country>();
            GV.screen.ShowInfo($"地图版本: {l.GetVersionString()}");

            var i = 1;
            var camps = new List<string>();
            foreach (var p in l.profile) {
                camps.Add($"{i} - {p.head[0]}");
                i += 1;
                map.Add(new Country(
                    name: p.head[0],
                    g: Convert.ToDecimal(p.body["g"]),
                    p: Convert.ToDecimal(p.body["p"]),
                    a: Convert.ToDecimal(p.body["a"]),
                    h: Convert.ToDecimal(p.body["h"]),
                    taxRate: p.body["tR"],
                    isCountry: Convert.ToBoolean(p.head[1])
                    )
                );
            }
            GV.screen.ShowTable(camps, "Camps", 4);
            var id_ = Array.ConvertAll(GV.screen.Ask("请输入AI控制的国家编号: ").Split(',').Distinct().ToArray(),
                                       s => int.Parse(s) - 1);
            Array.Sort(id_);
            Array.Reverse(id_);
            var npc = new List<Country>();
            foreach (var id in id_) {
                if (id < 0) {
                    continue;
                }
                npc.Add(map[id]);
                map.RemoveAt(id);
            }
            Shuffle(map);
            Shuffle(npc);
            var j = (uint)map.Count + 1;
            foreach (var c in npc) {
                c.Id = j;
                j += 1;
            }
            j = 1u;
            foreach (var c in map) {
                c.Id = j;
                j += 1;
            }
            ai = new AI(npc);
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
                if (p > map.Count - 1) {
                    p = 0;
                    Turn();
                }
            }
            return map[p];
        }

        public Country Select(int id) {
            if (id > map.Count) {
                return ai.Select(id - map.Count);
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
            var g_bar = g / map.Count;
            var p_bar = p / map.Count;
            var a_bar = a / map.Count;
            ai.Auto(g_bar, p_bar, a_bar);
        }
    }
}
