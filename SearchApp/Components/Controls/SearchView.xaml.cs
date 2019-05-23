using Xamarin.Forms;

namespace SearchApp.Components.Controls
{
    public partial class SearchView : ContentView
    {
        public static readonly BindableProperty PrimaryViewProperty = BindableProperty.Create(nameof(PrimaryView), typeof(View), typeof(SearchView));
        public static readonly BindableProperty ResultsViewProperty = BindableProperty.Create(nameof(ResultsView), typeof(View), typeof(SearchView));
        public static readonly BindableProperty SearchEntryProperty = BindableProperty.Create(nameof(SearchEntry), typeof(ISearchEntry), typeof(SearchView));

        public View PrimaryView
        {
            get => (View)this.GetValue(PrimaryViewProperty);
            set => this.SetValue(PrimaryViewProperty, value);
        }

        public View ResultsView
        {
            get => (View)this.GetValue(ResultsViewProperty);
            set => this.SetValue(ResultsViewProperty, value);
        }

        public ISearchEntry SearchEntry
        {
            get => (ISearchEntry)this.GetValue(SearchEntryProperty);
            set => this.SetValue(SearchEntryProperty, value);
        }

        public SearchView()
        {
            this.InitializeComponent();
        }
    }
}



