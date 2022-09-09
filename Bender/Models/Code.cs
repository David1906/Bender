using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bender.Models
{
    public class Code
    {
        private string Text { get; set; } = "";

        public Code(string text)
        {
            Text = text;
        }

        public PackageInfo Decode(List<CodeChunk> Format)
        {
            var packageInfo = new PackageInfo();
            var textCopy = this.Text;
            foreach (var chunk in Format)
            {
                var terminatorIndex = textCopy.IndexOf(chunk.Terminator);
                if (String.IsNullOrEmpty(chunk.Terminator))
                {
                    terminatorIndex = textCopy.Length;
                }
                if (terminatorIndex > -1)
                {
                    packageInfo.SetPropByName(chunk.Property, textCopy.Substring(0, terminatorIndex));
                    textCopy = textCopy.Remove(0, terminatorIndex + chunk.Terminator.Length);
                }
            }
            return packageInfo;
        }
    }
}
