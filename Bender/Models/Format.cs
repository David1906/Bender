using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bender.Extensions;

namespace Bender.Models
{

    public class Format
    {
        public List<CodeChunk> Items = new List<CodeChunk>();

        public Format(List<CodeChunk> items)
        {
            this.Items = items;
            this.SetFormatIndexes();
        }

        public void SwapUp(CodeChunk codeChunk)
        {
            var sourceIndex = this.Items.FindIndex(x => x == codeChunk);
            var targetIndex = sourceIndex - 1;
            if (sourceIndex >= 1 && targetIndex >= 0)
            {
                this.Items.Swap(sourceIndex, targetIndex);
            }
            this.SetFormatIndexes();
        }

        public void SwapDown(CodeChunk codeChunk)
        {
            var sourceIndex = this.Items.FindIndex(x => x == codeChunk);
            var targetIndex = sourceIndex + 1;
            if (sourceIndex >= 0 && targetIndex < this.Items.Count)
            {
                this.Items.Swap(sourceIndex, targetIndex);
            }
            this.SetFormatIndexes();
        }

        private void SetFormatIndexes()
        {
            for (int i = 0; i < this.Items.Count; i++)
            {
                this.Items[i].Index = i + 1;
            }
        }

        public void SetProp(string PropName, string value)
        {
            var codeChunck = this.Items.Find(x => x.Property == PropName);
            if (codeChunck != null)
            {
                codeChunck.Value = value;
            }
        }
    }
}
