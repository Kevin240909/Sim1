using System.Runtime.ExceptionServices;

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
            try
            {
                char choice = Menu();

                // modificare per fare delle prove
                int[] numbers = { 1, 2, 3, 4, 5 };
                int[] result;
                string[] values = { "Aldo", "Bruno", "Carlo" };

                switch (choice)
                {
                    case 'A':
                        int squareCount = Esercizio1(numbers);
                        Console.WriteLine(squareCount);
                        break;
                    case 'B':
                        // modificare per fare delle prove
                        char prefix = 'B';

                        int firstIndex = Esercizio2(values, prefix);
                        Console.WriteLine(firstIndex);
                        break;
                    case 'C':
                        // modificare per fare delle prove
                        Random rnd = new Random();
                        int size = 5;

                        int[] generatedNumbers = Esercizio3(rnd, size);
                        Console.WriteLine(String.Join(',', generatedNumbers));
                        break;
                    case 'D':
                        // esempio per fare delle prove
                        float[] floatNumbers = { 10, 20, 30 };
                        float[] weights = { 1, 2, 3 };
                        float weightedAverage = Esercizio4(floatNumbers, weights);
                        Console.WriteLine(weightedAverage);
                        break;
                    case 'E':
                        Console.WriteLine($"Array sorgente: ${String.Join(',', numbers)}");
                        result = Esercizio5(numbers);
                        Console.WriteLine($"Array risultato: ${String.Join(',', result)}");
                        break;
                    case 'X':
                        Console.WriteLine("Programma terminato");
                        break;
                    default:
                        throw new Exception("Scelta non valida");
                }

            }
            catch (NotImplementedException ex)
            {
                Console.WriteLine($"ATTENZIONE: {ex.TargetSite!.Name} non implementato");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ATTENZIONE: {ex.Message}");
            }
        }


        /// <summary>
        /// (1 punti) Crea un menù con le seguenti voci:
        /// A -> Esercizio 1
        /// B -> Esercizio 2
        /// C -> Esercizio 3
        /// D -> Esercizio 4
        /// E -> Esercizio 5
        /// X -> Esci dal programma
        /// </summary>
        /// <returns>
        /// La voce scelta, se corretta.
        /// Se la scelta non è valida, riproporre nuovamente
        /// il menù finché non si sceglie correttamente.
        /// </returns>
        public static char Menu()
        {
            char choice;
            do
            {
                Console.WriteLine("Menu:");
                Console.WriteLine("A -> Esercizio 1");
                Console.WriteLine("B -> Esercizio 2");
                Console.WriteLine("C -> Esercizio 3");
                Console.WriteLine("D -> Esercizio 4");
                Console.WriteLine("E -> Esercizio 5");
                Console.WriteLine("X -> Esci dal programma");
                Console.Write("Scelta: ");
                choice = Char.ToUpper(Console.ReadKey().KeyChar);
            } while (choice != 'X' && choice !='A' && choice!='B' && choice != 'D' && choice != 'E' && choice != 'C');

            return choice;
        }

        /// <summary>
        /// (1,5 punti) Restituisce il numero di numeri quadrati all'interno dell'array passato come parametro.
        /// Si noti che la soluzione deve essere ottimizzata o i test non passeranno. Un numero quadrato è un numero intero che può essere
        /// espresso come il quadrato di un altro numero intero (ad esempio: 1, 4, 9, 16, 25, ...).
        /// </summary>
        /// <param name="numbers">L'array di numeri da considerare per il conteggio</param>
        /// <returns>
        /// Il conteggio dei numeri quadrati presenti nell'array.
        /// </returns>
        /// <example>
        /// int[] numbers = { 1, 2, 5, 10, 25 };
        /// 
        /// Console.WriteLine(Esercizio1(numbers)); // 2
        /// </example>
        public static int Esercizio1(int[] numbers)
        {
            int cont=0;
            foreach (int n in numbers)
            {
                if(Math.Sqrt(n) % 1 == 0)
                {
                    cont++;
                }
            }
            return cont;
        }

        /// <summary>
        /// (1 punto) Cerca all'interno dell'array di stringhe ordinato passato come parametro, la prima stringa
        /// che cominci con la lettera specificata.
        /// </summary>
        /// <param name="values">L'array di valori in cui ricercare</param>
        /// <param name="prefix">La lettera iniziale da ricercare</param>
        /// <returns>
        /// L'indice della prima stringa che comincia con la lettera specificata.
        /// </returns>
        /// <example>
        /// string[] values = { "Aldo", "Bruno", "Carlo" };
        /// 
        /// Console.WriteLine(Esercizio2(values, 'B')); // 1
        /// Console.WriteLine(Esercizio2(values, 'C')); // 2
        /// Console.WriteLine(Esercizio2(values, 'D')); // -1
        /// </example>
        public static int Esercizio2(string[] values, char prefix)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// (1,5 punti) Carica un array di numeri interi casuali, distinti e pari, del numero di
        /// elementi specificato.
        /// </summary>
        /// <param name="rnd">L'oggetto Random da utilizzare per la generazione dei numeri</param>
        /// <param name="size">La dimensione dell'array da generare</param>
        /// <returns>
        /// L'array di numeri generato
        /// </returns>
        /// <example>
        /// int size = 5;
        /// 
        /// int[] result = Esercizio3(size);
        /// Console.WriteLine(String.Join(',', result)); // ad esempio: 12,4,8,16,2
        /// </example>
        public static int[] Esercizio3(Random rnd, int size)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// (1,5 punti) Restituisce la media pesata di 2 array di numeri, uno corrispondente ai valori e l'altro ai pesi.
        /// </summary>
        /// <param name="numbers">L'array di numeri da considerare per il calcolo della media</param>
        /// <param name="weights">L'array di pesi corrispondente ai numeri</param>
        /// <returns>
        /// La media pesata calcolata
        /// </returns>
        /// <example>
        /// float[] numbers = { 10, 20, 30 };
        /// float[] weights = { 1, 2, 3 };
        /// 
        /// Console.WriteLine(Esercizio4(numbers, weights)); // 23.333334
        /// </example>
        public static float Esercizio4(float[] numbers, float[] weights)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// (1,5 punti) Dato un array passato come parametro, restituire gli elementi aventi 
        /// indice corrispondente a tutti i numeri corrispondenti a un numero ottenibile tramite fattoriale.
        /// </summary>
        /// <param name="values">L'array di elementi da campionare</param>
        /// <example>
        /// int[] values = { 1, 0, 10, 9, 5 };
        ///    // indici =   0, 1, 2, 3, 4
        ///    // fattoriali = 0!,1!,2!,3!,4!,... = 1,1,2 ( mi fermo appena supero la lunghezza dell'array )
        /// int[] result = Esercizio5(values);
        /// Console.WriteLine(String.Join(',', result)); // 0,0,10,9
        /// int[] values2 = { 3, 10, 11, 9, 5, 13, 25, 87, 99, 1 };
        ///    // indici =    0,  1,  2, 3, 4,  5,  6,  7,  8, 9
        ///    // fattoriali = 0!,1!,2!,3!,4!,5!,... = 1,1,2,6 ( mi fermo appena supero la lunghezza dell'array )
        /// result = Esercizio5(values2);
        /// Console.WriteLine(String.Join(',', result)); // 10,10,11,25
        /// </example>
        public static int[] Esercizio5(int[] values)
        {
            throw new NotImplementedException();
        }
    }
}