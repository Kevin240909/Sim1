namespace lesson
{
    public class Program
    {
        /// <summary>
        /// The main entrypoint of your application.
        /// </summary>
        /// <param name="args">The arguments passed to the program</param>
        public static void Main(string[] args)
        {
            // Write some code here...
            int n = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Il perimetro del quadrato è " + n * 4);
            Console.WriteLine("L'area del quadrato è " + n * n);
        }
    }
}
