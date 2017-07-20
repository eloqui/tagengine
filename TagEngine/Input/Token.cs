using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

using TagEngine.Data;

namespace TagEngine.Input
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
        /// The position of this token in the input line (0-based)
        /// </summary>
        public int Position;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="word">The word string</param>
		/// <param name="status">The status of the word</param>
        /// <param name="position">The position this token was in the input</param>
		public Token(string word, TokenStatus status, int position)
		{
			Word = word;
			Status = status;
            Position = position;
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool operator ==(Token x, Token y)
        {
            return (x.Word == y.Word && x.Status == y.Status && x.Position == y.Position);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool operator !=(Token x, Token y)
        {
            return !(x.Word == y.Word && x.Status == y.Status && x.Position == y.Position);
        }
	}

	/// <summary>
	/// Collection of tokens
	/// </summary>
	public class Tokeniser : IEnumerable
	{
		#region Fields & Properties

		/// <summary>
		/// The individual tokens
		/// </summary>
		private List<Token> tokens;
        
        /// <summary>
        /// Count of words ignored
        /// </summary>
        public int IgnoreCount { get; protected set; } = 0;

        /// <summary>
        /// Count of parsed words
        /// </summary>
        public int WordCount { get; protected set; } = 0;

        /// <summary>
        /// The detected command word
        /// </summary>
        public Token Command { get; protected set; }

		/// <summary>
		/// Get a list of unrecognised tokens
		/// </summary>
		public List<Token> Unrecognised
		{
			get
			{
                var unrecognised = from token in tokens
                                   where token.Status == TokenStatus.Unrecognised
                                   select token;
                return unrecognised.ToList();
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

        /// <summary>
        /// Get the token at a position
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public Token GetTokenAtPosition(int position)
        {
            if (position < 0 || position >= tokens.Count) throw new ArgumentOutOfRangeException("No token at position" + position.ToString());

            return tokens[position];
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

            int position = 0;
			foreach (string el in elements)
			{
				// ensure the word is in lower case and has no space
				string element = el.ToLower().Trim();

				if (String.IsNullOrWhiteSpace(element)) continue;

				if (WordStore.IsIgnored(element))
				{
					// token added as an ignored word
					tokens.Add(new Token(element, TokenStatus.Ignored, position));
					IgnoreCount++;
				}
				else
				{
					// this will add "back" as both a direction and a command
					// but that doesn't matter (allows "go back" and "back")
					if (WordStore.IsDirection(element))
					{
						tokens.Add(new Token(element, TokenStatus.Direction, position));
					}
                    // this is a command, but only accept the first command found
					else if (CommandManager.IsCommand(element, position) && Command == default(Token))
					{
						Command = new Token(element, TokenStatus.Command, position);
						tokens.Add(Command);
					}
					else
					{
						tokens.Add(new Token(element, TokenStatus.Unrecognised, position));
					}
				}

                position++; // TODO: these two vars could be consolidated, but does it look stupid passing WordCount as position?
				WordCount++;
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