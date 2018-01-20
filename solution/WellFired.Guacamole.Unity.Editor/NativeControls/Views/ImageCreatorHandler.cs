using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using WellFired.Guacamole.Image;
using WellFired.Guacamole.Platforms;
using WellFired.Guacamole.Unity.Editor.Extensions;
using Logger = WellFired.Guacamole.Unity.Editor.Diagnostics.Logger;

namespace WellFired.Guacamole.Unity.Editor.NativeControls.Views
{
    internal class ImageCreatorHandler
    {
        public async Task<Texture2D> UpdatedImageSource(IImageSource imageSource)
        {
            if (imageSource == null)
                return null;
            
            try
            {
                var data = await imageSource.Load();
                return await imageSource.ToUnityTexture(data);
            }
            catch (Exception ex)
            {
                try
                {
                    Logger.UnityLogger.LogMessage($"Failed to load Image {imageSource} Loaded default instead. Exception was {ex}");
                    var data = await ImageShapeDefinition.DefaultHandler.Handle(new CancellationToken());
                    return await imageSource.ToUnityTexture(LoadedImage.From(data));
                }
                catch (Exception e)
                {
                    MainThreadRunner.ExecuteOnMainThread(() => { throw e; });
                    return default(Texture2D);
                }
            }
        }
    }
}