using System;
using System.Collections.Generic;
using System.ComponentModel;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Exceptions;
using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Styling;

namespace WellFired.Guacamole.Views
{
    public abstract partial class ViewWithChildren : View, IHasChildren
    {
        protected ViewWithChildren()
        {
            Children = new List<ILayoutable>();
        }
        
        public override void Render(UIRect parentRect)
        {
            base.Render(parentRect);

            try
            {
                var resetToOriginContentRenderRect = FinalContentRenderRect;
                if (NativeRenderer.PushMaskStack(FinalRenderRect))
                {
                    resetToOriginContentRenderRect.X = 0;
                    resetToOriginContentRenderRect.Y = 0;
                }
    
                foreach (var child in Children)
                    (child as View)?.Render(resetToOriginContentRenderRect);
                
                NativeRenderer.PopMaskStack();
            }
            catch (Exception e)
            {
                if (e is GuacamoleUserFacingException) throw;
				
                var renderingException = new ViewRenderingException(GetType(), Id, e.Message, e.StackTrace);
                throw renderingException;
            }
        }

        public override void InvalidateRectRequest()
        {
            base.InvalidateRectRequest();

            foreach (var child in Children)
                (child as View)?.InvalidateRectRequest();
        }

        protected override void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(sender, e);

            if (e.PropertyName == ChildrenProperty.PropertyName || e.PropertyName == BindingContextProperty.PropertyName)
                SetupChildBindingContext();
        }

        private void SetupChildBindingContext()
        {
            // [DISCUSS] We currently don't support setting the bindingContext to null, though this is probably something we want to do in the future, maybe someone wants to unbind something?
            if (BindingContext == null)
                return;
            
            foreach (var child in Children)
            {
                var view = child as View;
                if (view != null && view.BindingContext == null)
                    view.BindingContext = BindingContext;
            }
        }

        public override void SetStyleDictionary(IStyleDictionary styleDictionary)
        {
            base.SetStyleDictionary(styleDictionary);

            foreach (var child in Children)
                (child as View)?.SetStyleDictionary(styleDictionary);
        }

        public override void ResetBindingContext(INotifyPropertyChanged newBindingContext)
        {
            base.ResetBindingContext(newBindingContext);

            foreach (var child in Children)
                (child as View)?.ResetBindingContext(newBindingContext);
        }
    }
}