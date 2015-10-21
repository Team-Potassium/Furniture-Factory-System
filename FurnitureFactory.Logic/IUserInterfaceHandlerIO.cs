namespace FurnitureFactory.Utils
{
    public interface IUserInterfaceHandlerIO
    {
        string GetPassword();

        string GetInput();

        void SetOutput(object obj);
    }
}
