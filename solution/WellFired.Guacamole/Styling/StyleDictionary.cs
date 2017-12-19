using System;
using System.Collections.Generic;
using WellFired.Guacamole.Diagnostics;

namespace WellFired.Guacamole.Styling
{
    public class StyleDictionary : IStyleDictionary
    {
        private readonly ILogger _logger;
        private readonly IDictionary<Type, Style> _dict = new Dictionary<Type, Style>();

        public StyleDictionary()
        {
            
        }

        public StyleDictionary(ILogger logger)
        {
            _logger = logger;
        }

        public StyleDictionary(ILogger logger, IDictionary<Type, Style> from)
        {
            _logger = logger;
            _dict = from;
        }

        /// <summary>
        /// Will add aStyle for a given view, will log a warning if constructed with a logger.
        /// </summary>
        /// <param name="aStyle">The Style we'd like to add to the dictionary.</param>
        /// <param name="forViewType">The view type that we would associate with this view type.</param>
        public void Add(Style aStyle, Type forViewType)
        {
            if(_dict.ContainsKey(forViewType))
                _logger?.LogWarning($"Style Dictionary already contains a style entry for {forViewType}");
            
            _dict[forViewType] = aStyle;
        }

        /// <summary>
        /// Will return the style for a given View Type.
        /// </summary>
        /// <param name="forViewType">The view type for which we'd like to find a style.</param>
        /// <returns>The style for the passed view type, it could possible be default(Style)</returns>
        public Style Get(Type forViewType)
        {
            Style style;
            _dict.TryGetValue(forViewType, out style);
            return style;
        }
    }
}