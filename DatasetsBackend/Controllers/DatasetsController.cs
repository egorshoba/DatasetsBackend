using DatasetsBackend.Data;
using DatasetsBackend.Dtos;
using DatasetsBackend.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.IO.Compression;

namespace DatasetsBackend.Controllers
{
    public static class DatasetsController
    {
        [Consumes("multipart/form-data")]
        [DisableRequestSizeLimit]
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        public static IResult Upload(HttpRequest request)
        {
            var form = request.ReadFormAsync().Result;

            var model = CustomDeserializer.DeserializeForm<UploadDatasetDto>(form);

            var formFile = form.Files["File"];

            if (formFile is null || formFile.Length == 0)
                return Results.BadRequest();


            using (var stream = formFile.OpenReadStream())
            using (var archive = new ZipArchive(stream))
            {
                var innerFile = archive.GetEntry("answers.txt");
                // do something with the inner file
            }


            return Results.Ok();
        }
    }
}
