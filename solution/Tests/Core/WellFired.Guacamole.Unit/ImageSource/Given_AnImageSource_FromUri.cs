using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using NSubstitute.Core;
using NUnit.Framework;
using WellFired.Guacamole.Image;
using WellFired.Guacamole.WebRequestHandler;

namespace WellFired.Guacamole.Unit.ImageSource
{
    [TestFixture]
    public class GivenAnImageSourceFromUri
    {
        [Test]
        public void When_TheLoadIsInteruptedWithACancel_Then_TheChainIsCancelled()
        {
            var mockedWebRequestHandler = Substitute.For<IWebRequestHandler>();
            mockedWebRequestHandler.GetStream(Arg.Any<Uri>(), Arg.Any<CancellationToken>()).Returns(ReturnMockedDataAfterLongTime);
            
            var source = Image.ImageSource.From(new Uri("http://www.wellfired.com"), mockedWebRequestHandler);

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
            var mockedWebRequestHandler = Substitute.For<IWebRequestHandler>();
            mockedWebRequestHandler.GetStream(Arg.Any<Uri>(), Arg.Any<CancellationToken>()).Returns(ReturnMockedDataQuickly);
            
            var source = Image.ImageSource.From(new Uri("http://www.wellfired.com"), mockedWebRequestHandler);

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
            var cancellationToken = (CancellationToken)arg[1];
            await TaskEx.Delay(10000000, cancellationToken);
            return new MemoryStream(new byte[] { 23, 44 });
        }

        private static async Task<Stream> ReturnMockedDataQuickly(CallInfo arg)
        {
            var cancellationToken = (CancellationToken)arg[1];
            await TaskEx.Delay(1, cancellationToken);
            return new MemoryStream(new byte[] { 23, 44 });
        }
    }
}