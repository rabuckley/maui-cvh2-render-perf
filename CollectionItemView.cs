using System.Diagnostics;
using Microsoft.Maui.Controls.Shapes;

namespace RotateBindingContext;

public sealed class CollectionItemView : Border
{
    public CollectionItemView()
    {
        Background = Colors.Blue;
        StrokeShape = new RoundRectangle { CornerRadius = 12 };

        var label = new Label
        {
            TextColor = Colors.White,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            FontSize = 24
        };

        label.SetBinding(
            Label.TextProperty,
            static (string s) => s);

        Content = label;
    }

    protected override void OnBindingContextChanged()
    {
        Debug.WriteLine($"{nameof(CollectionItemView)}.{nameof(OnBindingContextChanged)}: {BindingContext ?? "<null>"}");
        base.OnBindingContextChanged();
    }
}