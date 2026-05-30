NeedyNest UI Theme
===================

What I changed
- Added `UI/ThemeManager.cs` — centralized colors, fonts, and styling helpers.
- Added `UI/BaseForm.cs` — a base form that applies the theme to all pages on load.
- Updated `Program.cs` to initialize the theme at startup.
- Updated all form `.cs` files to inherit `BaseForm` so the look is consistent across the app.

How it works
- Forms now inherit `BaseForm`. When a form loads, `ThemeManager.ApplyTo(this)` applies consistent fonts, colors and control styles at runtime.
- Most designer-set fonts and colors are overridden at runtime so you get a unified professional look without editing every Designer file.

Customizing the theme
- Edit `UI/ThemeManager.cs` to change `PrimaryColor`, `AccentColor`, `BackgroundColor`, `SurfaceColor`, and `DefaultFont`.
- If you want additional control-specific styling, add cases to `ApplyToControls` in `ThemeManager`.

Next steps (recommended)
- Review each form in the Designer and use layout panels (FlowLayoutPanel, TableLayoutPanel) where appropriate to make forms responsive.
- Replace any inline images or fonts with high-quality assets and update resources.
- Optionally add a shared header/footer in `BaseForm` (requires small layout adjustments in forms).

If you'd like, I can:
- Tweak spacing, button sizes and a header bar for every form.
- Produce a sample styled login form (mockup) for review before applying deeper layout changes.
