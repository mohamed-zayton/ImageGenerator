using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using ImageGenerator;

// Output image is 96dpi, and Skia doesn't seem to support changing it.
// Use this website to convert cm dimensions to pixels under a given resolution (in dpi) https://www.pixelto.net/cm-to-px-converter

const float Resolution = 300;
const int WidthFor96Dpi = 794;
const int HeightFor96Dpi = 1134;
var bitmap = new Bitmap(width: (int)(WidthFor96Dpi * Resolution / 96), height: (int)(HeightFor96Dpi * Resolution / 96));
bitmap.SetResolution(Resolution, Resolution);
var g = Graphics.FromImage(bitmap);
g.CompositingQuality = CompositingQuality.HighQuality;
var font = new Font("Times New Roman", 16);

var lineBuilder = new StringBuilder();
var pageBuilder = new StringBuilder();
const int NumberOfImages = 10;
Directory.CreateDirectory("./data/");
int lineHeight;
for (int i = 0; i < NumberOfImages; i++)
{
    g.FillRectangle(Brushes.White, new(0, 0, bitmap.Width, bitmap.Height));
    for (int line = 50; line < bitmap.Height; line += lineHeight + 5)
    {
        lineBuilder.Clear();
        // Don't generate a space at the beginning of a new line.
        ArabicCharacterGenerator.PreviousWasSpace = true;
        while (true)
        {
            lineBuilder.Append(ArabicCharacterGenerator.Generate());
            var text = lineBuilder.ToString();
            var textSize = g.MeasureString(text, font);
            lineHeight = (int)textSize.Height;
            if (textSize.Width >= bitmap.Width)
            {
                g.DrawString(text, font, Brushes.Black, 0, line);
                pageBuilder.AppendLine(text);
                break;
            }
        }
    }

    bitmap.Save($"./data/{i}.png");
    File.WriteAllText($"./data/{i}.txt", pageBuilder.ToString());
    pageBuilder.Clear();
}

