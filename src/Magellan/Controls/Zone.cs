using System;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;
using System.Windows.Markup;

namespace Magellan.Controls
{
    /// <summary>
    /// Represents a zone used in a <see cref="Layout"/>.
    /// </summary>
    [ContentProperty("Content")]
    public class Zone : Freezable
    {
        /// <summary>
        /// Dependency property for the ZoneName property.
        /// </summary>
        public static readonly DependencyProperty ZonePlaceHolderNameProperty = DependencyProperty.Register("ZonePlaceHolderName", typeof(string), typeof(Zone), new UIPropertyMetadata(string.Empty, ZoneNameSet));

        /// <summary>
        /// Dependency property for the Content property.
        /// </summary>
        public static readonly DependencyProperty ContentProperty = DependencyProperty.Register("Content", typeof(object), typeof(Zone), new UIPropertyMetadata(null));

        /// <summary>
        /// Initializes a new instance of the <see cref="Zone"/> class.
        /// </summary>
        public Zone()
        {
        }

        /// <summary>
        /// Gets or sets the content that will be placed in the zone.
        /// </summary>
        /// <value>The content.</value>
        public object Content
        {
            get { return (object)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        /// <summary>
        /// Gets or sets the name of the zone.
        /// </summary>
        /// <value>The name of the zone.</value>
        public string ZonePlaceHolderName
        {
            get { return (string)GetValue(ZonePlaceHolderNameProperty); }
            set { SetValue(ZonePlaceHolderNameProperty, value); }
        }

        [DebuggerNonUserCode]
        protected override bool FreezeCore(bool isChecking)
        {
            if (string.IsNullOrEmpty(ZonePlaceHolderName))
            {
                throw new InvalidOperationException("The ZonePlaceHolderName property must be set.");
            }
            return base.FreezeCore(isChecking);
        }

        [DebuggerNonUserCode]
        private static void ZoneNameSet(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty((string)e.OldValue))
            {
                throw new InvalidOperationException("The ZonePlaceHolderName has already been set. Once set, it cannot be changed.");
            }
        }

        protected override Freezable CreateInstanceCore()
        {
            return new Zone();
        }
    }
}