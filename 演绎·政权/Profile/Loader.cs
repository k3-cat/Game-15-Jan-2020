using System.Collections.Generic;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System;

namespace 演绎_政权.Profile{
    class Loader {
        readonly HashAlgorithm sha;
        public readonly List<Item> profile;

        public Loader(string path) {
            sha = new SHA256Managed();
            profile = new List<Item>();

            Item i = null;
            foreach (var line in File.ReadLines(path)) {
                var l = line.Trim();
                if (l.StartsWith('#') || l.Length == 0) {
                    continue;
                }
                var block = Encoding.UTF8.GetBytes(l);
                sha.TransformBlock(block, 0, block.Length, block, 0);
                if (l.Contains('|')) {
                    profile.Add(i);
                    i = new Item(l.Split('|'));
                } else {
                    var parm = l.Split(": ");
                    i.body.Add(parm[0], parm[1]);
                }
            }
            profile.Add(i);
            profile.RemoveAt(0);
            sha.TransformFinalBlock(Encoding.UTF8.GetBytes("\n"), 0, 1);
        }

        public string GetVersionString() {
            var result = BitConverter.ToString(sha.Hash).Replace("-", "");
            return result;
        }
    }
}
