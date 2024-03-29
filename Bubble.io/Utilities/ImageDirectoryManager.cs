namespace Bubble.io.Utilities
{
    public class ImageDirectoryManager
    {
        string directoryPath = $"./Resources";

        public async Task<string> Add(string userId, string url, string data)
        {
            Directory.CreateDirectory($"{directoryPath}/{userId}");
            
            string newUrl = Path.Combine( $"{directoryPath}/{userId}", url);
            byte[] imageBytes = Convert.FromBase64String( data );

            await File.WriteAllBytesAsync(newUrl, imageBytes);

            return newUrl.Replace('\\', '/');
        }

        public async Task<string> GetUserImage(string url)
        {

            byte[] data = await File.ReadAllBytesAsync(url);

            string base64Image = Convert.ToBase64String( data );
            return base64Image;
        }


        public async Task<string> GetDefaultImage()
        {
            byte[] data = await File.ReadAllBytesAsync($"./Resources/DefaultUserProfile.png");
            return Convert.ToBase64String( data );
        }
    }
}
