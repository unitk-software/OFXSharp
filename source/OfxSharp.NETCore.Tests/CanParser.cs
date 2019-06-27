using FluentAssertions;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;

namespace OfxSharp.NETCore.Tests
{
    public class CanParser
    {
        private OFXDocumentParser parser;

        [SetUp]
        public void SetUp()
        {
            parser = new OFXDocumentParser();
        }

        [Test]
        public void CanParserItau()
        {
            var ofxFile = new FileStream(@"Files/itau.ofx", FileMode.Open);

            var ofxDocument = parser.Import(ofxFile);

            ofxDocument.Should().NotBeNull();
            ofxDocument.StatementStart.Should().Be(DateTime.Parse("05/12/2013 00:00:00"));
            ofxDocument.StatementEnd.Should().Be(DateTime.Parse("28/02/2014 00:00:00"));
            ofxDocument.Account.AccountId.Trim().Should().Be("9999999999");
            ofxDocument.Account.BankId.Trim().Should().Be("0341");
            ofxDocument.Transactions.Count.Should().Be(3);
            ofxDocument.Transactions.Sum(x => x.Amount).Should().Be(-644.44M);
        }

        [Test]
        public void CanParserSantander()
        {
            var ofxFile = new FileStream(@"Files/santander.ofx", FileMode.Open);

            var ofxDocument = parser.Import(ofxFile);

            ofxDocument.Should().NotBeNull();
            ofxDocument.StatementStart.Should().Be(DateTime.Parse("03/02/2014 00:00:00"));
            ofxDocument.StatementEnd.Should().Be(DateTime.Parse("03/02/2014 00:00:00"));
            ofxDocument.Account.AccountId.Trim().Should().Be("9999999999999");
            ofxDocument.Account.BankId.Trim().Should().Be("033");
            ofxDocument.Transactions.Count.Should().Be(3);
            ofxDocument.Transactions.Sum(x => x.Amount).Should().Be(-22566.44M);
        }

        [Test]
        public void CanParserBradesco()
        {
            var ofxFile = new FileStream(@"Files/bradesco.ofx", FileMode.Open);

            var ofxDocument = parser.Import(ofxFile);

            ofxDocument.Should().NotBeNull();
            ofxDocument.StatementStart.Should().Be(DateTime.Parse("09/05/2019 00:00:00"));
            ofxDocument.StatementEnd.Should().Be(DateTime.Parse("09/05/2019 00:00:00"));
            ofxDocument.Account.AccountId.Should().Be("99999");
            ofxDocument.Account.BankId.Should().Be("0237");
            ofxDocument.Transactions.Count.Should().Be(3);
            ofxDocument.Transactions.Sum(x => x.Amount).Should().Be(200755M);
        }
    }
}