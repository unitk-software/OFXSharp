using System.IO;
using OfxSharp.NETStandard;

namespace OfxSharp.NETCore.Tests
{
    public class CanParser
    {
        public void CanParserItau()
        {
            var parser = new OFXDocumentParser();
            var ofxDocument = parser.Import(new FileStream(@"itau.ofx", FileMode.Open));
        }

        public void CanParserSantander()
        {
            var parser = new OFXDocumentParser();
            var ofxDocument = parser.Import(new FileStream(@"santander.ofx", FileMode.Open));
        }
    }
}