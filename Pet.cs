using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    internal abstract class Pet
    {
        //public abstract string[] tricksLearnt { get; set; }
        public abstract List<string> tricksLearnt { get; set; }
        public abstract bool IsSleeping { get; }
        public abstract string Type { get; }
        public abstract string Name { get; }
        public abstract bool IsMale { get; }
        public abstract string Breed { get; }

        public abstract void Speak();
        public abstract void Trick(string trick);
        public abstract void LearnTrick(string trick);
        public abstract void Sleep();
        public abstract void Wake();

    }
}
