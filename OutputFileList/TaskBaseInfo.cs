using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;

namespace OutputFileList
{
    public abstract class TaskBaseInfo
    {
        public int Count { get; private protected set; }
        public IList<TaskData> TaskInfoData { get; private protected set; }

        private protected bool VerifyAuthenticodeSignature(string path)
        {
            if (path.Contains('%') && !path.Contains("\""))
            {
                var beginIndex = path.IndexOf('%');
                var endIndex = path.LastIndexOf('%');
                var x = path[endIndex..].Length - 1;
                var envVarName = path.Substring(beginIndex, path.Length - x).Trim('%');

                path = Path.Combine(Environment.GetEnvironmentVariable(envVarName), path[(endIndex + 2)..]);
            }


            using var ps = PowerShell.Create();
            ps.Commands.AddScript("Get-AuthenticodeSignature \"" + path + "\"");
            var results = ps.Invoke();

            if (results.Count == 0)
            {
                return false;
            }
            var signature = (Signature)results.Single().BaseObject;
            return (signature.Status == SignatureStatus.Valid);
        }

        private protected StreamReader StringToStream(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return new StreamReader(stream);
        }
    }
}