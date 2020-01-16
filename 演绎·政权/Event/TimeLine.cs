using System;
using System.Collections.Generic;
using System.Text;

namespace 演绎_政权.Event {
    class TimeLine {
        readonly List<TimedEffect> events;

        public TimeLine() {
            events = new List<TimedEffect>();
        }

        public void Add(TimedEffect e) {
            events.Add(e);
        }

        public void Update() {
            foreach (var e in events) {
                if (e.check()) {
                    events.Remove(e);
                }
            }
        }
    }
}
