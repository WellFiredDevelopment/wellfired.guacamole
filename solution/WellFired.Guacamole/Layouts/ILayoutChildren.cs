using System.Collections.Generic;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Layouts
{
    /// <summary>
    /// Layouting is a three step process.
    /// 1. We work out what size we want to be.
    /// 2. We attempt to fullfil our rect request, though this may not be possible.
    /// 3. We layout our children in the space that was available to us.
    /// Each of these steps are called by something and will be called in the correct order.
    /// </summary>
    public interface ILayoutChildren
    {
        /// <summary>
        /// 1. We work out what size we want to be.
        /// </summary>
        /// <param name="layoutables"></param>
        /// <param name="minSize"></param>
        /// <returns></returns>
        UIRect CalculateValidRectRequest(IEnumerable<ILayoutable> layoutables, UISize minSize);
        
        /// <summary>
        /// 2. We attempt to fullfil our rect request, though this may not be possible.
        /// </summary>
        /// <param name="children"></param>
        /// <param name="availableSpace"></param>
        /// <param name="containerPadding"></param>
        /// <param name="horizontalLayout"></param>
        /// <param name="verticalLayout"></param>
        void AttemptToFullfillRequests(IList<ILayoutable> children, UIRect availableSpace, UIPadding containerPadding, LayoutOptions horizontalLayout, LayoutOptions verticalLayout);
        
        /// <summary>
        /// 3. We layout our children in the space that was available to us.
        /// </summary>
        /// <param name="layoutables"></param>
        /// <param name="availableSpace"></param>
        /// <param name="containerPadding"></param>
        void Layout(IEnumerable<ILayoutable> layoutables, UIRect availableSpace, UIPadding containerPadding);
    }
}