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
        /// <param name="layoutables">The things we are going to calculate the size on.</param>
        /// <param name="minSize">The minimum total size that these children can take up.</param>
        /// <returns></returns>
        UIRect CalculateValidRectRequest(IEnumerable<ILayoutable> layoutables, UISize minSize);

        /// <summary>
        /// 2. We attempt to fullfil our rect request, though this may not be possible.
        /// </summary>
        /// <param name="children">The Children that we will layout.</param>
        /// <param name="availableSpace">The space that is available for these children to be layouted in.</param>
        /// <param name="containerPadding">The parents padding.</param>
        /// <param name="horizontalLayout"></param>
        /// <param name="verticalLayout"></param>
        void AttemptToFullfillRequests(ICollection<ILayoutable> children, UIRect availableSpace, UIPadding containerPadding, LayoutOptions horizontalLayout, LayoutOptions verticalLayout);

        /// <summary>
        /// 3. We layout our children in the space that was available to us.
        /// </summary>
        /// <param name="layoutables">The layoutables that we will layout.</param>
        /// <param name="availableSpace">The space available to these objects. Please be aware that this may have 
        ///     changed since AttemptToFullfillRequests was called.</param>
        /// <param name="containerPadding">The parents padding.</param>
        void Layout(ICollection<ILayoutable> layoutables, UIRect availableSpace, UIPadding containerPadding);
    }
}