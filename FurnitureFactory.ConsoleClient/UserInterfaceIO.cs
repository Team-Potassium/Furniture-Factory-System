namespace FurnitureFactory.ConsoleClient
{
    using System;
    using Logic;

    public class UserInterfaceIO : IUserInterfaceHandlerIO
    {
        public string GetInput()
        {
            return Console.ReadLine();
        }

        public void SetOutput(object obj)
        {
           Console.WriteLine(obj);
        }
    }
}
