using System;
using System.Collections.Generic;
using DynamicExpresso;

namespace 演绎_政权.Event {
    class Action {
        readonly Dictionary<string, Effect> list;
        readonly List<string> index;

        public Action(Profile.Loader l) {
            index = new List<string>();
            list = new Dictionary<string, Effect>();
            var version = l.GetVersionString();
            GV.screen.ShowInfo($"参数版本: {version}");
            Console.Title += $" | 参数版本: {version.Substring(0, 16)}";

            var interpreter = new Interpreter();
            Effect.Null = interpreter.Parse("0*x", new Parameter("x", typeof(uint)));
            foreach (var p in l.profile) {
                index.Add(p.head[0]);
                var rule = new Dictionary<string, Lambda>();
                foreach (var r in p.body) {
                    rule.Add(r.Key, interpreter.Parse(r.Value, new Parameter("x", typeof(uint))));
                }
                list.Add(p.head[0], new Effect(p.head[0], Convert.ToUInt32(p.head[1]), Convert.ToUInt32(p.head[2]), rule));
            }

            var menu = new List<string>() {
                "*#: 修改数值",
                "1%: 招义勇军",
                "2%: 修改税率",
                "3%: 开始战争",
                "4%: 结束战争",
                "5%: 建国",
                "6%: 亡国",
                "7%: 开始流亡",
                "8%: 结束流亡",
                "9%: 改国号"
            };
            var i = 1;
            foreach (var a in index) {
                menu.Add($"{i}: {a}");
                i += 1;
            }
            GV.screen.SetMenu(menu);
        }

        public void Phase(World.Country c, uint cat, string action, string parm) {
            if (cat == 1) {
                if (action == "G") {
                    c.G = Convert.ToDecimal(parm);
                } else if (action == "P") {
                    c.P = Convert.ToDecimal(parm);
                } else if (action == "A") {
                    c.A = Convert.ToDecimal(parm);
                } else if (action == "V") {
                    c.V = Convert.ToDecimal(parm);
                } else if (action == "H") {
                    c.H = Convert.ToDecimal(parm);
                }
            } else if (cat == 2) {
                if (action == "1") {
                    c.招义勇军(parm);
                } else if (action == "2") {
                    c.修改税率(parm);
                } else if (action == "3") {
                    c.开始战争();
                } else if (action == "4") {
                    c.结束战争();
                } else if (action == "5") {
                    c.建国(parm);
                } else if (action == "6") {
                    c.亡国(parm);
                } else if (action == "7") {
                    c.开始流亡();
                } else if (action == "8") {
                    c.结束流亡();
                } else if (action == "9") {
                    c.改国号(parm);
                }
            } else {
                var i = Convert.ToInt32(action);
                list[index[i-1]].Apply(c: c, Convert.ToUInt32(parm));
            }
        }
    }
}
