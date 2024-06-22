using System.Runtime.CompilerServices;

namespace StefanStolz.Toolbox;

public static class TaskExtensions
{
    public static async void SafeFireAndForget(this Task task, Action<Exception> onException)
    {
        try
        {
            await task;
        }
        catch (Exception exception)
        {
            onException(exception);
        }
    }
}