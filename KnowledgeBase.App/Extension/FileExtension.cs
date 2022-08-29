namespace KnowledgeBase.App.Extension
{
    public static class FileExtension
    {
        
        public static async Task<string> SavaAsync(this IFormFile file, string root, string folder)
        {
            string path = Path.Combine(root, folder);
            string filename = Path.Combine(Guid.NewGuid().ToString() + Path.GetFileName(file.FileName));
            string resultPath = Path.Combine(path, filename);

            using (FileStream fileStream = new FileStream(resultPath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return filename;
        }

        public static void Delete(string root, string folder, string filename)
        {
            string path = Path.Combine(root, folder, filename);

            if (File.Exists(path))
            {
                File.Delete(path);
            }

        }
    }
}
