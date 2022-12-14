using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileAppLab.Views
{
    /// <summary>
    /// View của overlay loading trong màn hình login
    /// </summary>
    /// <Modified>
    /// Name Date Comments
    /// annv3 29/08/2022 created
    /// </Modified>
    /// <seealso cref="Xamarin.Forms.ContentView" />
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoadingView : ContentView
    {
        public static readonly BindableProperty IsLoadingProperty = BindableProperty.Create(nameof(IsLoading), typeof(bool), typeof(LoadingView), default(bool), propertyChanged: HandleIsRunningPropertyChanged);
        public static readonly BindableProperty LoadingIndicatorColorProperty = BindableProperty.Create(nameof(LoadingIndicatorColor), typeof(Color), typeof(LoadingView), default(Color), propertyChanged: HandleSpinnerColorPropertyChanged);
        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(LoadingView), default(Color), propertyChanged: HandleTextFontColorPropertyChanged);
        public static readonly BindableProperty TextFontSizeProperty = BindableProperty.Create(nameof(TextFontSize), typeof(double), typeof(LoadingView), default(double), propertyChanged: HandleTextFontSizePropertyChanged);
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(LoadingView), default(string), propertyChanged: HandleTextPropertyChanged);
        private const double FullyOpaque = 1;
        private const double FullyTransparent = 0;
        private const uint TogglingVisibilityAnimationDuration = 400;

        private static readonly SemaphoreSlim ToggleVisibilityAnimationSemaphoreSlim = new SemaphoreSlim(1);

        public LoadingView()
        {
            InitializeComponent();
        }

        public bool IsLoading
        {
            get => (bool)GetValue(IsLoadingProperty);
            set => SetValue(IsLoadingProperty, value);
        }

        public Color LoadingIndicatorColor
        {
            get => (Color)GetValue(LoadingIndicatorColorProperty);
            set => SetValue(LoadingIndicatorColorProperty, value);
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        public double TextFontSize
        {
            get => (double)GetValue(TextFontSizeProperty);
            set => SetValue(TextFontSizeProperty, value);
        }

        private static async void HandleIsRunningPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is LoadingView customActivityIndicator) || !(newValue is bool isRunning))
            {
                return;
            }

            await ToggleVisibility(customActivityIndicator);
        }

        private static void HandleSpinnerColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is LoadingView customActivityIndicator) || !(newValue is Color spinnerColor))
            {
                return;
            }

            customActivityIndicator.LoadingIndicatorSpinner.Color = spinnerColor;
        }

        private static void HandleTextFontColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is LoadingView customActivityIndicator) || !(newValue is Color textFontColor))
            {
                return;
            }

            customActivityIndicator.LoadingIndicatorText.TextColor = textFontColor;
        }

        private static void HandleTextFontSizePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is LoadingView customActivityIndicator) || !(newValue is double textFontSize))
            {
                return;
            }

            customActivityIndicator.LoadingIndicatorText.FontSize = textFontSize;
        }

        private static void HandleTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is LoadingView customActivityIndicator) || !(newValue is string text) || string.IsNullOrWhiteSpace(text))
            {
                return;
            }

            customActivityIndicator.LoadingIndicatorText.Text = text;
            customActivityIndicator.LoadingIndicatorText.IsVisible = true;
        }

        private static async Task ToggleVisibility(LoadingView customActivityIndicator)
        {
            try
            {
                ViewExtensions.CancelAnimations(customActivityIndicator);

                await ToggleVisibilityAnimationSemaphoreSlim.WaitAsync();
                if (customActivityIndicator.IsLoading)
                {
                    customActivityIndicator.LoadingIndicatorSpinner.IsRunning = true;
                    customActivityIndicator.IsVisible = true;
                    await customActivityIndicator.FadeTo(FullyOpaque, TogglingVisibilityAnimationDuration);
                }
                else
                {
                    await customActivityIndicator.FadeTo(FullyTransparent, TogglingVisibilityAnimationDuration);
                    customActivityIndicator.LoadingIndicatorSpinner.IsRunning = false;
                    customActivityIndicator.IsVisible = false;
                }
            }
            finally
            {
                ToggleVisibilityAnimationSemaphoreSlim.Release();
            }
        }
    }
}