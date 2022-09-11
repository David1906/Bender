using Bender.Models;

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
            var code = new Code("0,1,2,3,4,5,6,7,8,9\n");
            var format = new List<LabelItem>
            {
                new LabelItem(){PropertyName = "Supplier", Terminator = ","},
                new LabelItem(){PropertyName = "Model", Terminator = ","},
                new LabelItem(){PropertyName = "Rev", Terminator = ","},
                new LabelItem(){PropertyName = "SupplierPn", Terminator = ","},
                new LabelItem(){PropertyName = "Qty", Terminator = ","},
                new LabelItem(){PropertyName = "HhPn", Terminator = ","},
                new LabelItem(){PropertyName = "DateCode", Terminator = ","},
                new LabelItem(){PropertyName = "LotNo", Terminator = ","},
                new LabelItem(){PropertyName = "PkgId", Terminator = ","},
                new LabelItem(){PropertyName = "WorkOrder", Terminator = "\n"}
            };

            var result = code.Decode(format);

            Assert.That(result.Supplier, Is.EqualTo("0"));
            Assert.That(result.Model, Is.EqualTo("1"));
            Assert.That(result.Rev, Is.EqualTo("2"));
            Assert.That(result.SupplierPn, Is.EqualTo("3"));
            Assert.That(result.Qty, Is.EqualTo("4"));
            Assert.That(result.HhPn, Is.EqualTo("5"));
            Assert.That(result.DateCode, Is.EqualTo("6"));
            Assert.That(result.LotNo, Is.EqualTo("7"));
            Assert.That(result.PkgId, Is.EqualTo("8"));
            Assert.That(result.WorkOrder, Is.EqualTo("9"));
        }

        [Test]
        public void Decode_NoTerminator()
        {
            var codeText = "0,1,2,3,4,5,6,7,8,9\n";
            var code = new Code(codeText);
            var format = new List<LabelItem>
            {
                new LabelItem(){PropertyName = "Supplier", Terminator = ""}
            };

            var result = code.Decode(format);

            Assert.That(result.Supplier, Is.EqualTo(codeText));
        }
    }
}