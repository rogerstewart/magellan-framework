using System.Windows;
using System.Windows.Controls;
using Magellan.Controls.Conventions;

namespace Magellan.Controls
{
    /// <summary>
    /// A control that contains a collection of fields.
    /// </summary>
    [StyleTypedProperty(Property = "ItemContainerStyle", StyleTargetType = typeof(Form))]
    public class Form : ItemsControl
    {
        /// <summary>
        /// Dependency property for the FieldBuilder property.
        /// </summary>
        public static readonly DependencyProperty FieldBuilderProperty = DependencyProperty.RegisterAttached("FieldBuilder", typeof(IFieldConvention), typeof(Form), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));
        
        static Form()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Form), new FrameworkPropertyMetadata(typeof(Form)));
        }

        public static IFieldConvention GetFieldBuilder(DependencyObject obj)
        {
            return (IFieldConvention)obj.GetValue(FieldBuilderProperty);
        }

        public static void SetFieldBuilder(DependencyObject obj, IFieldConvention value)
        {
            obj.SetValue(FieldBuilderProperty, value);
        }
    }
}
