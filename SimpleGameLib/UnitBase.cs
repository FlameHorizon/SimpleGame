namespace SimpleGameLib {
    public abstract class UnitBase {

        public string Name { get; set; }
        public int Hp { get; set; }

        /// <summary>
        /// Indicates strength of each attack.
        /// </summary>
        public int Attack { get; set; }

        /// <summary>
        /// Value represents trait which describe likehood of
        /// starting fight first.
        /// </summary>
        public int Initiative { get; set; }
        
        /// <summary>
        /// Indicates if Unit is still alive or not.
        /// </summary>
        public bool IsAlive { get => Hp > 0; }
        
        /// <summary>
        /// Value represents strength of each magic attack.
        /// </summary>
        public int Magic { get; set; }

        protected UnitBase(string name, int hp, int attack, 
            int initiative, int magic)
        {
            Name = name;
            Hp = hp;
            Initiative = initiative;
            Magic = magic;
            Attack = attack;
        }
    }
}