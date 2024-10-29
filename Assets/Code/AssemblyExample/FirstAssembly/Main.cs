public sealed class Test
{
    public char Example;
}
namespace Code
{
    public sealed class Test
    {
        public string Example;
    }
    namespace Roman
    {
        public sealed class Test
        {
            public int Example;
        }
    }
    namespace Roman.OTUS
    {
        public sealed class Test
        {
            public bool Example;
        }
    }
    
    public sealed class Main
    {
        private void NameMethod()
        {
            Test test = new Test();
           // test.Example = '1';
        }
    }
}
