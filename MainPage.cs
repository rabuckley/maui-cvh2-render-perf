using System.Diagnostics;

namespace RotateBindingContext;

public sealed partial class MainPage : ContentPage
{
	public MainPage()
	{
		BindingContext = new MainPageViewModel(200);

		var subtractButton = new Button
		{
			Text = "Remove",
		};

		subtractButton.SetBinding(
			Button.CommandProperty,
			static (MainPageViewModel m) => m.RemoveColumnCommand);

		var addButton = new Button
		{
			Text = "Add",
		};

		addButton.SetBinding(
			Button.CommandProperty,
			static (MainPageViewModel m) => m.AddColumnCommand);

		var layout = new GridItemsLayout(ItemsLayoutOrientation.Vertical);

		layout.SetBinding(
			GridItemsLayout.SpanProperty,
			static (MainPageViewModel m) => m.Span);

		var collection = new CollectionView
		{
			ItemsLayout = layout,
			ItemTemplate = new DataTemplate(() => new CollectionItemView
			{
				WidthRequest = 100,
				HeightRequest = 200,
			})
		};

		collection.SetBinding(
			CollectionView.ItemsSourceProperty,
			static (MainPageViewModel m) => m.Items);

		var grid = new Grid
		{
			RowDefinitions =
			[
				new RowDefinition { Height = GridLength.Auto },
				new RowDefinition { Height = GridLength.Star },
			]
		};

		grid.Add(new HorizontalStackLayout { Children = { subtractButton, addButton } }, row: 0);
		grid.Add(collection, row: 1);

		Content = grid;
	}

	protected override void OnBindingContextChanged()
	{
		Debug.WriteLine($"{nameof(MainPage)}.{nameof(OnBindingContextChanged)}");
		base.OnBindingContextChanged();
	}
}
