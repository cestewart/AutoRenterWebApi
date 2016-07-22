using System;
using System.IO;
using System.Reflection;
using System.Web;

namespace Api.Tests
{
    public static class GetTestObjects
    {
        public static HttpPostedFile ConstructHttpPostedFile(byte[] data, string filename, string contentType)
        {
            var systemWebAssembly = typeof(HttpPostedFileBase).Assembly;
            var typeHttpRawUploadedContent = systemWebAssembly.GetType("System.Web.HttpRawUploadedContent");
            var typeHttpInputStream = systemWebAssembly.GetType("System.Web.HttpInputStream");

            Type[] uploadedParams = { typeof(int), typeof(int) };
            Type[] streamParams = { typeHttpRawUploadedContent, typeof(int), typeof(int) };
            Type[] parameters = { typeof(string), typeof(string), typeHttpInputStream };

            var uploadedContent = typeHttpRawUploadedContent
              .GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, uploadedParams, null)
              .Invoke(new object[] { data.Length, data.Length });

            typeHttpRawUploadedContent
              .GetMethod("AddBytes", BindingFlags.NonPublic | BindingFlags.Instance)
              .Invoke(uploadedContent, new object[] { data, 0, data.Length });

            typeHttpRawUploadedContent
              .GetMethod("DoneAddingBytes", BindingFlags.NonPublic | BindingFlags.Instance)
              .Invoke(uploadedContent, null);

            object stream = (Stream)typeHttpInputStream
              .GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, streamParams, null)
              .Invoke(new[] { uploadedContent, 0, data.Length });

            var postedFile = (HttpPostedFile)typeof(HttpPostedFile)
              .GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, parameters, null)
              .Invoke(new[] { filename, contentType, stream });

            return postedFile;
        }
    }
}
