using System.Collections.ObjectModel;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace RotateBindingContext;

public sealed class MainPageViewModel : ObservableObject
{
    public MainPageViewModel(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Items.Add($"Item {i}");
        }
    }

    public ObservableCollection<string> Items { get; } = [];

    public int Span
    {
        get => field;
        set => SetProperty(ref field, value);
    } = 1;

    public IRelayCommand AddColumnCommand => field ??= new RelayCommand(AddColumn);

    public void AddColumn()
    {
        Span++;
    }

    public IRelayCommand RemoveColumnCommand => field ??= new RelayCommand(new Action(RemoveColumn), CanRemoveColumn);

    public bool CanRemoveColumn() => Span > 1;

    public void RemoveColumn()
    {
        Span--;
    }

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(Span))
        {
            RemoveColumnCommand.NotifyCanExecuteChanged();
        }

        base.OnPropertyChanged(e);
    }
}