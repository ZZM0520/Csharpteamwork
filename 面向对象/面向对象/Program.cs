using System;

namespace 面向对象
{
    class Program
    {
        static void Main(string[] args)
        {
            Animal dog = new Dog();
            Animal chicken = new Chicken();
            dog.speak();
            chicken.speak();
        }
    }
    abstract class Animal
    {
        public abstract void speak(); 
    }
    class Dog : Animal
    {
        override public void speak()
        {
            Console.WriteLine("Dog speak");
        }
    }
    class Chicken : Animal
    {
        override public void speak()
        {
            Console.WriteLine("Chicken speak");
        }
    }
}
