using System.ComponentModel.DataAnnotations;
namespace Shop.Infrastructure.Components
{
    public class FileExtensionAtribiute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value , ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                 var extension = Path.GetExtension(file.FileName);

                string[] extensions = { ".jpg", ".png", ".jpeg" };

                bool result = extensions.Any(x =>extension.EndsWith(x));

                if (!result)
                {
                    return new ValidationResult("Alloweg extension");
                }
            }
            return ValidationResult.Success;
        }
    }
}
