using System.ComponentModel;
using System.Linq;
using Magellan.Controls.Conventions.Editors;

namespace Magellan.Controls.Conventions
{
    /// <summary>
    /// A field convention that makes use of a set of <see cref="IEditorStrategy">editor strategies</see> to 
    /// choose an appropriate editor.
    /// </summary>
    public class DefaultFieldConvention : IFieldConvention
    {
        private readonly EditorStrategyCollection _editorStrategies;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultFieldConvention"/> class.
        /// </summary>
        /// <param name="editorStrategies">The editor strategies.</param>
        public DefaultFieldConvention(EditorStrategyCollection editorStrategies)
        {
            _editorStrategies = editorStrategies;
        }

        /// <summary>
        /// Configures the field using all of the information in the <paramref name="fieldInfo"/> parameter.
        /// </summary>
        /// <param name="fieldInfo">The field info.</param>
        public void Configure(FieldContext fieldInfo)
        {
            var displayName = fieldInfo.PropertyDescriptor.Attributes.OfType<DisplayNameAttribute>().FirstOrDefault();
            var desciption = fieldInfo.PropertyDescriptor.Attributes.OfType<DescriptionAttribute>().FirstOrDefault();

            fieldInfo.Field.InferredHeader = (displayName == null ? fieldInfo.PropertyName : displayName.DisplayName) + ":";
            fieldInfo.Field.InferredDescription = desciption == null ? null : desciption.Description;

            var editor = _editorStrategies.GetEditor(fieldInfo);
            if (fieldInfo.Field.Content == null)
            {
                fieldInfo.Field.Content = editor;
            }
        }
    }
}