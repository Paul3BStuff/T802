using FormulaRunner.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace T802.Tests
{
    [TestClass]
    public class SampleRunnerTests
    {
        [TestCategory("Formula")]
        [TestMethod]
        public void ShouldReturnAnEmptyListIfNoFormulasProvided()
        {
            var samples = new List<Sample> { new Sample("", "", "") };
            var formulas = new List<IFormula>();
            var testRunner = new TestRunner(samples, formulas);
            var results = testRunner.Execute();
            Assert.AreEqual(0, results.Count);
        }

        [TestCategory("Formula")]
        [TestMethod]
        public void ShouldReturnAnEmptyListIfNoSamplesProvided()
        {
            var samples = new List<Sample>();
            var formulas = new List<IFormula>() { new NullFormula() };
            var testRunner = new TestRunner(samples, formulas);
            var results = testRunner.Execute();
            Assert.AreEqual(0, results.Count);
        }

        [TestCategory("Sentence")]
        [TestMethod]
        public void ShouldHaveZeroSentenceCountWhenEmpty()
        {
            var samples = new List<Sample> { new Sample("Test Sample", "", "") };
            var formulas = new List<IFormula> { new NullFormula() };
            var testRunner = new TestRunner(samples, formulas);
            var result = testRunner.Execute().First();
            Assert.AreEqual(0, result.Sentences.Count);
        }

        [TestCategory("Sentence")]
        [TestMethod]
        public void ShouldSetSentenceToOneForASingleSentence()
        {
            var samples = new List<Sample> { new Sample("Test Sample", "", "Lorem ipsum dolor sit amet, consectetur adipiscing elit.") };
            var formulas = new List<IFormula> { new NullFormula() };
            var testRunner = new TestRunner(samples, formulas);
            var result = testRunner.Execute().First();
            Assert.AreEqual(1, result.Sentences.Count);
        }

        [TestCategory("Sentence")]
        [TestMethod]
        public void ShouldSetSentenceToTwoForTwoSentences()
        {
            var samples = new List<Sample> { new Sample("Test Sample", "", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin egestas nunc quis faucibus consectetur.") };
            var formulas = new List<IFormula> { new NullFormula() };
            var testRunner = new TestRunner(samples, formulas);
            var result = testRunner.Execute().First();
            Assert.AreEqual(2, result.Sentences.Count);
        }

        [TestCategory("Word")]
        [TestMethod]
        public void ShouldCalculateAverageWordsPerSentence()
        {
            var samples = new List<Sample>
            {
                new Sample("Test Sample", "",
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin egestas nunc quis faucibus consectetur. Integer tincidunt ante in nunc sagittis, nec pharetra ex tristique.")
            };
            var formulas = new List<IFormula> { new NullFormula() };
            var testRunner = new TestRunner(samples, formulas);
            var result = testRunner.Execute().First();
            Assert.AreEqual(3, result.Sentences.Count);
            var actualRoundedToTwoDecimals = Math.Round(result.AverageWordsPerSentence, 2);
            Assert.AreEqual(8.00, actualRoundedToTwoDecimals);
        }

        [TestCategory("Sentence")]
        [TestMethod]
        public void ShouldCalculateAverageCharactersPerSentence()
        {
            var samples = new List<Sample>
            {
                new Sample("Test Sample", "",
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin egestas nunc quis faucibus consectetur. Integer tincidunt ante in nunc sagittis, nec pharetra ex tristique.")
            };
            var formulas = new List<IFormula> { new NullFormula() };
            var testRunner = new TestRunner(samples, formulas);
            var result = testRunner.Execute().First();
            Assert.AreEqual(3, result.Sentences.Count);
            var actualRoundedToTwoDecimals = Math.Round(result.AverageCharCountPerSentenceWord, 2);
            Assert.AreEqual(47, actualRoundedToTwoDecimals);
        }

        [TestCategory("Word")]
        [TestMethod]
        public void ShouldReturnCorrectCountOfWordsWhenEmpty()
        {
            var samples = new List<Sample> { new Sample("Test Sample", "", "") };
            var formulas = new List<IFormula> { new NullFormula() };
            var testRunner = new TestRunner(samples, formulas);
            var result = testRunner.Execute().First();
            Assert.AreEqual(0, result.WordCount);
        }

        [TestCategory("Word")]
        [TestMethod]
        public void ShouldReturnAverageNumberOfWordsWhenEmpty()
        {
            var samples = new List<Sample> { new Sample("Test Sample", "", "") };
            var formulas = new List<IFormula> { new NullFormula() };
            var testRunner = new TestRunner(samples, formulas);
            var result = testRunner.Execute().First();
            Assert.AreEqual(0, result.AverageWordsPerSentence);
        }

        [TestCategory("Word")]
        [TestMethod]
        public void ShouldReturnCountOfWords()
        {
            var samples = new List<Sample> { new Sample("Test Sample", "", "Lorem ipsum dolor sit amet, consectetur adipiscing elit.") };
            var formulas = new List<IFormula> { new NullFormula() };
            var testRunner = new TestRunner(samples, formulas);
            var result = testRunner.Execute().First();
            Assert.AreEqual(8, result.WordCount);
        }

        [TestCategory("Word")]
        [TestMethod]
        public void ShouldReturnAverageWordCountOfOfEmptySample()
        {
            var samples = new List<Sample> { new Sample("Test Sample", "", "") };
            var formulas = new List<IFormula> { new NullFormula() };
            var testRunner = new TestRunner(samples, formulas);
            var result = testRunner.Execute().First();
            Assert.AreEqual(0, result.AverageWordsPerSentence);
        }

        [TestCategory("Character")]
        [TestMethod]
        public void ShouldReturnAverageCharCountPerWordOfEmptySample()
        {
            var samples = new List<Sample> { new Sample("Test Sample", "", "") };
            var formulas = new List<IFormula> { new NullFormula() };
            var testRunner = new TestRunner(samples, formulas);
            var result = testRunner.Execute().First();
            Assert.AreEqual(0, result.AverageCharCountPerWord);
        }

        [TestCategory("Character")]
        [TestMethod]
        public void ShouldReturnAverageCharCountPerWord()
        {
            var samples = new List<Sample>
            { new Sample("Test Sample", "",
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit.")
            };
            var formulas = new List<IFormula> { new NullFormula() };
            var testRunner = new TestRunner(samples, formulas);
            var result = testRunner.Execute().First();
            var resultRoundedToTwoDecimalPlaces = Math.Round(result.AverageCharCountPerWord, 2);
            Assert.AreEqual(5.88, resultRoundedToTwoDecimalPlaces);
        }

        [TestCategory("Character")]
        [TestMethod]
        public void ShouldReturnZeroCharacterCountOnEmptySample()
        {
            var samples = new List<Sample> { new Sample("Test Sample", "", "") };
            var formulas = new List<IFormula> { new NullFormula() };
            var testRunner = new TestRunner(samples, formulas);
            var result = testRunner.Execute().First();
            Assert.AreEqual(0, result.CharacterCount);
        }

        [TestCategory("Character")]
        [TestMethod]
        public void ShouldReturnZeroAlphaNumericCharacterCountOnEmptySample()
        {
            var samples = new List<Sample> { new Sample("Test Sample", "", "") };
            var formulas = new List<IFormula> { new NullFormula() };
            var testRunner = new TestRunner(samples, formulas);
            var result = testRunner.Execute().First();
            Assert.AreEqual(0, result.CharacterCount);
        }

        [TestCategory("Character")]
        [TestMethod]
        public void ShouldReturnNumberOfCharactersInSampleExcludingWhitespace()
        {
            const string SampleText = "Lorem ipsum.";
            var samples = new List<Sample> { new Sample("Test Sample", string.Empty, SampleText) };
            var formulas = new List<IFormula> { new NullFormula() };
            var testRunner = new TestRunner(samples, formulas);
            var result = testRunner.Execute().First();
            Assert.AreEqual(11, result.CharacterCount);
        }

        [TestCategory("Character")]
        [TestMethod]
        public void ShouldReturnZeroForStringOfWhitespace()
        {
            const string SampleText = "  ";
            var samples = new List<Sample> { new Sample("Test Sample", string.Empty, SampleText) };
            var formulas = new List<IFormula> { new NullFormula() };
            var testRunner = new TestRunner(samples, formulas);
            var result = testRunner.Execute().First();
            Assert.AreEqual(00, result.CharacterCount);
        }

        [TestCategory("Character")]
        [TestMethod]
        public void ShouldReturnTotalAlphaNumbericCharsInSample()
        {
            const string SampleText = "Lorem ipsum.";
            var samples = new List<Sample> { new Sample("Test Sample", string.Empty, SampleText) };
            var formulas = new List<IFormula> { new NullFormula() };
            var testRunner = new TestRunner(samples, formulas);
            var result = testRunner.Execute().First();
            Assert.AreEqual(SampleText.Count(chr => char.IsLetterOrDigit(chr)), result.AlphaNumericCharacterCount);
        }

        [TestCategory("Character")]
        [TestMethod]
        public void ShouldCountCharactersInWords()
        {
            var samples = new List<Sample>
            {
                new Sample(
                    "Test Sample", string.Empty,
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit."
                    )
            };
            var formulas = new List<IFormula> { new NullFormula() };
            var testRunner = new TestRunner(samples, formulas);
            var result = testRunner.Execute().First();
            Assert.AreEqual(47, result.AlphaNumericCharacterCount);
        }

        [TestCategory("Sentence")]
        [TestMethod]
        public void ShouldCalculateZeroAverageAlphaNumericForAnEmptySentence()
        {
            var samples = new List<Sample>
            {
                new Sample("Test Sample", string.Empty, string.Empty)
            };
            var formulas = new List<IFormula> { new NullFormula() };
            var testRunner = new TestRunner(samples, formulas);
            var result = testRunner.Execute().First();
            Assert.AreEqual(0, result.Sentences.Count);
            var actualRoundedToTwoDecimals = Math.Round(result.AverageCharCountPerSentenceWord, 2);
            Assert.AreEqual(0.0, actualRoundedToTwoDecimals);
        }

        [TestCategory("Syblification")]
        [TestMethod]
        public void ShouldGetCountOfSyllablesForSample()
        {
            var samples = new List<Sample>
            {
                new Sample("Test Sample", string.Empty, "Program")
            };
            var formulas = new List<IFormula> { new NullFormula() };
            var testRunner = new TestRunner(samples, formulas);
            var result = testRunner.Execute().First();
            Assert.AreEqual(2, result.SyllableCount);
        }

        [TestCategory("Sentence")]
        [TestMethod]
        public void ShouldGetCountOfSentences()
        {
            var samples = new List<Sample>
            {
                new Sample("Test Sample", string.Empty, "Lorem ipsum! Lorem ipsum? Lorem ipsum.")
            };
            var formulas = new List<IFormula> { new NullFormula() };
            var testRunner = new TestRunner(samples, formulas);
            var result = testRunner.Execute().First();
            Assert.AreEqual(3, result.Sentences.Count());
        }
    }

    class NullFormula : IFormula
    {
        public string Name
        {
            get
            {
                return "A test formula";
            }
        }

        public int WordCount => 0;

        public double Evaluate(string text)
        {
            return 1.0;
        }
    }

}
