using FluentAssertions;

namespace StefanStolz.Toolbox.Tests
{
    [TestFixture]
    [TestOf(typeof(TaskExtensions))]
    public class TaskExtensionsTests
    {
        [Test]
        public void SafeFireForget()
        {
            int methodCallCount = 0;

            Func<Task> t = Target;

            t.SafeFireAndForget(_ => { Assert.Fail(); });

            methodCallCount.Should().Be(1);

            Task Target()
            {
                methodCallCount++;
                return Task.CompletedTask;
            }
        }

        [Test]
        public void CallErrorHandlerOnException()
        {
            int errorHandlerCallCount = 0;
            Exception? caughtException=null;

            Func<Task> t = Target;

            t.SafeFireAndForget(exception =>
            {
                errorHandlerCallCount++;
                caughtException = exception;
            });

            errorHandlerCallCount.Should().Be(1);
            caughtException.Should().NotBeNull();
            caughtException!.Message.Should().Be("Some Error");

            Task Target()
            {
                throw new Exception("Some Error");
            }
        }
    }
}