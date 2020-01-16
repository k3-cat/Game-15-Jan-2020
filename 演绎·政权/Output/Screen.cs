using System;
using System.Collections.Generic;
using System.Text;

namespace 演绎_政权.Output {
    class Screen {
        StringBuilder state;
        StringBuilder timedEffect;
        string menu;

        public Screen() {
            Clean();
        }

        public void AddTE(string name, string s, uint t) {
            timedEffect.Append($"   {name}: {s}({t})");
            timedEffect.Append('\n');
        }

        public void AddS(World.Country c) {
            var pre = $"   {c.Id} {c.name}: ";
            var preLen = 24 - Output.TextLength.Measure(pre);
            var s = new StringBuilder(pre);
            while (preLen > 0) {
                s.Append('\t');
                preLen -= 8;
            }
            s.Append($"{c.G}\t{c.P}\t{c.A}\t{c.V}\t{c.H}");
            if (!c.isCountry) {
                s.Append("\t非国家");
            }
            if (c.inWar) {
                s.Append("\t战争");
            }
            if (c.isExile) {
                s.Append("\t逃亡");
            }
            state.Append(s.ToString());
            state.Append('\n');
        }

        public void Clean() {
            timedEffect = new StringBuilder();
            state = new StringBuilder("   编号 名称\t\tG\tP\tA\tV\tH\t其它状态\n" +
                "   - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -\n");
        }

        public void SetMenu(List<string> m) {
            menu = Table.Build(m, "MENU", 5);
        }

        public void Flush() {
            Console.Clear();
            Console.WriteLine();
            if (timedEffect.Length > 0) {
                Console.WriteLine("   = = = = = [ 延迟效果 ] = = = = = = = = = = = = = = = = = = = = = = = = = = = = =");
                Console.Write(timedEffect.ToString());
                Console.WriteLine();
            }
            Console.WriteLine("   = = = = = [ 各国状态 ] = = = = = = = = = = = = = = = = = = = = = = = = = = = = =");
            Console.Write(state.ToString());
            Console.Write(menu);
        }

        public string Ask(string s) {
            Console.WriteLine();
            Console.Write($"   {s}");
            return Console.ReadLine();
        }

        public void ShowInfo(string s) {
            Console.WriteLine($"      < {s} >");
        }

        public void ShowTable(List<string> list, string title, int maxCol) {
            Console.Write(Table.Build(list, title, maxCol));
        }
    }
}
