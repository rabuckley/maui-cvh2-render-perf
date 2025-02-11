using System.Diagnostics;

namespace RotateBindingContext;

public sealed class AdaptableCollectionView : CollectionView
{
    public static readonly BindableProperty ItemMinimumWidthRequestProperty = BindableProperty.Create(
        nameof(ItemMinimumWidthRequest),
        typeof(double),
        typeof(AdaptableCollectionView),
        100.0);

    public static readonly BindableProperty SpanProperty = BindableProperty.Create(
        nameof(Span),
        typeof(int),
        typeof(AdaptableCollectionView),
        1);

    public AdaptableCollectionView()
    {
        var layout = new GridItemsLayout(ItemsLayoutOrientation.Vertical);

        layout.SetBinding(
            GridItemsLayout.SpanProperty,
            static (AdaptableCollectionView c) => c.Span,
            source: this);

        ItemsLayout = layout;
    }

    public int Span
    {
        get => (int)GetValue(SpanProperty);
        set => SetValue(SpanProperty, value);
    }

    public double ItemMinimumWidthRequest
    {
        get => (double)GetValue(ItemMinimumWidthRequestProperty);
        set => SetValue(ItemMinimumWidthRequestProperty, value);
    }

    protected override void OnBindingContextChanged()
    {
        Debug.WriteLine($"{nameof(AdaptableCollectionView)}.{nameof(OnBindingContextChanged)}: {BindingContext ?? "<null>"}");
        base.OnBindingContextChanged();
    }

    protected override void OnSizeAllocated(double width, double height)
    {
        var span = Math.Max(1, (int)Math.Floor(width / ItemMinimumWidthRequest));
        Debug.WriteLine($"{nameof(AdaptableCollectionView)}.{nameof(OnSizeAllocated)}: {width}x{height}, span={span}");
        Span = span;
    }
}
