using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Magellan.Controls
{
    public class TextBoxBehavior
    {
        public static readonly DependencyProperty InputPromptProperty = DependencyProperty.RegisterAttached("InputPrompt", typeof(string), typeof(TextBoxBehavior), new UIPropertyMetadata(null));

        public static string GetInputPrompt(DependencyObject obj)
        {
            return (string)obj.GetValue(InputPromptProperty);
        }

        public static void SetInputPrompt(DependencyObject obj, string value)
        {
            obj.SetValue(InputPromptProperty, value);
        }

        // Using a DependencyProperty as the backing store for InputPrompt.  This enables animation, styling, binding, etc...
        

    }
}
