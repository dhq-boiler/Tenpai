using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Tenpai.Helpers
{
    [ContentProperty("Items")]
    public class MeldItemTemplateSelector : DataTemplateSelector
    {
        public List<DataTemplate> Items { get; set; }

        public MeldItemTemplateSelector()
        {
            Items = new List<DataTemplate>();
        }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item == null)
                return null;

            var template = Items.Find(s => item.GetType().Equals(s.DataType));
            if (template != null) return template;

            return base.SelectTemplate(item, container);
        }
    }
}
