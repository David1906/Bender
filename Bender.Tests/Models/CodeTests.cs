using Bender.Models;
using System.ComponentModel;

namespace Bender.Tests.Models
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Decode_ValidCode()
        {
            var format = new BindingList<LabelItem>
            {
                new LabelItem(){Key = "Supplier", Terminator = Enums.Terminators.Comma},
                new LabelItem(){Key = "Model", Terminator = Enums.Terminators.Comma},
                new LabelItem(){Key = "Rev", Terminator = Enums.Terminators.Comma},
                new LabelItem(){Key = "SupplierPn", Terminator = Enums.Terminators.Comma},
                new LabelItem(){Key = "Qty", Terminator = Enums.Terminators.Comma},
                new LabelItem(){Key = "HhPn", Terminator = Enums.Terminators.Comma},
                new LabelItem(){Key = "DateCode", Terminator = Enums.Terminators.Comma},
                new LabelItem(){Key = "LotNo", Terminator = Enums.Terminators.Comma},
                new LabelItem(){Key = "PkgId", Terminator = Enums.Terminators.Comma},
                new LabelItem(){Key = "WorkOrder", Terminator = Enums.Terminators.CR}
            };
            var label = new Label(format);
            label.Code = "0,1,2,3,4,5,6,7,8,9\n";

            label.Decode();

            Assert.That(label.Items[0].Value, Is.EqualTo("0"));
            Assert.That(label.Items[1].Value, Is.EqualTo("1"));
            Assert.That(label.Items[2].Value, Is.EqualTo("2"));
            Assert.That(label.Items[3].Value, Is.EqualTo("3"));
            Assert.That(label.Items[4].Value, Is.EqualTo("4"));
            Assert.That(label.Items[5].Value, Is.EqualTo("5"));
            Assert.That(label.Items[6].Value, Is.EqualTo("6"));
            Assert.That(label.Items[7].Value, Is.EqualTo("7"));
            Assert.That(label.Items[8].Value, Is.EqualTo("8"));
            Assert.That(label.Items[9].Value, Is.EqualTo("9"));
        }

        [Test]
        public void Decode_NoTerminator()
        {
            var codeText = "0,1,2,3,4,5,6,7,8,9\n";
            var code = new Code(codeText);
            var format = new List<LabelItem>
            {
                new LabelItem(){Key = "Supplier", Terminator = ""}
            };

            var result = code.Decode(format);

            Assert.That(result.Supplier, Is.EqualTo(codeText));
        }
    }
}