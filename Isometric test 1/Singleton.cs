

namespace Isometric_test_1
{
    // Singleton med et parameter der er GGeneric 'where' beskriver hvad den generiske type skal hvere , og skal kunne lave en ny instance adf klassen uden param.
    public abstract class Singleton<T> where T: Singleton<T>, new()
    {
        // Field
        private static T _instance;

        // Property  null operator
        public static T Instance => _instance ??= new T();

    }
}
