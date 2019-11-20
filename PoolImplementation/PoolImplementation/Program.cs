using System;
using System.Threading;

namespace PoolImplementation
{
    class Program
    {
        delegate void RunAsync();

        static void Main(string[] args)
        {
            ExamplePool<ExampleObject>.Init();
            
            for(int i = 0; i < 10; i++)
            {
                Console.WriteLine($"Chamada {i} => ()");
                RunAsync runAsync = new RunAsync(Execute);
                runAsync.BeginInvoke(null, null);
                
                Thread.Sleep(1500);
            }
            
            Console.ReadKey();
        }

        private static void Execute()
        {
            //Pega objeto do Pool
            ExampleObject objExample = ExamplePool<ExampleObject>.GetObject();
            //Utiliza objeto
            objExample.Run();
            //Devolve objeto para o Pool
            ExamplePool<ExampleObject>.FreeObject(ref objExample);
        }
    }
}
