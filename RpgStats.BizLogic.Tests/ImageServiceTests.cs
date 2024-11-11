using SkiaSharp;

namespace RpgStats.BizLogic.Tests;

public class ImageServiceTests
{
    [Fact]
    public void ResizeImageTo512_ShouldResizeLargerImageTo512()
    {
        // Arrange
        using var sourceBitmap = new SKBitmap(1024, 768);
        using var image = SKImage.FromBitmap(sourceBitmap);
        using var data = image.Encode(SKEncodedImageFormat.Png, 100);
        var imageBytes = data.ToArray();

        // Act
        var resizedImageBytes = ImageService.ResizeImageTo512(imageBytes);

        using var ms = new MemoryStream(resizedImageBytes);
        using var resizedBitmap = SKBitmap.Decode(ms);

        // Assert
        Assert.Equal(512, Math.Max(resizedBitmap.Width, resizedBitmap.Height));
    }

    [Fact]
    public void ResizeImageTo512_ShouldHandleSquareImageCorrectly()
    {
        // Arrange
        using var sourceBitmap = new SKBitmap(1024, 1024);
        using var image = SKImage.FromBitmap(sourceBitmap);
        using var data = image.Encode(SKEncodedImageFormat.Png, 100);
        var imageBytes = data.ToArray();

        // Act
        var resizedImageBytes = ImageService.ResizeImageTo512(imageBytes);

        using var ms = new MemoryStream(resizedImageBytes);
        using var resizedBitmap = SKBitmap.Decode(ms);

        // Assert
        Assert.Equal(512, resizedBitmap.Width);
        Assert.Equal(512, resizedBitmap.Height);
    }

    [Fact]
    public void ConvertByteArrayToImage_ShouldReturnValidBase64String()
    {
        // Arrange
        var imageBytes = new byte[] { 1, 2, 3, 4, 5 }; // example byte array

        // Act
        var base64String = ImageService.ConvertByteArrayToImage(imageBytes);

        // Assert
        Assert.StartsWith("data:image/png;base64,", base64String);
        Assert.Equal("data:image/png;base64," + Convert.ToBase64String(imageBytes), base64String);
    }

    [Fact]
    public void ResizeImageTo512_ShouldThrowExceptionOnInvalidInput()
    {
        // Arrange
        byte[] invalidImageBytes = { 0, 1, 2, 3 }; // Invalid image data

        // Act & Assert
        Assert.Throws<ArgumentException>(() => ImageService.ResizeImageTo512(invalidImageBytes));
    }
}