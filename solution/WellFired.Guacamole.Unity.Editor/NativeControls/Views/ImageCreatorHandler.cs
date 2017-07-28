using System;
using System.Threading.Tasks;
using UnityEngine;
using WellFired.Guacamole.Image;
using WellFired.Guacamole.Unity.Editor.Extensions;

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
                Device.ExecuteOnMainThread(() => { throw ex; });
                return null;
            }
        }
    }
}