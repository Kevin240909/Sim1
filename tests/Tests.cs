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

        [Fact(DisplayName="Example - INPUT: 10")]
        public void TestExample()
        {
            const string input = "10";
            const string expectedOutput = "Il perimetro del quadrato è 40\nL'area del quadrato è 100\n";

            this.IOTest(input, expectedOutput);
        }

        [Fact(DisplayName = "Case 1 - INPUT: 20")]
        public void Test1()
        {
            const string input = "20";
            const string expectedOutput = "Il perimetro del quadrato è 80\nL'area del quadrato è 400\n";

            this.IOTest(input, expectedOutput);
        }

        [Fact(DisplayName = "Case 2 - INPUT: 150")]
        public void Test2()
        {
            const string input = "150";
            const string expectedOutput = "Il perimetro del quadrato è 600\nL'area del quadrato è 22500\n";

            this.IOTest(input, expectedOutput);
        }

        [Fact(DisplayName = "Case 3 - INPUT: 100")]
        public void Test3()
        {
            const string input = "100";
            const string expectedOutput = "Il perimetro del quadrato è 400\nL'area del quadrato è 10000\n";

            this.IOTest(input, expectedOutput);
        }

        [Fact(DisplayName = "Case 4 - INPUT: 1234")]
        public void Test4()
        {
            const string input = "1234";
            const string expectedOutput = "Il perimetro del quadrato è 4936\nL'area del quadrato è 1522756\n";

            this.IOTest(input, expectedOutput);
        }

        [Fact(DisplayName = "Case 5 - INPUT: 1111")]
        public void Test5()
        {
            const string input = "1111";
            const string expectedOutput = "Il perimetro del quadrato è 4444\nL'area del quadrato è 1234321\n";

            this.IOTest(input, expectedOutput);
        }

        private void IOTest(string input, string expectedOutput)
        {
            // Mock input
            StringReader stdInMock = new StringReader(input);
            Console.SetIn(stdInMock);

            string[] args = { "" };
            lesson.Program.Main(args);
            this._stdOutMock.Flush();

            var actualOutputLines = this._stdOutMock.ToString().Trim();
            var parsedExpectedOutput = expectedOutput.ReplaceLineEndings().Trim();

            Assert.Contains(actualOutputLines, parsedExpectedOutput);
        }

        public void Dispose()
        {
            Console.SetIn(this._stdIn);
            Console.SetOut(this._stdOut);

            this._stdOutMock.Dispose();
        }
    }
}