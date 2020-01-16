using System;
using System.Collections.Generic;
using System.Text;

namespace 演绎_政权.Output {
    class Table {
        static string Banner(string s) {
            return $"\n    - - - ( {s} ) - - -\n";
        }

        public static string Build(List<string> list, string title, int maxCol) {
            var col = 1;
            var t = new StringBuilder();
            var colWidth = 142 / maxCol;
            t.Append(Banner(title));
            foreach (var line in list) {
                if (col == 1) {
                    t.Append("   ");
                }
                t.Append(line);
                if (col < maxCol) {
                    var i = (colWidth - TextLength.Measure(line));
                    while (i > 0) {
                        t.Append(' ');
                        i -= 1;
                    }
                    col += 1;
                } else {
                    t.Append('\n');
                    col = 1;
                }
            }
            t.Append('\n');
            return t.ToString().Replace("\n\n", "\n");
        }
    }
}
