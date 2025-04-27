using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    internal class Dog : Pet
    {
        private bool isSleeping = false;
        private string name;
        private string breed;
        private bool isMale;
        private readonly string type = "Dog";
        public override string Type { get { return type; } }
        public override List<string> tricksLearnt { get; set; } = new List<string>();

        public override string Name { get { return name; } }
        public override string Breed { get { return breed; } }
        public override bool IsMale { get { return isMale; } }
        public override bool IsSleeping
        {
            get { return isSleeping; }
        }

        public Dog(string name, string breed, bool isMale)
        {
            this.name = char.ToUpper(name[0]) + name.Substring(1);
            this.breed = breed;
            this.isMale = isMale;
        }

        public override void Sleep()
        {
            if (isSleeping)
            {
                Console.WriteLine("The dog is already sleeping");
                return;
            }

            isSleeping = true;
            Console.WriteLine("Dog is sleeping.");
        }

        public override void Speak()
        {
            if (isSleeping)
            {
                Console.WriteLine("Dog is sleeping. Please wake it up first.");
                return;
            }

            Console.WriteLine("Woof! Woof!");
        }

        public override void Trick(string trick)
        {
            trick = trick.ToLower();
            if (isSleeping)
            {
                Console.WriteLine("Dog is sleeping. Please wake it up first.");
                return;
            }

            if (tricksLearnt.Count == 0)
            {
                Console.WriteLine("Dog doesn't know any tricks.");
                return;
            }

            if (tricksLearnt.Contains(trick))
            {
                Console.WriteLine("Dog is doing the trick: " + trick);
            }
            else
            {
                Console.WriteLine("Dog doesn't know this trick: " + trick);
                Console.WriteLine("Please teach it first.");
            }
            //Console.WriteLine("Dog is doing a trick.");
        }

        public override void LearnTrick(string trick)
        {
            if (isSleeping)
            {
                Console.WriteLine("Dog is sleeping. Please wake it up first.");
                return;
            }

            trick = trick.ToLower();

            if (tricksLearnt.Contains(trick))
            {
                Console.WriteLine("Dog already knows this trick: " + trick);
                return;
            }

            Console.WriteLine("Dog is learning trick: "+trick);
            Console.WriteLine("Please wait!");

            Thread.Sleep(5000);
            Console.WriteLine("Dog has learned the trick: " + trick);
            tricksLearnt.Add(trick);
        }

        public override void Wake()
        {
            if (!isSleeping)
            {
                Console.WriteLine("The dog is already awake!");
                return;
            }

            isSleeping = false;

        }
    }
}
