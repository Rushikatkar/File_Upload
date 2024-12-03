using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace File_Upload.Controllers
{
    public class UploadFileController : ApiController
    {
        // Allowed file types and size limit (5MB)
        private static readonly string[] AllowedFileTypes = { ".jpg", ".png", ".pdf" };
        private const int MaxFileSize = 5 * 1024 * 1024; // 5 MB
        private const string UploadDirectory = "~/Uploads/";

        [HttpPost]
        [Route("api/files/upload")]
        public IHttpActionResult UploadFile()
        {
            try
            {
                // Check if the request contains a file
                if (!HttpContext.Current.Request.Files.AllKeys.Any())
                    return BadRequest("No file was uploaded.");

                var file = HttpContext.Current.Request.Files["file"];
                if (file == null)
                    return BadRequest("File is required.");

                // Check if the file size exceeds the max file size before any processing
                if (file.ContentLength > MaxFileSize)
                {
                    var errorMessage = $"The file size exceeds the maximum allowed size of {MaxFileSize / (1024 * 1024)} MB. Please upload a smaller file.";
                    return Content(HttpStatusCode.RequestEntityTooLarge, new
                    {
                        status = "error",
                        message = errorMessage
                    });
                }

                // Validate file type
                var fileExtension = Path.GetExtension(file.FileName).ToLower();
                if (!AllowedFileTypes.Contains(fileExtension))
                    return BadRequest("Unsupported file type. Allowed types are: .jpg, .png, .pdf.");

                // Sanitize file name and generate a unique name
                var sanitizedFileName = Path.GetFileNameWithoutExtension(file.FileName);
                var uniqueFileName = $"{Guid.NewGuid()}_{sanitizedFileName}{fileExtension}";

                // Determine the full path to save the file
                var uploadPath = HttpContext.Current.Server.MapPath(UploadDirectory);
                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);

                var filePath = Path.Combine(uploadPath, uniqueFileName);

                // Save the file to the server
                file.SaveAs(filePath);

                // Return success response with metadata
                return Ok(new
                {
                    status = "success",
                    message = "File uploaded successfully.",
                    data = new
                    {
                        fileName = uniqueFileName,
                        filePath = $"/Uploads/{uniqueFileName}",
                        fileSize = $"{file.ContentLength / 1024} KB",
                        fileType = file.ContentType
                    }
                });
            }
            catch (HttpException ex) when (ex.GetHttpCode() == 413) // Request Entity Too Large
            {
                // Handle the specific case for file size exceeding request limit
                return Content(HttpStatusCode.RequestEntityTooLarge, new
                {
                    status = "error",
                    message = "The file size exceeds the server's maximum upload size limit. Please upload a smaller file."
                });
            }
            catch (Exception ex)
            {
                // Handle general errors
                return InternalServerError(new Exception("An error occurred while uploading the file.", ex));
            }
        }
    }
}
