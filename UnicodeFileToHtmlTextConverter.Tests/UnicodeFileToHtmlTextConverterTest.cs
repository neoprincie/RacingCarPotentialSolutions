using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace TDDMicroExercises.UnicodeFileToHtmlTextConverter.Tests
{
    public class HikerTest
    {
        [Fact]
        public void GetFileName_GivenFileAdapter_ShouldReturnFileName()
        {
            var mockFileAdapter = Substitute.For<IFileAdapter>();
            var converter = new UnicodeFileToHtmlTextConverter(mockFileAdapter);
            var expectedFileName = "foobar.txt";
            
            mockFileAdapter.GetFilePath().Returns(expectedFileName);

            converter.GetFilename().Should().Be(expectedFileName);
        }

        [Fact]
        public void ConvertToHtml_GivenBasicLine_ShouldReturnLineWithBreak()
        {
            var mockFileAdapter = Substitute.For<IFileAdapter>();
            var converter = new UnicodeFileToHtmlTextConverter(mockFileAdapter);

            mockFileAdapter.OpenText().Returns(new StringReader("Hello, World"));
            
            var actual = converter.ConvertToHtml();

            actual.Should().Be("Hello, World<br />");
        }

        [Fact]
        public void ConvertToHtml_GivenMultiLines_ShouldReturnMultiLinesWithBreaks()
        {
            var mockFileAdapter = Substitute.For<IFileAdapter>();
            var converter = new UnicodeFileToHtmlTextConverter(mockFileAdapter);

            mockFileAdapter.OpenText().Returns(new StringReader("Hello\r\nWorld"));
            
            var actual = converter.ConvertToHtml();

            actual.Should().Be("Hello<br />World<br />");
        }

        [Fact]
        public void ConvertToHtml_GivenLessThanSymbol_ShouldEncodeForHtml()
        {
            var mockFileAdapter = Substitute.For<IFileAdapter>();
            var converter = new UnicodeFileToHtmlTextConverter(mockFileAdapter);

            mockFileAdapter.OpenText().Returns(new StringReader("Hello <"));
            
            var actual = converter.ConvertToHtml();

            actual.Should().Be("Hello &lt;<br />");
        }
        
        [Fact]
        public void ConvertToHtml_GivenGreaterThanSymbol_ShouldEncodeForHtml()
        {
            var mockFileAdapter = Substitute.For<IFileAdapter>();
            var converter = new UnicodeFileToHtmlTextConverter(mockFileAdapter);

            mockFileAdapter.OpenText().Returns(new StringReader("Hello >"));
            
            var actual = converter.ConvertToHtml();

            actual.Should().Be("Hello &gt;<br />");
        }
        
        [Fact]
        public void ConvertToHtml_GivenAmpersandSymbol_ShouldEncodeForHtml()
        {
            var mockFileAdapter = Substitute.For<IFileAdapter>();
            var converter = new UnicodeFileToHtmlTextConverter(mockFileAdapter);

            mockFileAdapter.OpenText().Returns(new StringReader("Hello &"));
            
            var actual = converter.ConvertToHtml();

            actual.Should().Be("Hello &amp;<br />");
        }
        
        [Fact]
        public void ConvertToHtml_GivenDoubleQuote_ShouldEncodeForHtml()
        {
            var mockFileAdapter = Substitute.For<IFileAdapter>();
            var converter = new UnicodeFileToHtmlTextConverter(mockFileAdapter);

            mockFileAdapter.OpenText().Returns(new StringReader("Hello \""));
            
            var actual = converter.ConvertToHtml();

            actual.Should().Be("Hello &quot;<br />");
        }
        
        [Fact]
        public void ConvertToHtml_GivenSingleQuote_ShouldEncodeForHtml()
        {
            var mockFileAdapter = Substitute.For<IFileAdapter>();
            var converter = new UnicodeFileToHtmlTextConverter(mockFileAdapter);

            mockFileAdapter.OpenText().Returns(new StringReader("Hello '"));
            
            var actual = converter.ConvertToHtml();

            actual.Should().Be("Hello &quot;<br />");
        }
    }
}
