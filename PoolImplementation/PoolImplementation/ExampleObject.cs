using System;
using System.Threading;

namespace PoolImplementation
{
    public class ExampleObject
    {
        public string RandomString;

        public void Run()
        {
            Random rd = new Random();
            int multiplicador = rd.Next(3, 5);
            int threadTime = 1000 * multiplicador;

            Console.WriteLine(" Objeto = " + this.GetHashCode() +
                              " Thread = " + Thread.CurrentThread.ManagedThreadId + " Running...");

            Thread.Sleep(threadTime);

            Console.WriteLine(" Tarefa finalizada Objeto = " + this.GetHashCode() +
                              " Thread = " + Thread.CurrentThread.ManagedThreadId + " Completed in "+ threadTime + " (ms) ");

        }

    }
}
