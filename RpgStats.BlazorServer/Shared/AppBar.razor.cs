using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace RpgStats.BlazorServer.Shared;

public partial class AppBar
{
    private PaletteDark _darkPalette = new();
    private PaletteLight _lightPalette = new();
    private MudTheme _currentTheme = new();
    private bool _isLightMode = false;

    [Parameter] public EventCallback OnSidebarToggled { get; set; }
    [Parameter] public EventCallback<MudTheme> OnThemeToggled { get; set; }

    protected override Task OnInitializedAsync()
    {
        _currentTheme.Palette = _darkPalette;
        return base.OnInitializedAsync();
    }

    private async Task ToggleTheme()
    {
        if (_isLightMode)
        {
            _currentTheme.Palette = _darkPalette;
            _isLightMode = false;
        }
        else
        {
            _currentTheme.Palette = _lightPalette;
            _isLightMode = true;
        }

        await OnThemeToggled.InvokeAsync(_currentTheme);
    }
}