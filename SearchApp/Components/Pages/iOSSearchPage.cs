using System.Runtime.CompilerServices;
using System.Windows.Input;
using SearchApp.Components.Controls;
using Xamarin.Forms;

namespace SearchApp.Components.Pages
{
    public class iOSSearchPage : ContentPage, ISearchEntry
    {
        public static readonly BindableProperty SearchTextProperty = BindableProperty.Create(nameof(SearchText), typeof(string), typeof(iOSSearchPage), string.Empty, BindingMode.TwoWay);
        public static readonly BindableProperty SearchCommandProperty = BindableProperty.Create(nameof(SearchCommand), typeof(ICommand), typeof(iOSSearchPage), null, BindingMode.OneWay);
        public static readonly BindableProperty SearchCommandParameterProperty = BindableProperty.Create(nameof(SearchCommand), typeof(object), typeof(iOSSearchPage), null, BindingMode.OneWay);
        public static readonly BindableProperty SearchCancelledCommandProperty = BindableProperty.Create(nameof(SearchCancelledCommand), typeof(ICommand), typeof(iOSSearchPage), null, BindingMode.OneWay);
        public static readonly BindableProperty SearchPlaceholderProperty = BindableProperty.Create(nameof(SearchPlaceholder), typeof(string), typeof(iOSSearchPage), string.Empty, BindingMode.OneWay);
        public static readonly BindableProperty IsSearchActiveProperty = BindableProperty.Create(nameof(IsSearchActive), typeof(bool), typeof(iOSSearchPage), false, BindingMode.OneWayToSource);
        public static readonly BindableProperty IsSearchFocusedProperty = BindableProperty.Create(nameof(IsSearchFocused), typeof(bool), typeof(iOSSearchPage), false, BindingMode.OneWayToSource);
        public static readonly BindableProperty ActionImageProperty = BindableProperty.Create(nameof(ActionImage), typeof(ImageSource), typeof(iOSSearchPage), null, BindingMode.OneWay);
        public static readonly BindableProperty ActionCommandProperty = BindableProperty.Create(nameof(ActionCommand), typeof(ICommand), typeof(iOSSearchPage), null, BindingMode.OneWay);

        public string SearchText
        {
            get => (string)this.GetValue(SearchTextProperty);
            set => this.SetValue(SearchTextProperty, value);
        }

        public ICommand SearchCommand
        {
            get => (ICommand)this.GetValue(SearchCommandProperty);
            set => this.SetValue(SearchCommandProperty, value);
        }

        public object SearchCommandParameter
        {
            get => this.GetValue(SearchCommandParameterProperty);
            set => this.SetValue(SearchCancelledCommandProperty, value);
        }

        public ICommand SearchCancelledCommand
        {
            get => (ICommand)this.GetValue(SearchCancelledCommandProperty);
            set => this.SetValue(SearchCancelledCommandProperty, value);
        }

        public string SearchPlaceholder
        {
            get => (string)this.GetValue(SearchPlaceholderProperty);
            set => this.SetValue(SearchPlaceholderProperty, value);
        }

        public bool IsSearchActive
        {
            get => (bool)this.GetValue(IsSearchActiveProperty);
            set => this.SetValue(IsSearchActiveProperty, value);
        }

        public bool IsSearchFocused
        {
            get => (bool)this.GetValue(IsSearchFocusedProperty);
            set => this.SetValue(IsSearchFocusedProperty, value);
        }

        public ImageSource ActionImage
        {
            get => (ImageSource)this.GetValue(ActionImageProperty);
            set => this.SetValue(ActionImageProperty, value);
        }

        public ICommand ActionCommand
        {
            get => (ICommand)this.GetValue(ActionCommandProperty);
            set => this.SetValue(ActionCommandProperty, value);
        }

        string ISearchEntry.Placeholder
        {
            get => this.SearchPlaceholder;
            set => this.SearchPlaceholder = value;
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == SearchTextProperty.PropertyName || propertyName == IsSearchFocusedProperty.PropertyName)
            {
                if (this.IsSearchFocused)
                {
                    this.IsSearchActive = true;
                }
                else if (!string.IsNullOrWhiteSpace(this.SearchText))
                {
                    this.IsSearchActive = true;
                }
                else
                {
                    this.IsSearchActive = false;
                }
            }
        }
    }
}
