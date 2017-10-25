using System;

namespace Ibd.Framework
{
    public class Singleton<T> where T : class, new()
    {
        private static readonly Lazy<T> Instancia = new Lazy<T>(() => new T());

        private Singleton()
        {
        }

        public static T Single
        {
            get { return Instancia.Value; }
        }
    }
}