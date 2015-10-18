namespace FurnitureFactory.ConsoleClient
{
    using System;
    using System.Text;
    using Logic;

    public class ConsoleUserInterfaceIO : IUserInterfaceHandlerIO
    {
        public string GetInput()
        {
            return Console.ReadLine();
        }

        public void SetOutput(object obj)
        {
            Console.WriteLine(obj);
        }

        public string GetPassword()
        {
            StringBuilder builder = new StringBuilder();
            ConsoleKeyInfo symbol = Console.ReadKey(true);
            while (symbol.Key != ConsoleKey.Enter)
            {
                if (symbol.Key == ConsoleKey.Backspace)
                {
                    if (builder.Length > 0)
                    {
                        builder.Length--;
                        Console.CursorLeft--;
                    }

                    Console.Write(' ');
                    Console.CursorLeft--;
                }
                else
                {
                    builder.Append(symbol.KeyChar);
                    Console.Write('*');
                }

                symbol = Console.ReadKey(true);
            }

            Console.WriteLine();
            return builder.ToString();
        }
    }
}
