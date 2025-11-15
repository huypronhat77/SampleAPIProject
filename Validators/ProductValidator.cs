using SampleAPI.Models;

namespace SampleAPI.Validators;

public static class ProductValidator
{
    public static List<string> ValidateCreate(ProductCreateDto dto)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(dto.Name))
        {
            errors.Add("Name is required.");
        }
        else if (dto.Name.Length < 3 || dto.Name.Length > 100)
        {
            errors.Add("Name must be between 3 and 100 characters.");
        }

        if (dto.Price <= 0)
        {
            errors.Add("Price must be greater than 0.");
        }

        if (!Enum.IsDefined(typeof(ProductCategory), dto.Category))
        {
            errors.Add("Invalid category value.");
        }

        if (!string.IsNullOrWhiteSpace(dto.Description) && dto.Description.Length > 500)
        {
            errors.Add("Description must not exceed 500 characters.");
        }

        return errors;
    }

    public static List<string> ValidateUpdate(ProductUpdateDto dto)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(dto.Name))
        {
            errors.Add("Name is required.");
        }
        else if (dto.Name.Length < 3 || dto.Name.Length > 100)
        {
            errors.Add("Name must be between 3 and 100 characters.");
        }

        if (dto.Price <= 0)
        {
            errors.Add("Price must be greater than 0.");
        }

        if (!Enum.IsDefined(typeof(ProductCategory), dto.Category))
        {
            errors.Add("Invalid category value.");
        }

        if (!string.IsNullOrWhiteSpace(dto.Description) && dto.Description.Length > 500)
        {
            errors.Add("Description must not exceed 500 characters.");
        }

        return errors;
    }

    public static List<string> ValidatePatch(ProductPatchDto dto)
    {
        var errors = new List<string>();

        if (dto.Name != null && (dto.Name.Length < 3 || dto.Name.Length > 100))
        {
            errors.Add("Name must be between 3 and 100 characters.");
        }

        if (dto.Price.HasValue && dto.Price.Value <= 0)
        {
            errors.Add("Price must be greater than 0.");
        }

        if (dto.Category.HasValue && !Enum.IsDefined(typeof(ProductCategory), dto.Category.Value))
        {
            errors.Add("Invalid category value.");
        }

        if (dto.Description != null && dto.Description.Length > 500)
        {
            errors.Add("Description must not exceed 500 characters.");
        }

        return errors;
    }
}


