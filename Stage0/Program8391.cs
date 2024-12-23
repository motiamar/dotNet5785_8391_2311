namespace Stage0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            welcome8391();
            welcome3211();
            Console.ReadKey();
        }
        static partial void welcome3211();
        private static void welcome8391()
        {
            Console.Write("Enter your name: ");
            string a = Console.ReadLine()!;
            Console.Write(a);
            Console.WriteLine(" welcome to my first console application");
        }
    }
}
