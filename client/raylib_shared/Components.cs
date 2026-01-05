using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Riskuj.RaylibShared;

public class TextBox()
{
    Font font = LoadFont(AppDomain.CurrentDomain.BaseDirectory + "font.ttf");
    public required string text;
    public required Color color;
    public required Rectangle rectangle;
    public void Draw()
    {
        DrawRectangleLinesEx(rectangle, 1, color);
        int fontSize = 40;
        int spacing = 5;
        Vector2 fontMeasurement = MeasureTextEx(font, text, fontSize, spacing);
        DrawTextEx(font, text, rectangle.Position + rectangle.Size / 2 - fontMeasurement / 2, fontSize, spacing, color);
    }
}


public class Button()
{
    public required Rectangle rectangle;
    public required string text;
    public required Color color;
    public bool pressed = false;
    public Action? onPressed;
    Font font = LoadFont(AppDomain.CurrentDomain.BaseDirectory + "font.ttf");
    public void Draw()
    {
        if (pressed)
            DrawRectangleRec(rectangle,color);
        else
            DrawRectangleLinesEx(rectangle, 1, color);
        int fontSize = 40;
        int spacing = 5;
        Vector2 fontMeasurement = MeasureTextEx(font, text, fontSize, spacing);
        DrawTextEx(font, text, rectangle.Position + rectangle.Size / 2 - fontMeasurement / 2, fontSize, spacing, color);
    }
    public void Update()
    {
        if (IsMouseButtonDown(MouseButton.Left))
        {
            Vector2 mouse = GetMousePosition();
            pressed = rectangle.X < mouse.X && rectangle.X + rectangle.Size.X > mouse.X && rectangle.Y < mouse.Y && rectangle.Y + rectangle.Size.Y > mouse.Y;
            if (pressed && onPressed != null) onPressed();
            return;
        }
        if (!IsMouseButtonUp(MouseButton.Left)) return;
        pressed = false;
    }
}