using System.Web;
using Api.Models;

namespace Api.Validators
{
    public interface IFileUploadValidator
    {
        ResultModel IsValid(HttpPostedFile httpPostedFile);
    }
}
