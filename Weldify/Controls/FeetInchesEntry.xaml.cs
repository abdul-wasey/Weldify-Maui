namespace Weldify.Controls;

/// <summary>
/// Reusable measurement input: Feet / Inches / Suttar (1 inch = 8 suttar, common
/// in Pakistani welding and fabrication trades).
/// Auto-carry rules:
///   Suttar >= 8  →  carry into Inches
///   Inches >= 12 →  carry into Feet
/// Exposes TotalInches (double) as a convenience binding for storage/pricing.
/// </summary>
public partial class FeetInchesEntry : ContentView
{
    public static readonly BindableProperty LabelProperty =
        BindableProperty.Create(nameof(Label), typeof(string), typeof(FeetInchesEntry), string.Empty);

    public static readonly BindableProperty FeetProperty =
        BindableProperty.Create(nameof(Feet), typeof(int), typeof(FeetInchesEntry), 0,
            BindingMode.TwoWay, propertyChanged: OnAnyValueChanged);

    public static readonly BindableProperty InchesProperty =
        BindableProperty.Create(nameof(Inches), typeof(int), typeof(FeetInchesEntry), 0,
            BindingMode.TwoWay, propertyChanged: OnAnyValueChanged);

    public static readonly BindableProperty SuttarProperty =
        BindableProperty.Create(nameof(Suttar), typeof(int), typeof(FeetInchesEntry), 0,
            BindingMode.TwoWay, propertyChanged: OnAnyValueChanged);

    /// <summary>
    /// Total value expressed as decimal inches.
    /// Formula: (Feet * 12) + Inches + (Suttar / 8.0)
    /// Bind with Mode=OneWayToSource to receive the final number in the ViewModel.
    /// </summary>
    public static readonly BindableProperty TotalInchesProperty =
        BindableProperty.Create(nameof(TotalInches), typeof(double), typeof(FeetInchesEntry), 0.0,
            BindingMode.OneWayToSource);

    public static readonly BindableProperty HasValidationErrorProperty =
        BindableProperty.Create(nameof(HasValidationError), typeof(bool), typeof(FeetInchesEntry), false);

    public static readonly BindableProperty ValidationMessageProperty =
        BindableProperty.Create(nameof(ValidationMessage), typeof(string), typeof(FeetInchesEntry), string.Empty);

    public string Label
    {
        get => (string)GetValue(LabelProperty);
        set => SetValue(LabelProperty, value);
    }

    public int Feet
    {
        get => (int)GetValue(FeetProperty);
        set => SetValue(FeetProperty, value);
    }

    public int Inches
    {
        get => (int)GetValue(InchesProperty);
        set => SetValue(InchesProperty, value);
    }

    /// <summary>1 inch = 8 suttar in Pakistani trade convention.</summary>
    public int Suttar
    {
        get => (int)GetValue(SuttarProperty);
        set => SetValue(SuttarProperty, value);
    }

    public double TotalInches
    {
        get => (double)GetValue(TotalInchesProperty);
        private set => SetValue(TotalInchesProperty, value);
    }

    public bool HasValidationError
    {
        get => (bool)GetValue(HasValidationErrorProperty);
        private set => SetValue(HasValidationErrorProperty, value);
    }

    public string ValidationMessage
    {
        get => (string)GetValue(ValidationMessageProperty);
        private set => SetValue(ValidationMessageProperty, value);
    }

    public FeetInchesEntry()
    {
        InitializeComponent();
    }

    static void OnAnyValueChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not FeetInchesEntry ctrl) return;
        ctrl.Normalize();
    }

    bool _normalizing;

    void Normalize()
    {
        if (_normalizing) return;
        _normalizing = true;

        try
        {
            var feet = Math.Max(0, Feet);
            var inches = Math.Max(0, Inches);
            var suttar = Math.Max(0, Suttar);

            // Carry: suttar → inches (8 suttar per inch)
            if (suttar >= 8)
            {
                inches += suttar / 8;
                suttar = suttar % 8;
            }

            // Carry: inches → feet (12 inches per foot)
            if (inches >= 12)
            {
                feet += inches / 12;
                inches = inches % 12;
            }

            bool hasError = false;
            if (Feet < 0 || Inches < 0 || Suttar < 0)
            {
                ValidationMessage = "Values cannot be negative.";
                HasValidationError = true;
                hasError = true;
            }

            if (!hasError && HasValidationError)
            {
                ValidationMessage = string.Empty;
                HasValidationError = false;
            }

            // Only update bindings if values actually changed (avoids re-triggering the handler)
            if (Feet != feet) SetValue(FeetProperty, feet);
            if (Inches != inches) SetValue(InchesProperty, inches);
            if (Suttar != suttar) SetValue(SuttarProperty, suttar);

            TotalInches = (feet * 12) + inches + (suttar / 8.0);
        }
        finally
        {
            _normalizing = false;
        }
    }
}