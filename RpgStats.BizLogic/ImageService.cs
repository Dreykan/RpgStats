using SkiaSharp;

namespace RpgStats.BizLogic;

public class ImageService
{
    public static byte[] ResizeImageTo512(byte[] imageBytes, SKFilterQuality quality = SKFilterQuality.Medium)
    {
        using MemoryStream ms = new MemoryStream(imageBytes);
        using SKBitmap sourceBitmap = SKBitmap.Decode(ms);
        double ratio = (double)sourceBitmap.Width / sourceBitmap.Height;

        int width;
        int height;

        if (sourceBitmap.Width > sourceBitmap.Height)
        {
            width = 512;
            var tmpHeight = width / ratio;
            height = (int)Math.Ceiling(tmpHeight);
        }
        else if (sourceBitmap.Height > sourceBitmap.Width)
        {
            height = 512;
            var tmpWidth = height * ratio;
            width = (int)Math.Ceiling(tmpWidth);
        }
        else
        {
            width = 512;
            height = 512;
        }

        using SKBitmap scaledBitmap = sourceBitmap.Resize(new SKImageInfo(width, height), quality);
        using SKImage scaledImage = SKImage.FromBitmap(scaledBitmap);
        using SKData data = scaledImage.Encode(SKEncodedImageFormat.Jpeg, 90);

        return data.ToArray();
    }

    public static string ConvertByteArrayToImage(byte[] imageBytes)
    {
        return $"data:image/png;base64,{Convert.ToBase64String(imageBytes)}";
    }
}