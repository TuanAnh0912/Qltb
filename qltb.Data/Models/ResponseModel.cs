using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qltb.Data.Models
{
    public class ResponseModel
    {
        public ResponseModel()
        {

        }
        public ResponseModel(bool? _status, string _alert, string _messages)
        {
            status = _status;
            alert = _alert;
            messages = _messages;
            index = null;
            data = null;
        }
        public ResponseModel(bool? _status, string _alert, string _messages, int? _index, string _data)
        {
            status = _status;
            alert = _alert;
            messages = _messages;
            index = _index;
            data = _data;
        }
        public Nullable<bool> status { get; set; }
        public string alert { get; set; }
        public string messages { get; set; }
        public Nullable<int> index { get; set; }
        public string data { get; set; }
        public List<string> list_message { get; set; }
    }
}
