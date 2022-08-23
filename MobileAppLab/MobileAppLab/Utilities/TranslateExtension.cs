using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;

namespace MobileAppLab.Utilities
{
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension<BindingBase>
    {
        public string Text { get; set; }
        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return ProvideValue(serviceProvider);
        }

        public BindingBase ProvideValue(IServiceProvider serviceProvider)
        {
            try
            {
                var binding = new Binding
                {
                    Mode = BindingMode.OneWay,
                    Path = $"[{Text}]",
                    Source = LocalizationResourceManager.Instance,
                };
                return binding;
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }
    }
}
