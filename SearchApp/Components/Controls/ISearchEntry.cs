using System.Windows.Input;

namespace SearchApp.Components.Controls
{
    public interface ISearchEntry
    {
        string Placeholder { get; set; }
        ICommand SearchCommand { get; set; }
        object SearchCommandParameter { get; set; }
        ICommand SearchCancelledCommand { get; set; }
        bool IsSearchActive { get; set; }
        bool IsSearchFocused { get; }
    }
}
