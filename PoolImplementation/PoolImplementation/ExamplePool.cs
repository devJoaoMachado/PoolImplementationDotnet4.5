using System;
using System.Linq;
using System.Threading;

namespace PoolImplementation
{
    /// <summary>
    /// Pool de gerenciamento de objetos
    /// </summary>
    public static class ExamplePool<T> where T : class, new()
    {
        //Tamanho do pool de objetos deve ser dimensionado de acordo com a necessidade
        private static ExampleEncapsulatedObject<T>[] _poolList = new ExampleEncapsulatedObject<T>[3];
        private static Object lockObj = new Object();

        /// <summary>
        /// Inicializa os objetos no Pool
        /// </summary>
        public static void Init()
        {
            for (int i = 0; i < _poolList.Count(); i++)
            {
                _poolList[i] = new ExampleEncapsulatedObject<T>();
                Console.WriteLine("Inicializando objeto no Pool: " + _poolList[i].ExObject.GetHashCode());
            }
        }

        /// <summary>
        /// Retorna um objeto do Pool ou cria um novo caso todos já estejam sendo utilizados.
        /// Consome o Pool de objetos de forma sequencial
        /// </summary>
        /// <returns></returns>
        public static T GetObject()
        {
            T retObject = null;

            int threadTimeout = 100;

            for (int i = 0; i < _poolList.Count(); i++)
            {
                if (Monitor.TryEnter(lockObj, threadTimeout))
                {
                    try
                    {
                        if (_poolList[i].InUse == false)
                        {
                            Console.WriteLine($"Pegou objeto {_poolList[i].ExObject.GetHashCode()} do Pool... ");
                            _poolList[i].InUse = true;
                            retObject = _poolList[i].ExObject;
                            break;
                        }
                        else if (i == _poolList.Count() - 1)
                        {
                            retObject = new T();
                            Console.WriteLine($"Todos objetos do Pool em uso, criou novo : {retObject.GetHashCode()}");
                        }
                    }
                    finally
                    {
                        Monitor.Exit(lockObj);
                    }
                }
                else if (i == _poolList.Count() - 1)
                {
                    retObject = new T();
                    Console.WriteLine($"Excedido limite de concorrência de Threads, criou novo : {retObject.GetHashCode()}");
                }
            }

            return retObject;
        }


        /// <summary>
        /// Libera objeto para uso
        /// </summary>
        /// <param name="exampleObj"></param>
        public static void FreeObject(ref T exampleObj)
        {
            foreach (var item in _poolList)
            {
                if (item.ExObject.GetHashCode() == exampleObj.GetHashCode())
                {
                    item.InUse = false;
                    Console.WriteLine("Liberou objeto... " + item.ExObject.GetHashCode());
                    break;
                }
            }
        }
    }

}
