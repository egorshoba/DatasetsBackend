using DatasetsBackend.Dtos;
using DatasetsBackend.Helpers;
using DatasetsBackend.Validators;
using Microsoft.AspNetCore.Mvc;

namespace DatasetsBackend.Controllers
{
    public static class DatasetsController
    {
        [Consumes("multipart/form-data")]
        [DisableRequestSizeLimit]
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        public static IResult Upload(HttpRequest request)
        {
            try
            {
                var form = request.ReadFormAsync().Result;

                var datasetMetadata = CustomDeserializer.DeserializeForm<UploadDatasetDto>(form);

                var formFile = form.Files["File"];

                if (formFile is null || formFile.Length == 0)
                    return Results.BadRequest(new { error = "File is required" });

                var validator = new DatasetValidator(datasetMetadata, formFile);

                var validationErrors = validator.GetValidationErrors();

                if (validationErrors.Any())
                    return Results.BadRequest(new { validationErrors });

                return Results.NoContent();
            }
            catch (ArgumentException ex)
            {
                return Results.BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Results.Problem("Unknown error");
            }
        }
    }
}
