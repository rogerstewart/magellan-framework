using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using Magellan.Controls.Conventions.Editors;

namespace Magellan.Controls.Conventions
{
    /// <summary>
    /// Contains information used by <see cref="IFieldConvention"/> and <see cref="IEditorStrategy"/> 
    /// implementations when they need to interrogate a field.
    /// </summary>
    public class FieldContext
    {
        private readonly Field _field;
        private readonly Binding _binding;
        private readonly object _sourceItem;
        private readonly string _propertyName;
        private readonly PropertyDescriptor _propertyDescriptor;

        /// <summary>
        /// Initializes a new instance of the <see cref="FieldContext"/> class.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <param name="binding">The binding.</param>
        /// <param name="sourceItem">The source item.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="propertyDescriptor">The property descriptor.</param>
        public FieldContext(Field field, Binding binding, object sourceItem, string propertyName, PropertyDescriptor propertyDescriptor)
        {
            _field = field;
            _binding = binding;
            _sourceItem = sourceItem;
            _propertyName = propertyName;
            _propertyDescriptor = propertyDescriptor;
        }

        /// <summary>
        /// Gets the UI field control.
        /// </summary>
        /// <value>The field.</value>
        public Field Field
        {
            get { return _field; }
        }

        /// <summary>
        /// Gets the binding that should be used for editing the UI field.
        /// </summary>
        /// <value>The binding.</value>
        public Binding Binding
        {
            get { return _binding; }
        }

        /// <summary>
        /// Gets the source item, the object which contains the property. For deep bindings, such as 
        /// <code>{Binding Path=Orders[0].LineItems[0].Address.Street1}</code>, the <see cref="SourceItem"/> 
        /// would be <code>Address</code>, and the <see cref="PropertyName"/> would be <code>Street1</code>.
        /// </summary>
        /// <value>The source item.</value>
        public object SourceItem
        {
            get { return _sourceItem; }
        }

        /// <summary>
        /// Gets the name of the property this field is bound to. For deep bindings, such as 
        /// <code>{Binding Path=Orders[0].LineItems[0].Address.Street1}</code>, the <see cref="SourceItem"/> 
        /// would be <code>Address</code>, and the <see cref="PropertyName"/> would be <code>Street1</code>.
        /// </summary>
        /// <value>The name of the property.</value>
        public string PropertyName
        {
            get { return _propertyName; }
        }

        /// <summary>
        /// Gets the property descriptor for the property represented by <see cref="PropertyName"/>. 
        /// </summary>
        /// <value>The property descriptor.</value>
        public PropertyDescriptor PropertyDescriptor
        {
            get { return _propertyDescriptor; }
        }

        public void WhenHasAttribute<TAttribute>(Action<TAttribute> hasAttributeCallback) where TAttribute : Attribute
        {
            var attributes = _propertyDescriptor.Attributes.OfType<TAttribute>().ToList();
            foreach (var att in attributes)
            {
                hasAttributeCallback(att);
            }
        }
    }
}
