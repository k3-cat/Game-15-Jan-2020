using System.Collections.Generic;

namespace 演绎_政权.Output {
    class TextLength {
        static readonly Dictionary<int, int> widths = new Dictionary<int, int>() {
            {126, 1}, {159, 0}, {687, 1}, {710, 0}, {711, 1}, {727, 0}, {733, 1},
            {879, 0}, {1154, 1}, {1161, 0}, {4347, 1}, {4447, 2}, {7467, 1}, {7521, 0},
            {8369, 1}, {8426, 0}, {9000, 1}, {9002, 2}, {11021, 1}, {12350, 2},
            {12351, 1}, {12438, 2}, {12442, 0}, {19893, 2}, {19967, 1}, {55203, 2},
            {63743, 1}, {64106, 2}, {65039, 1}, {65059, 0}, {65131, 2}, {65279, 1},
            {65376, 2}, {65500, 1}, {65510, 2}, {120831, 1}, {262141, 2}, {1114109, 1}
        };

        static int GetWidth(int o) {
            if (o == 0xe || o == 0xf) {
                return 0;
            }
            foreach (var i in widths) {
                if (o <= i.Key) {
                    return i.Value;
                }
            }
            return 1;
        }

        public static int Measure(string s) {
            var sc = 0;
            foreach (var c in s) {
                sc += GetWidth((int)c);
            }
            return sc;
        }
    }
}
