using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Foundation;
using SearchApp.Components.Pages;
using SearchApp.iOS.Components.Renderers;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(iOSSearchPage), typeof(SearchPageRenderer))]
namespace SearchApp.iOS.Components.Renderers
{
    public class SearchPageRenderer : PageRenderer, IUISearchBarDelegate
    {
        private readonly UISearchController searchController;

        public SearchPageRenderer()
        {
            this.searchController = new UISearchController(searchResultsController: null)
            {
                HidesNavigationBarDuringPresentation = false,
                DimsBackgroundDuringPresentation = false,
                ObscuresBackgroundDuringPresentation = false,
                DefinesPresentationContext = true
            };

            this.searchController.SearchBar.Delegate = this;
            this.searchController.SearchBar.SearchBarStyle = UISearchBarStyle.Default;
            this.searchController.SearchBar.Translucent = true;
            this.searchController.SearchBar.BarStyle = UIBarStyle.Black;
        }

        [Export("searchBarCancelButtonClicked:")]
        public void CancelButtonClicked(UISearchBar searchBar)
        {
            if (this.Element is iOSSearchPage iosSearchPage
                && iosSearchPage.SearchCancelledCommand != null
                && iosSearchPage.SearchCancelledCommand.CanExecute(null))
            {
                this.TextChanged(this.searchController.SearchBar, string.Empty);
                iosSearchPage.SearchCancelledCommand.Execute(null);
            }
        }

        [Export("searchBarTextDidBeginEditing:")]
        public void OnEditingStarted(UISearchBar searchBar)
        {
            if (this.Element is iOSSearchPage iosSearchPage)
            {
                iosSearchPage.IsSearchFocused = true;
            }
        }

        [Export("searchBarTextDidEndEditing:")]
        public void OnEditingStopped(UISearchBar searchBar)
        {
            if (this.Element is iOSSearchPage iosSearchPage)
            {
                iosSearchPage.IsSearchFocused = false;
            }
        }

        [Export("searchBarSearchButtonClicked:")]
        public void SearchButtonClicked(UISearchBar searchBar)
        {
            if (this.Element is iOSSearchPage iosSearchPage
                && iosSearchPage.SearchCommand != null
                && iosSearchPage.SearchCommand.CanExecute(this.searchController.SearchBar.Text))
            {
                iosSearchPage.SearchCommand.Execute(this.searchController.SearchBar.Text);
            }
        }

        [Export("searchBarBookmarkButtonClicked:")]
        public void BookmarkButtonClicked(UISearchBar searchBar)
        {
            if (this.Element is iOSSearchPage iosSearchPage
                && iosSearchPage.ActionCommand != null
                && iosSearchPage.ActionCommand.CanExecute(null))
            {
                iosSearchPage.ActionCommand.Execute(null);
            }
        }

        [Export("searchBar:textDidChange:")]
        public void TextChanged(UISearchBar searchBar, string searchText)
        {
            if (this.Element is iOSSearchPage iosSearchPage)
            {
                iosSearchPage.SetValue(iOSSearchPage.SearchTextProperty, searchText);
            }
        }

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            if (e.OldElement is iOSSearchPage oldSearchPage)
            {
                oldSearchPage.PropertyChanged -= this.OnSearchPagePropertyChanged;
            }

            if (e.NewElement is iOSSearchPage iosSearchPage)
            {
                iosSearchPage.PropertyChanged += this.OnSearchPagePropertyChanged;
                this.searchController.SearchBar.Placeholder = iosSearchPage.SearchPlaceholder;
                this.searchController.SearchBar.Text = iosSearchPage.SearchText;

                Task.Run(async () =>
                {
                    if (iosSearchPage.ActionImage != null)
                    {
                        try
                        {
                            var handler = new FileImageSourceHandler();
                            var actionImage = await handler.LoadImageAsync(iosSearchPage.ActionImage);

                            this.searchController.SearchBar.SetImageforSearchBarIcon(actionImage, UISearchBarIcon.Bookmark, UIControlState.Normal);
                            this.searchController.SearchBar.ShowsBookmarkButton = true;
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine($"Error loading ActionImage: {ex.Message}");
                        }
                    }
                });
            }
        }

        private void OnSearchPagePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is iOSSearchPage iOSSearchPage && e.PropertyName.Equals(iOSSearchPage.SearchTextProperty.PropertyName))
            {
                this.searchController.SearchBar.Text = iOSSearchPage.SearchText;
            }
        }

        public override void WillMoveToParentViewController(UIViewController parent)
        {
            parent.NavigationItem.SearchController = this.searchController;
            parent.NavigationItem.HidesSearchBarWhenScrolling = false;
        }

        protected override void Dispose(bool disposing)
        {
            this.searchController?.Dispose();
            base.Dispose(disposing);
        }
    }
}
