using System;
using System.Collections.Generic;
using System.Text;

namespace 演绎_政权.World {
    class AI {
        readonly List<Country> npcM;

        public AI(List<Country> npc) {
            npcM = npc;
        }

        public void Auto(decimal g, decimal p, decimal a) {
            foreach (var npc in npcM) {
                npc.Update();
                npc.m = 0;
                if (npc.G < g) {
                    A(npc);
                } else if (npc.P < p) {
                    if (GV.rng.Next(99) < 50) {
                        B(npc);
                    } else {
                        X3(npc);
                    }
                } else if (npc.A < a) {
                    if (GV.rng.Next(99) < 50) {
                        C(npc);
                    } else {
                        X3(npc);
                    }
                } else {
                    if (GV.rng.Next(99) < 50) {
                        X1(npc);
                    } else {
                        X2(npc);
                    }
                }
            }
        }

        public Country Select(int id) {
            return npcM[id - 1];
        }

        void A(Country c) {
            c.G += 2;
            c.P += 1;
        }

        void B(Country c) {
            c.G -= 1;
            c.P -= 1;
            c.A += 1.5m;
        }

        void C(Country c) {
            c.G += 1;
            c.P += 2;
        }

        void X1(Country c) {
            c.G += 2;
            c.P += 1;
        }

        void X2(Country c) {
            c.P -= 1;
            c.G += 2;
            c.A += 1.5m;
        }

        void X3(Country c) {
            c.P += 3;
        }
    }
}
