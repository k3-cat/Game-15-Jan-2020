using System;
using System.Collections.Generic;
using System.Text;

namespace 演绎_政权.Profile {
    class Item {
        public readonly string[] head;
        public readonly Dictionary<string, string> body;

        public Item(string[] h) {
            head = h;
            body = new Dictionary<string, string>();
        }
    }
}
