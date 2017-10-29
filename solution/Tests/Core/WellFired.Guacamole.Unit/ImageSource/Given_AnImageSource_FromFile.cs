using System.IO;
using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using NSubstitute.Core;
using NUnit.Framework;
using WellFired.Guacamole.FileSystem;
using WellFired.Guacamole.Image;

namespace WellFired.Guacamole.Unit.ImageSource
{
    [TestFixture]
    public class GivenAnImageSourceFromFile
    {
        [Test]
        public void When_TheLoadIsInteruptedWithACancel_Then_TheChainIsCancelled()
        {
            var mockedFileSystem = Substitute.For<IFileSystem>();
            mockedFileSystem.GetStream(Arg.Any<string>(), Arg.Any<FileMode>(), Arg.Any<CancellationToken>()).Returns(ReturnMockedDataAfterLongTime);
            
            var source = Image.ImageSource.From("doge.jpg", mockedFileSystem);

            var image = default(LoadedImage);

            Assert.That(() =>
                {
                    AsyncHelpers.RunSync(() =>
                    {
                        return TaskEx.WhenAll(
                            TaskEx.Run(async () => image = await source.Load()),
                            TaskEx.Run(async () => await WaitAWhileThenCancelImageSource(source)));
                    });
                },
                Throws.InstanceOf<TaskCanceledException>());
            
            Assert.That(image, Is.Null);
        }
        
        [Test]
        public void When_TheLoadIsInteruptedWithACancelAfterLoadHasCompleted_Then_TheChainIsNotCancelled()
        {
            var mockedFileSystem = Substitute.For<IFileSystem>();
            mockedFileSystem.GetStream(Arg.Any<string>(), Arg.Any<FileMode>(), Arg.Any<CancellationToken>()).Returns(ReturnMockedDataQuickly);
            
            var source = Image.ImageSource.From("doge.jpg", mockedFileSystem);

            var image = default(LoadedImage);

            Assert.That(() =>
            {
                AsyncHelpers.RunSync(() => TaskEx.Run(async () => image = await source.Load()));
            }, Throws.Nothing);
            
            Assert.That(image, Is.Not.Null);
            Assert.That(image.Data.Length, Is.EqualTo(2));
            Assert.That(image.Data[0], Is.EqualTo(23));
            Assert.That(image.Data[1], Is.EqualTo(44));
        }

        private static async Task WaitAWhileThenCancelImageSource(IImageSource imageSource)
        {
            await TaskEx.Delay(10);
            imageSource.Cancel();
        }

        private static async Task<Stream> ReturnMockedDataAfterLongTime(CallInfo arg)
        {
            var cancellationToken = (CancellationToken)arg[2];
            await TaskEx.Delay(10000000, cancellationToken);
            return new MemoryStream(new byte[] { 23, 44 });
        }

        private static async Task<Stream> ReturnMockedDataQuickly(CallInfo arg)
        {
            var cancellationToken = (CancellationToken)arg[2];
            await TaskEx.Delay(1, cancellationToken);
            return new MemoryStream(new byte[] { 23, 44 });
        }
    }
}