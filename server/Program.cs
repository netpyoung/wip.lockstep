using System;
using Akka.Actor;
using Akka.IO;

namespace server
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().Run();
        }

        public void Run()
        {
            Console.WriteLine("a");

            var system = ActorSystem.Create("example");
            var manager = system.Tcp();
            Console.WriteLine("a");

        }
    }

    public class GameRoom : TypedActor
    {
        protected GameRoom()
        {
            Receive
        }
    }
}
