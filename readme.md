# How to use

```C#
var parser = new OFXDocumentParser();
var ofxDocument = parser.Import(new FileStream(@"c:\ofxdoc.ofx", FileMode.Open));
```