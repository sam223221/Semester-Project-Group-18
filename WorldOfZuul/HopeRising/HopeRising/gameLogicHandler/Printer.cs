namespace HopeRising
{
    class Printer
    {
        public static void Print(string text, int speed = 25)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                System.Threading.Thread.Sleep(speed);
            }
            
        }

        public static void PrintLine(string text, int speed = 25)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                System.Threading.Thread.Sleep(speed);
            }
            Console.WriteLine();
            
        }
    }
}