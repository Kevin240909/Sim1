namespace tests
{
    public class IOTests : IDisposable
    {
        private StringWriter _stdOutMock;

        private TextReader _stdIn;
        private TextWriter _stdOut;

        public IOTests()
        {
            this._stdIn = Console.In;
            this._stdOut = Console.Out;

            this._stdOutMock = new StringWriter();

            Console.SetOut(_stdOutMock);
        }

        /// <summary>
        /// Allows to test timeout behaviors
        /// </summary>
        /// <param name="timeoutMSec">The timeout in msecs</param>
        /// <param name="action">The function to call</param>
        /// <exception cref="TimeoutException">if timeout exceeded</exception>
        static void AssertCompletesBeforeTimeout(int timeoutMSec, Action action)
        {
            var task = Task.Run(action);
            var completedInTime = Task.WaitAll(new[] { task }, TimeSpan.FromMilliseconds(timeoutMSec));

            if (task.Exception != null)
            {
                if (task.Exception.InnerExceptions.Count == 1)
                {
                    throw task.Exception.InnerExceptions[0];
                }

                throw task.Exception;
            }

            if (!completedInTime)
            {
                throw new TimeoutException($"La soluzione non è ottimizzata (troppo lenta)");
            }
        }

        [Fact(DisplayName = "Menu (0,5 punti)")]
        public void TestMenu1()
        {
            // Test con scelta valida 'A'
            string input = "A\n";
            Console.SetIn(new StringReader(input));

            char result = lesson.Program.Menu();

            Assert.Equal('A', result);
            string output = _stdOutMock.ToString();
            Assert.Contains("A -> Esercizio 1", output);
            Assert.Contains("X -> Esci dal programma", output);
        }

        [Fact(DisplayName = "Menu 2 (0,5 punti)")]
        public void TestMenu2()
        {
            // Test con scelta non valida prima, poi valida
            string input = "Z\nB\n";
            Console.SetIn(new StringReader(input));

            char result = lesson.Program.Menu();

            Assert.Equal('B', result);
            string output = _stdOutMock.ToString();
            // Il menu deve essere mostrato almeno due volte (una per scelta invalida)
            int menuCount = System.Text.RegularExpressions.Regex.Matches(output, "A -> Esercizio 1").Count;
            Assert.True(menuCount >= 2);
        }

        [Fact(DisplayName = "Esercizio 1 - Test 1 (0,5 punti)", Timeout = 4000)]
        public void TestEsercizio1_1()
        {
            // Test base con numeri piccoli
            int[] numbers = { 1, 2, 3, 4, 5, 9, 16, 25 };
            int result = lesson.Program.Esercizio1(numbers);
            Assert.Equal(5, result); // 1, 4, 9, 16, 25 sono quadrati perfetti
        }

        [Fact(DisplayName = "Esercizio 1 - Test 2 (0,5 punti)")]
        public void TestEsercizio1_2()
        {
            // Test con array vuoto
            int[] numbers = { };
            int result = lesson.Program.Esercizio1(numbers);
            Assert.Equal(0, result);
        }

        [Fact(DisplayName = "Esercizio 1 - Test 3 (0,5 punti)")]
        public void TestEsercizio1_3()
        {
            // Test con numeri grandi per verificare l'ottimizzazione
            int[] numbers = { 10000, 10201, 15625, 20000, 40000 };

            AssertCompletesBeforeTimeout(2000, () =>
            {
                int result = lesson.Program.Esercizio1(numbers);
                Assert.Equal(2, result); // 10000 (100²), 10201 (101²), 15625 (125²)
            });
        }

        [Fact(DisplayName = "Esercizio 2 - Test 1 (0,25 punti)")]
        public void TestEsercizio2_1()
        {
            // Test base
            string[] values = { "Aldo", "Bruno", "Carlo" };
            int result = lesson.Program.Esercizio2(values, 'B');
            Assert.Equal(1, result);
        }

        [Fact(DisplayName = "Esercizio 2 - Test 2 (0,25 punti)")]
        public void TestEsercizio2_2()
        {
            // Test con carattere non presente
            string[] values = { "Aldo", "Bruno", "Carlo" };
            int result = lesson.Program.Esercizio2(values, 'D');
            Assert.Equal(-1, result);
        }

        [Fact(DisplayName = "Esercizio 2 - Test 3 (0,25 punti)")]
        public void TestEsercizio2_3()
        {
            // Test con primo elemento
            string[] values = { "Aldo", "Bruno", "Carlo" };
            int result = lesson.Program.Esercizio2(values, 'A');
            Assert.Equal(0, result);
        }

        [Fact(DisplayName = "Esercizio 2 - Test 4 (0,25 punti)")]
        public void TestEsercizio2_4()
        {
            // Test con ultimo elemento
            string[] values = { "Aldo", "Bruno", "Carlo", "Dario" };
            int result = lesson.Program.Esercizio2(values, 'D');
            Assert.Equal(3, result);
        }

        [Fact(DisplayName = "Esercizio 3 - Test 1 (0,25 punti)")]
        public void TestEsercizio3_1()
        {
            // Test con size 5
            Random rnd = new Random(12345);
            int[] result = lesson.Program.Esercizio3(rnd, 5);

            Assert.Equal(5, result.Length);
            // Verifica che tutti siano pari
            Assert.All(result, n => Assert.True(n % 2 == 0));
            // Verifica che siano distinti
            Assert.Equal(result.Length, result.Distinct().Count());
        }

        [Fact(DisplayName = "Esercizio 3 - Test 2 (0,25 punti)")]
        public void TestEsercizio3_2()
        {
            // Test con size 0
            Random rnd = new Random(54321);
            int[] result = lesson.Program.Esercizio3(rnd, 0);

            Assert.Empty(result);
        }

        [Fact(DisplayName = "Esercizio 3 - Test 3 (0,5 punti)")]
        public void TestEsercizio3_3()
        {
            // Test con size 10
            Random rnd = new Random(99999);
            int[] result = lesson.Program.Esercizio3(rnd, 10);

            Assert.Equal(10, result.Length);
            Assert.All(result, n => Assert.True(n % 2 == 0));
            Assert.Equal(result.Length, result.Distinct().Count());
        }

        [Fact(DisplayName = "Esercizio 3 - Test 4 (0,5 punti)")]
        public void TestEsercizio3_4()
        {
            // Test deterministico con seed fisso
            Random rnd = new Random(42);
            int[] result = lesson.Program.Esercizio3(rnd, 3);

            Assert.Equal(3, result.Length);
            Assert.All(result, n => Assert.True(n % 2 == 0));
            Assert.Equal(result.Length, result.Distinct().Count());
        }

        [Fact(DisplayName = "Esercizio 4 - Test 1 (0,5 punti)")]
        public void TestEsercizio4_1()
        {
            // Test base come nell'esempio
            float[] numbers = { 10, 20, 30 };
            float[] weights = { 1, 2, 3 };
            float result = lesson.Program.Esercizio4(numbers, weights);

            // Media pesata = (10*1 + 20*2 + 30*3) / (1+2+3) = 140/6 = 23.333334
            Assert.Equal(23.333334f, result, 5);
        }

        [Fact(DisplayName = "Esercizio 4 - Test 2 (0,5 punti)")]
        public void TestEsercizio4_2()
        {
            // Test con pesi uguali
            float[] numbers = { 5, 10, 15 };
            float[] weights = { 1, 1, 1 };
            float result = lesson.Program.Esercizio4(numbers, weights);

            // Media pesata = (5+10+15)/3 = 10
            Assert.Equal(10f, result, 5);
        }

        [Fact(DisplayName = "Esercizio 4 - Test 3 (0,5 punti)")]
        public void TestEsercizio4_3()
        {
            // Test con valori decimali
            float[] numbers = { 2.5f, 3.5f, 4.5f };
            float[] weights = { 2, 3, 5 };
            float result = lesson.Program.Esercizio4(numbers, weights);

            // Media pesata = (2.5*2 + 3.5*3 + 4.5*5) / (2+3+5) = 38/10 = 3.8
            Assert.Equal(3.8f, result, 5);
        }

        [Fact(DisplayName = "Esercizio 5 (0 punti)")]
        public void TestEsercizio5()
        {
            // Test come nell'esempio
            // fattoriali: 0!=1, 1!=1, 2!=2, 3!=6, 4!=24, ...
            int[] values = { 1, 0, 10, 9, 5 };
            // indici fattoriali < 5: 1, 1, 2 (quindi elementi agli indici 1, 1, 2)
            int[] result = lesson.Program.Esercizio5(values);
            int[] expected = { 0, 0, 10 };

            Assert.Equal(expected, result);
        }

        [Fact(DisplayName = "Esercizio 5 - Test 2 (0,5 punti)")]
        public void TestEsercizio5_2()
        {
            // Test con il secondo esempio fornito nel commento
            // fattoriali: 0!=1, 1!=1, 2!=2, 3!=6, 4!=24, ...
            int[] values2 = { 3, 10, 11, 9, 5, 13, 25, 87, 99, 1 };
            // indici fattoriali < 10: 1, 1, 2, 6 (quindi elementi agli indici 1, 1, 2, 6)
            int[] result = lesson.Program.Esercizio5(values2);
            int[] expected = { 10, 10, 11, 25 };

            Assert.Equal(expected, result);
        }

        [Fact(DisplayName = "Esercizio 5 - Test 3 (0,5 punti)")]
        public void TestEsercizio5_3()
        {
            // Test con array più grande
            int[] values = { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000, 1100, 1200, 1300, 1400, 1500, 1600, 1700, 1800, 1900, 2000, 2100, 2200, 2300, 2400, 2500 };
            // fattoriali < 25: 0!=1, 1!=1, 2!=2, 3!=6, 4!=24
            // quindi indici: 1, 1, 2, 6, 24
            int[] result = lesson.Program.Esercizio5(values);
            int[] expected = { 200, 200, 300, 700, 2500 };

            Assert.Equal(expected, result);
        }

        [Fact(DisplayName = "Esercizio 5 - Test 4 (0,5 punti)")]
        public void TestEsercizio5_4()
        {
            // Test con array piccolo (solo 3 elementi)
            int[] values = { 99, 88, 77 };
            // fattoriali < 3: 0!=1, 1!=1, 2!=2
            // quindi indici: 1, 1, 2
            int[] result = lesson.Program.Esercizio5(values);
            int[] expected = { 88, 88, 77 };

            Assert.Equal(expected, result);
        }

        public void Dispose()
        {
            Console.SetIn(this._stdIn);
            Console.SetOut(this._stdOut);

            this._stdOutMock.Dispose();
        }
    }
}