using System;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;

namespace Magellan.Controls
{
    /// <summary>
    /// Represents a zone used in a <see cref="Layout"/>.
    /// </summary>
    public class Zone : ContentControl
    {
        /// <summary>
        /// Dependency property for the ZoneName property.
        /// </summary>
        public static readonly DependencyProperty ZonePlaceHolderNameProperty = DependencyProperty.Register("ZonePlaceHolderName", typeof(string), typeof(Zone), new UIPropertyMetadata(string.Empty, ZoneNameSet));

        /// <summary>
        /// Initializes the <see cref="Zone"/> class.
        /// </summary>
        static Zone()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Zone), new FrameworkPropertyMetadata(typeof(Zone)));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Zone"/> class.
        /// </summary>
        public Zone()
        {
            Initialized += ZoneInitialized;
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
        private static void ZoneNameSet(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty((string)e.OldValue))
            {
                throw new InvalidOperationException("The ZonePlaceHolderName has already been set. Once set, it cannot be changed.");
            }
        }

        [DebuggerNonUserCode]
        private void ZoneInitialized(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ZonePlaceHolderName))
            {
                throw new InvalidOperationException("The ZonePlaceHolderName property must be set.");
            }
        }
    }
}