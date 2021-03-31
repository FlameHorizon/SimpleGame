using System.Collections.Generic;
using System.Linq;

namespace SimpleGameLib {
    public class FightQueue {
        private readonly Queue<UnitBase> queue;

        public FightQueue(IEnumerable<UnitBase> units)
        {
            queue = new Queue<UnitBase>(units);
        }

        public bool AllAlive()
        {
            return queue.All(unit => unit.IsAlive);
        }

        /// <summary>
        /// Returns unit which current has a turn without changing its position.
        /// </summary>
        /// <returns></returns>
        public UnitBase CurrentUnit()
        {
            return queue.Peek();
        }

        /// <summary>
        /// Returns unit which is next in queue without changing its position.
        /// </summary>
        /// <returns></returns>
        public UnitBase NextUnit()
        {
            return queue.ToArray()[1];
        }

        /// <summary>
        /// Moves queue up by one step.
        /// </summary>
        public void AdvanceQueue()
        {
            queue.Enqueue(queue.Dequeue());
        }

    }
}
