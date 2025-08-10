using System.Text;

namespace InfotecsWebAPI.Web.Services
{
    public static class FileReader
    {
        /// <summary>
        /// Reads received IFormFile content into string
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static async Task<string> ReadAsStringAsync(this IFormFile file)
        {
            var result = new StringBuilder();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                    result.AppendLine(await reader.ReadLineAsync());
            }
            return result.ToString();
        }
    }
}
