using NSubstitute;
using NUnit.Framework;
using WellFired.Guacamole.FileSystem;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using NSubstitute.Core;
using WellFired.Guacamole.Image;

namespace WellFired.Guacamole.Unit.ImageSource
{
    [TestFixture]
    public class Given_AFileSourceHandler
    {
        [Test]
        public void When_AnInValidPathIsPassedAnExceptionIsThrown()
        {
            var normalFileSystem = new FileSystem.FileSystem();
            var fileSourceHandler = new FileSourceHandler("MyWorkingFile.png", normalFileSystem);
            var cancellationTokenSource = new CancellationTokenSource();
            Assert.That(() => fileSourceHandler.Handle(cancellationTokenSource.Token).Result, Throws.Exception);
        }
        
        [Test]
        public void When_ThePathToTheStreamIsValid_TheStreamIsReturned()
        {
            var cancellationTokenSource = new CancellationTokenSource();
            
            var mockedFileSystem = Substitute.For<IFileSystem>();
            mockedFileSystem.GetStream(Arg.Any<string>(), Arg.Any<FileMode>(), cancellationTokenSource.Token).Returns(MockedData);
            
            var fileSourceHandler = new FileSourceHandler("MyWorkingFile.png", mockedFileSystem);
            var wrapper = AsyncHelpers.RunSync(() => fileSourceHandler.Handle(cancellationTokenSource.Token));
            
            Assert.That(wrapper, Is.Not.Null);
            Assert.That(wrapper.Data, Is.Not.Null);
            Assert.That(wrapper.ImageType, Is.Not.Null);
            Assert.That(wrapper.Data.Length, Is.EqualTo(2));
            Assert.That(wrapper.Data[0], Is.EqualTo(23));
            Assert.That(wrapper.Data[1], Is.EqualTo(44));
        }
        
        [Test]
        public void When_TheLoadIsInteruptedWithACancel_Then_TheChainIsCancelled()
        {
            // Testing the cancellation Behaviour, we create a fake Task that waits 4ms before returning a fake stream
            // We also create a second task that waits 1ms and cancels the token.
            var cancellationTokenSource = new CancellationTokenSource();
            
            var mockedFileSystem = Substitute.For<IFileSystem>();
            mockedFileSystem.GetStream(Arg.Any<string>(), Arg.Any<FileMode>(), cancellationTokenSource.Token).Returns(ReturnMockedDataAfterLongTime);
            
            var fileSourceHandler = new FileSourceHandler("MyWorkingFile.png", mockedFileSystem);
            IImageSourceWrapper wrapper = null;
            
            Assert.That(() =>
            {
                AsyncHelpers.RunSync(() =>
                {
                    return TaskEx.WhenAll(
                        TaskEx.Run(async () => wrapper = await fileSourceHandler.Handle(cancellationTokenSource.Token), cancellationTokenSource.Token),
                        TaskEx.Run(async () => {
                            await TaskEx.Delay(1, cancellationTokenSource.Token);
                            cancellationTokenSource.Cancel();
                        }, cancellationTokenSource.Token)
                    );
                });
            }, Throws.InstanceOf<TaskCanceledException>());
            
            Assert.That(wrapper, Is.Null);
        }

        private static async Task<Stream> MockedData(CallInfo arg)
        {
            await TaskEx.Delay(1);
            return new MemoryStream(new byte[] { 23, 44 });
        }

        private static async Task<Stream> ReturnMockedDataAfterLongTime(CallInfo arg)
        {
            var cancellationToken = (CancellationToken)arg[2];
            await TaskEx.Delay(100, cancellationToken);
            return new MemoryStream(new byte[] { 23, 44 });
        }
    }
}