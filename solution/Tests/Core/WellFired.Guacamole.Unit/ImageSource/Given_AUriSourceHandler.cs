using System;
using NSubstitute;
using NUnit.Framework;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using NSubstitute.Core;
using NSubstitute.ExceptionExtensions;
using WellFired.Guacamole.Image;
using WellFired.Guacamole.WebRequestHandler;

namespace WellFired.Guacamole.Unit.ImageSource
{
    [TestFixture]
    public class Given_AUriSourceHandler
    {
        [Test]
        public void When_AnInValidPathIsPassedAnExceptionIsThrown()
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var webRequestHandler = Substitute.For<IWebRequestHandler>();
            webRequestHandler.GetStream(Arg.Any<Uri>(), cancellationTokenSource.Token).Throws<Exception>();
            
            var uriSourceHandler = new UriSourceHandler(new Uri("http://www.wellfired.com"), webRequestHandler);
            Assert.That(() => uriSourceHandler.Handle(cancellationTokenSource.Token).Result, Throws.Exception);
        }
        
        [Test]
        public void When_ThePathToTheStreamIsValid_TheStreamIsReturned()
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var webRequestHandler = Substitute.For<IWebRequestHandler>();
            webRequestHandler.GetStream(Arg.Any<Uri>(), cancellationTokenSource.Token).Returns(MockedData);
            
            var uriSourceHandler = new UriSourceHandler(new Uri("http://www.wellfired.com"), webRequestHandler);
            var wrapper = AsyncHelpers.RunSync(() => uriSourceHandler.Handle(cancellationTokenSource.Token));
            
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
            
            var webRequestHandler = Substitute.For<IWebRequestHandler>();
            webRequestHandler.GetStream(Arg.Any<Uri>(), cancellationTokenSource.Token).Returns(ReturnMockedDataAfterLongTime);
            
            var uriSourceHandler = new UriSourceHandler(new Uri("http://www.wellfired.com"), webRequestHandler);
            IImageSourceWrapper wrapper = null;
            
            Assert.That(() =>
            {
                AsyncHelpers.RunSync(() =>
                {
                    return TaskEx.WhenAll(
                        TaskEx.Run(async () => wrapper = await uriSourceHandler.Handle(cancellationTokenSource.Token), cancellationTokenSource.Token),
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
            var cancellationToken = (CancellationToken)arg[1];
            for(var index = 0; index <= 10; index++)
                await TaskEx.Delay(1, cancellationToken);
            
            return new MemoryStream(new byte[] { 23, 44 });
        }
    }
}