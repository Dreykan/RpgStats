using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace RpgStats.BlazorServer.Shared;

public partial class AppBar
{
    private bool _isLightMode = true;
    private MudTheme _currentTheme = new();
    private PaletteDark _darkPalette = new();
    private PaletteLight _lightPalette = new();

    [Parameter] public EventCallback OnSidebarToggled { get; set; }
    [Parameter] public EventCallback<MudTheme> OnThemeToggled { get; set; }

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