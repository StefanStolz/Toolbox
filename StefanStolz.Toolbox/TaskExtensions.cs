namespace StefanStolz.Toolbox;

public static class TaskExtensions
{
    public static async void SafeFireAndForget(this Func<Task> task, Action<Exception> onException)
    {
        try
        {
            await task();
        }
        catch (Exception exception)
        {
            onException(exception);
        }
    }
}