using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;

namespace Api.Models
{
    public class ResultModel
    {
        public ResultModel()
        {
            Success = true;
            Messages = new List<string>();
        }

        public object Data { get; set; }

        public bool Success { get; set; }

        public List<string> Messages { get; set; }

        public string Message
        {
            get { return Messages.Count == 0 ? null : Messages[0]; }
            set { Messages.Add(value); }
        }
    }
}