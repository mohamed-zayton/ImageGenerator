using SkiaSharp;
using SkiaSharp.HarfBuzz;

namespace ImageGenerator;

internal static class SKCanvasExtensions
{
    public static void DrawArabicText(this SKCanvas canvas, string text, float y, SKPaint paint, SKBitmap bitmap)
    {
        canvas.DrawShapedText(text, bitmap.Width - paint.MeasureText(text), y, paint);
    }
}
