using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace eventTicketPesentation.Data
{
    public static class FileUtil
    {
        public static  ValueTask<object> SaveAs(this IJSRuntime  js, string fileName, byte[] data)
            =>  js.InvokeAsync<object>("saveAsFile", fileName, Convert.ToBase64String(data));
    }
}