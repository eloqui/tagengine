using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

using TagEngine.Data;

namespace TagEngine.Parser
{
	/// <summary>
	/// Statuses of tokens
	/// </summary>
	public enum TokenStatus { Ignored, Unrecognised, Direction, Command };

	/// <summary>
	/// A parser token
	/// </summary>
	public struct Token
	{
		/// <summary>
		/// The word
		/// </summary>
		public string Word;

		/// <summary>
		/// The status of this token
		/// </summary>
		public TokenStatus Status;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="word">The word string</param>
		/// <param name="status">The status of the word</param>
		public Token(string word, TokenStatus status)
		{
			this.Word = word;
			this.Status = status;
		}
	}

	/// <summary>
	/// Collection of tokens
	/// </summary>
	public class Tokeniser : IEnumerable
	{
		#region Fields

		/// <summary>
		/// The individual tokens
		/// </summary>
		private List<Token> tokens;

		/// <summary>
		/// Count of ignored words
		/// </summary>
		private int ignoreCount = 0;

		/// <summary>
		/// Index of the detected command word
		/// </summary>
		private Token command;

		/// <summary>
		/// Count of parsed words
		/// </summary>
		private int wordCount = 0;

		#endregion

		#region Properties

		/// <summary>
		/// Gets the count of words ignored
		/// </summary>
		public int IgnoreCount
		{
			get { return ignoreCount; }
		}

		/// <summary>
		/// Gets the count of words parsed
		/// </summary>
		public int WordCount
		{
			get { return wordCount; }
		}

		/// <summary>
		/// Get the command word
		/// </summary>
		public Token Command
		{
			get { return command; }
		}

		/// <summary>
		/// Get a list of unrecognised tokens
		/// </summary>
		public List<Token> Unrecognised
		{
			get
			{
				List<Token> unrecognised = new List<Token>();
				foreach (Token t in tokens)
					if (t.Status == TokenStatus.Unrecognised)
						unrecognised.Add(t);
				return unrecognised;
			}
		}

		/// <summary>
		/// Get the direction from the command list
		/// </summary>
		public string Direction
		{
			get
			{
				foreach (Token t in tokens)
					if (t.Status == TokenStatus.Direction)
						return t.Word;
				return null;
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="source">The incoming string of tokens</param>
		public Tokeniser(string source)
			: this(source, new char[] { ' ', '\t', '\n', '_', ',', '.', ':', ';' })
		{ }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="source">The incoming string of tokens</param>
		/// <param name="delimiters">Delimiters to use when splitting source into tokens</param>
		public Tokeniser(string source, char[] delimiters)
		{
			// Parse the string into tokens:
			Tokenise(source.Split(delimiters));
		}

		#endregion

		#region Methods

		/// <summary>
		/// Declaration of the GetEnumerator() method required by IEnumerable
		/// </summary>
		public IEnumerator GetEnumerator()
		{
			return new TokenEnumerator(this);
		}

		#endregion

		#region Implementation

		/// <summary>
		/// Determine the type of each of the source elements and fill the tokens collection
		/// </summary>
		/// <param name="elements">Array of split elements</param>
		private void Tokenise(string[] elements)
		{
			tokens = new List<Token>(elements.Length);

			foreach (string el in elements)
			{
				// ensure the word is in lower case and has no space
				string element = el.ToLower().Trim();

				if (element.Equals(String.Empty)) continue;

				if (WordStore.IsIgnored(element))
				{
					// token added as an ignored word
					tokens.Add(new Token(element, TokenStatus.Ignored));
					ignoreCount++;
				}
				else
				{
					// this will add "back" as both a direction and a command
					// but that doesn't matter (allows "go back" and "back")
					if (WordStore.IsDirection(element))
					{
						tokens.Add(new Token(element, TokenStatus.Direction));
					}
					else if (WordStore.IsCommand(element))
					{
						command = new Token(element, TokenStatus.Command);
						tokens.Add(command);
					}
					else
					{
						tokens.Add(new Token(element, TokenStatus.Unrecognised));
					}
				}

				wordCount++;
			}
			elements = null;
		}

		#endregion

		/// <summary>
		/// Inner class implementing IEnumerator interface
		/// </summary>
		private class TokenEnumerator : IEnumerator
		{
			private int position = -1;
			private Tokeniser t;

			public TokenEnumerator(Tokeniser t)
			{
				this.t = t;
			}

			// Declare the MoveNext method required by IEnumerator:
			public bool MoveNext()
			{
				if (position < t.tokens.Count - 1)
				{
					position++;
					return true;
				}
				else
				{
					return false;
				}
			}

			// Declare the Reset method required by IEnumerator:
			public void Reset()
			{
				position = -1;
			}

			// Declare the Current property required by IEnumerator:
			public object Current
			{
				get
				{
					return t.tokens[position];
				}
			}
		}
	}

	/// <summary>
	/// Tests for the tokeniser
	/// </summary>
	[TestFixture]
	public class TokenTest
	{
		/// <summary>
		/// Test the basic tokenise functionality
		/// </summary>
		[Test]
		public void Tokenise()
		{
			Dictionary<string, string[]> tests = new Dictionary<string, string[]>();

			tests.Add("this is a test", new string[] { "this", "is", "a", "test" });
			tests.Add("this, too, is-also a_test", new string[] { "this", "too", "is-also", "a", "test" });

			foreach (KeyValuePair<string, string[]> kvp in tests)
			{
				Tokeniser ts = new Tokeniser(kvp.Key);

				Assert.That(ts.WordCount, Is.EqualTo(kvp.Value.Length));
				int ii = 0;
				foreach (Token t in ts)
				{
					//Console.Out.WriteLine(ii.ToString() + ": " + t.Word + "/" + t.Status.ToString());
					Assert.That(t.Word, Is.EqualTo(kvp.Value[ii]));
					ii++;
				}
				Assert.That(ii, Is.EqualTo(kvp.Value.Length));
			}
		}

		/// <summary>
		/// Test correct detection of token statuses
		/// </summary>
		[Test]
		public void CorrectTokenStatuses()
		{
			Dictionary<string, object[]> tests = new Dictionary<string, object[]>();

			tests.Add("get the ball north", new object[] { TokenStatus.Command, TokenStatus.Ignored, TokenStatus.Unrecognised, TokenStatus.Direction });
			tests.Add("go back", new object[] { TokenStatus.Command, TokenStatus.Direction });
			tests.Add("ball ball get ball", new object[] { TokenStatus.Unrecognised, TokenStatus.Unrecognised, TokenStatus.Command, TokenStatus.Unrecognised });

			foreach (KeyValuePair<string, object[]> kvp in tests)
			{
				Tokeniser ts = new Tokeniser(kvp.Key);
				int ii = 0;
				foreach (Token t in ts)
				{
					//Console.Out.WriteLine(ii.ToString() + ": " + t.Word + "/" + t.Status.ToString());
					Assert.That(t.Status, Is.EqualTo(kvp.Value[ii]));
					ii++;
				}
			}
		}
	}
}