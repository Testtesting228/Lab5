using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lab4.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IWebHostEnvironment _environment;

        public IndexModel(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [BindProperty]
        public IFormFile? UploadedFile { get; set; }

        public string? Message { get; set; }

        public List<string> FileNames { get; set; } = new List<string>();

        public void OnGet()
        {
            LoadFilesList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (UploadedFile == null || UploadedFile.Length == 0)
            {
                Message = "Помилка: ви не обрали жодного файлу!";
                LoadFilesList();
                return Page();
            }

            string folderPath = Path.Combine(_environment.WebRootPath, "Files");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string filePath = Path.Combine(folderPath, Path.GetFileName(UploadedFile.FileName));

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await UploadedFile.CopyToAsync(fileStream);
            }

            Message = $"Файл «{UploadedFile.FileName}» успішно завантажено в wwwroot/Files!";
            LoadFilesList();
            return Page();
        }

        private void LoadFilesList()
        {
            string folderPath = Path.Combine(_environment.WebRootPath, "Files");
            if (Directory.Exists(folderPath))
            {
                var files = Directory.GetFiles(folderPath);
                foreach (var file in files)
                {
                    FileNames.Add(Path.GetFileName(file));
                }
            }
        }
    }
}