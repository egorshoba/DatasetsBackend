using DatasetsBackend.Data;
using DatasetsBackend.Dtos;
using DatasetsBackend.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace DatasetsBackend.Controllers
{
    public static class DatasetsController
    {
        [Consumes("multipart/form-data")]
        public static IResult Upload(HttpRequest request)
        {
            var form = request.ReadFormAsync().Result;

            var model = CustomDeserializer.DeserializeForm<UploadDatasetDto>(form);

            return Results.Ok();
        }
    }
}
