using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Drawing.Imaging;

namespace Common.Extensions
{
    public static class ImageExtensions
    {
        public static string GetBase64Thumbnail(this string path, int maxWidth, int maxHeight)
        {
            using (var image = Image.FromFile(path))
            {
                var size = image.Size.ResizeKeepAspectRatio(maxWidth, maxHeight);
                var thumbnailImage = image.GetThumbnailImage(size.Width, size.Height, () => false, IntPtr.Zero);
                return thumbnailImage.ToBase64();
            }
        }

        /// <summary>
        /// Converts a System.Drawing.Image to a base-64 encoded string
        /// </summary>
        /// <param name="image"></param>
        /// <param name="imageFormat"></param>
        /// <returns></returns>
        public static string ToBase64(this Image image, ImageFormat imageFormat = null)
        {
            var imageStream = new MemoryStream();
            image.Save(imageStream, imageFormat ?? ImageFormat.Png);
            imageStream.Position = 0;
            var imageBytes = imageStream.ToArray();
            return Convert.ToBase64String(imageBytes);
        }

        /// <summary>
        /// Generates a base-64 string with file extension specified suitable for inserting on a HTML img src attribute
        /// </summary>
        /// <param name="image"></param>
        /// <param name="extension"></param>
        /// <returns></returns>
        public static string ToBase64ImageSrc(this Image image, string extension)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append($"data:image/{extension};base64,");
            stringBuilder.Append(image.ToBase64());
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Resizes an image whilst maintaining aspect ratio
        /// </summary>
        /// <param name="size"></param>
        /// <param name="maxWidth"></param>
        /// <param name="maxHeight"></param>
        /// <returns></returns>
        public static Size ResizeKeepAspectRatio(this Size size, int maxWidth, int maxHeight)
        {
            var rnd = Math.Min(maxWidth / (decimal)size.Width, maxHeight / (decimal)size.Height);
            return new Size((int)Math.Round(size.Width * rnd), (int)Math.Round(size.Height * rnd));
        }
    }
}