using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace FrontEnd.Controls
{
    public class TemplateSelector : DataTemplateSelector
    {
        public DataTemplate TemplateA { get; set; }
        public DataTemplate TemplateB { get; set; }
        public LayoutMode CurrentLayout { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            return (CurrentLayout == LayoutMode.Default)? TemplateA : TemplateB;
        }
    }

    public enum LayoutMode
    {
        Default,
        All
    }
}
