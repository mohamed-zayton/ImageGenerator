using System.Text;
using ImageGenerator;
using SkiaSharp;

// Output image is 96dpi, and Skia doesn't seem to support changing it.
// Use this website to convert cm dimensions to pixels under a given resolution (in dpi) https://www.pixelto.net/cm-to-px-converter
var bitmap = new SKBitmap(width: 794, height: 1134);
var paint = new SKPaint(new SKFont(SKTypeface.Default)) { TextSize = 16 };
var canvas = new SKCanvas(bitmap);
canvas.Clear(new SKColor(255, 255, 255));

var lineBuilder = new StringBuilder();
var pageBuilder = new StringBuilder();
const int NumberOfImages = 2000;
Directory.CreateDirectory("./data/");
for (int i = 0; i < NumberOfImages; i++)
{
    for (int line = 50; line < 1130; line += 30)
    {
        lineBuilder.Clear();
        while (true)
        {
            lineBuilder.Append(ArabicCharacterGenerator.Generate());
            var text = lineBuilder.ToString();
            if (paint.MeasureText(text) >= 700)
            {
                canvas.DrawArabicText(text, line, paint, bitmap);
                pageBuilder.AppendLine(text);
                break;
            }
        }
    }

    using (var stream = File.OpenWrite($"./data/{i}.jpg"))
    {
        bitmap.Encode(SKEncodedImageFormat.Jpeg, quality: 100).SaveTo(stream);
        File.WriteAllText($"./data/{i}.txt", pageBuilder.ToString());
        pageBuilder.Clear();
    }
}

