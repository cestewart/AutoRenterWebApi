using System.Linq;
using System.Web;
using Api.Models;

namespace Api.Validators
{
    public class FileUploadValidator : IFileUploadValidator
    {
        private readonly IAutoRenterApiConfiguration _autoRenterApiConfiguration;

        public FileUploadValidator(IAutoRenterApiConfiguration autoRenterApiConfiguration)
        {
            _autoRenterApiConfiguration = autoRenterApiConfiguration;
        }

        public ResultModel IsValid(HttpPostedFile httpPostedFile)
        {
            var resultModel = new ResultModel();
            IsFileSmall(httpPostedFile.ContentLength, resultModel);
            IsFileTypeAccepted(httpPostedFile.ContentType, resultModel);
            return resultModel;
        }

        public virtual void IsFileSmall(int contentLength, ResultModel resultModel)
        {
            if (contentLength <= _autoRenterApiConfiguration.MaximumFileSizeInKb * 1024) return;
            resultModel.Success = false;
            resultModel.Messages.Add($"The file could not be uploaded because the file is too large.  The maximum file size allowed is {_autoRenterApiConfiguration.MaximumFileSizeInKb} kb.");
        }

        public virtual void IsFileTypeAccepted(string contentType, ResultModel resultModel)
        {
            var fileType = contentType.ToLower().Replace("image/", string.Empty);
            var acceptedFileTypes = _autoRenterApiConfiguration.AcceptedFileTypes.Split(',');
            if (acceptedFileTypes.Any(acceptedFileType => acceptedFileType.Trim().ToLower() == fileType)) return;
            resultModel.Success = false;
            resultModel.Messages.Add($"The file could not be uploaded because the file type is not accepted.  Accepted file types are {_autoRenterApiConfiguration.AcceptedFileTypes}.");
        }
    }
}