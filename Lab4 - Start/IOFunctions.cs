using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Lab03
{
	/// <summary>
	/// IOFunctions is a static class that demonstrates the usefulness of "semi-global" functions.
	/// Also, note the use of the static fields to hold state for the various display colors.
	/// This allows the "Get" functions to not require the prompt colors as parameters.
	/// </summary>
	public static class IOFunctions
	{
		#region Fields
		private static ConsoleColor m_PromptColor;
		private static ConsoleColor m_InputColor;
		private static ConsoleColor m_ErrorColor;
		#endregion Fields

		#region Constructors
		/// <summary>
		/// Initialize the static fields
		/// </summary>
		static IOFunctions()
		{
			// These could also have been set in the initializers for the fields...
			PromptColor = ConsoleColor.White;
			InputColor = ConsoleColor.Yellow;
			ErrorColor = ConsoleColor.Red;
		}
		#endregion Constructors

		#region Properties
		/// <summary>
		/// Gets/sets the Prompt Color
		/// </summary>
		public static ConsoleColor PromptColor
		{
			get
			{
				return m_PromptColor;
			}
			set
			{
				if (!Enum.IsDefined(typeof(ConsoleColor), value))
				{
					throw new ArgumentException("Invalid color!", "PromptColor");
				}
				m_PromptColor = value;
			}
		}

		/// <summary>
		/// Gets/sets the Input Color
		/// </summary>
		public static ConsoleColor InputColor
		{
			get
			{
				return m_InputColor;
			}
			set
			{
				if (!Enum.IsDefined(typeof(ConsoleColor), value))
				{
					throw new ArgumentException("Invalid color!", "InputColor");
				}
				m_InputColor = value;
			}
		}

		/// <summary>
		/// Gets/sets the Error Color
		/// </summary>
		public static ConsoleColor ErrorColor
		{
			get
			{
				return m_ErrorColor;
			}
			set
			{
				if (!Enum.IsDefined(typeof(ConsoleColor), value))
				{
					throw new ArgumentException("Invalid color!", "ErrorColor");
				}
				m_ErrorColor = value;
			}
		}
		#endregion Properties

		#region Public Methods
		/// <summary>
		/// Gets a bool from the user
		/// </summary>
		/// <param name="prompt">Prompt to display to the user</param>
		/// <returns>Bool value entered by user</returns>
		/// <remarks>Accepts 'y', 'yes', 'true' as true, the rest false</remarks>
		public static bool GetBoolFromUser(string prompt)
		{
			// Remember original console color so we can set it back when exiting
			ConsoleColor originalColor = Console.ForegroundColor;

			try
			{
				// Prompt and get user input
				Console.ForegroundColor = PromptColor;
				Console.Write(prompt);

				Console.ForegroundColor = InputColor;
				string input = Console.ReadLine().ToLower().Trim();
				if ((input == "y") || (input == "yes") || (input == "true"))
				{
					return true;
				}
				return false;
			}
			catch (Exception ex)
			{
				string location = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Namespace + "." + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
				Console.ForegroundColor = ErrorColor;
				Console.WriteLine("An unexpected error occurred in {0}: {1}", location, ex.Message);
				throw ex;
			}
			finally
			{
				Console.ForegroundColor = originalColor;
			}
		}

		/// <summary>
		/// Gets a string from the user
		/// </summary>
		/// <param name="prompt">Prompt to display to the user</param>
		/// <param name="maxLength">Maximum length of string to get (0 = unlimited)</param>
		/// <returns>String value entered by user</returns>
		public static string GetStringFromUser(string prompt, int maxLength)
		{
			// Remember original console color so we can set it back when exiting
			ConsoleColor originalColor = Console.ForegroundColor;

			try
			{
				// Prompt and get user input
				Console.ForegroundColor = PromptColor;
				Console.Write(prompt);

				Console.ForegroundColor = InputColor;
				string input = Console.ReadLine();
				if ((maxLength > 0) && (input.Length > maxLength))
				{
					input = input.Substring(0, maxLength);
				}

				return input;
			}
			catch (Exception ex)
			{
				string location = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Namespace + "." + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
				Console.ForegroundColor = ErrorColor;
				Console.WriteLine("An unexpected error occurred in {0}: {1}", location, ex.Message);
				throw ex;
			}
			finally
			{
				Console.ForegroundColor = originalColor;
			}
		}

		/// <summary>
		/// Gets an int from the user
		/// </summary>
		/// <param name="prompt">Prompt to display to the user</param>
		/// <param name="lowValue">Input value must be greater than or equal to this</param>
		/// <param name="highValue">Input value must be less than or equal to this</param>
		/// <returns>Integer value entered by the user within the expected range</returns>
		/// <remarks>This function will not exit until the user gives a valid input</remarks>
		public static int GetIntFromUser(string prompt, int lowValue, int highValue)
		{
			// Remember original console color so we can set it back when exiting
			ConsoleColor originalColor = Console.ForegroundColor;

			try
			{
				bool done = false;
				int value = 0;

				while (!done)
				{
					// Prompt and get user input
					Console.ForegroundColor = PromptColor;
					Console.Write(prompt);

					Console.ForegroundColor = InputColor;
					string input = Console.ReadLine();

					// Check for valid type
					if (int.TryParse(input, NumberStyles.AllowLeadingSign | NumberStyles.AllowThousands, NumberFormatInfo.CurrentInfo, out value))
					{
						// Good type, now check range
						if ((value >= lowValue) && (value <= highValue))
						{
							// This will allow the loop to exit.
							done = true;
						}
						else
						{
							// Out of range
							Console.ForegroundColor = ErrorColor;
							Console.WriteLine("The value entered was not between {0} and {1}.  Please try again.", lowValue, highValue);
						}
					}
					else
					{
						// Not the correct type
						Console.ForegroundColor = ErrorColor;
						Console.WriteLine("The value entered was not an integer.  Please try again.");
					}
				}
				return value;
			}
			catch (Exception ex)
			{
				string location = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Namespace + "." + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
				Console.ForegroundColor = ErrorColor;
				Console.WriteLine("An unexpected error occurred in {0}: {1}", location, ex.Message);
				throw ex;
			}
			finally
			{
				Console.ForegroundColor = originalColor;
			}
		}

		/// <summary>
		/// Gets a ulong from the user
		/// </summary>
		/// <param name="prompt">Prompt to display to the user</param>
		/// <param name="lowValue">Input value must be greater than or equal to this</param>
		/// <param name="highValue">Input value must be less than or equal to this</param>
		/// <returns>Integer value entered by the user within the expected range</returns>
		/// <remarks>This function will not exit until the user gives a valid input</remarks>
		public static ulong GetULongFromUser(string prompt, ulong lowValue, ulong highValue)
		{
			// Remember original console color so we can set it back when exiting
			ConsoleColor originalColor = Console.ForegroundColor;

			try
			{
				bool done = false;
				ulong value = 0;

				while (!done)
				{
					// Prompt and get user input
					Console.ForegroundColor = PromptColor;
					Console.Write(prompt);

					Console.ForegroundColor = InputColor;
					string input = Console.ReadLine();

					// Check for valid type
					if (ulong.TryParse(input, NumberStyles.AllowLeadingSign | NumberStyles.AllowThousands, NumberFormatInfo.CurrentInfo, out value))
					{
						// Good type, now check range
						if ((value >= lowValue) && (value <= highValue))
						{
							// This will allow the loop to exit.
							done = true;
						}
						else
						{
							// Out of range
							Console.ForegroundColor = ErrorColor;
							Console.WriteLine("The value entered was not between {0} and {1}.  Please try again.", lowValue, highValue);
						}
					}
					else
					{
						// Not the correct type
						Console.ForegroundColor = ErrorColor;
						Console.WriteLine("The value entered was not an unsigned integer.  Please try again.");
					}
				}
				return value;
			}
			catch (Exception ex)
			{
				string location = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Namespace + "." + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
				Console.ForegroundColor = ErrorColor;
				Console.WriteLine("An unexpected error occurred in {0}: {1}", location, ex.Message);
				throw ex;
			}
			finally
			{
				Console.ForegroundColor = originalColor;
			}
		}

		/// <summary>
		/// Gets a double from the user
		/// </summary>
		/// <param name="prompt">Prompt to display to the user</param>
		/// <param name="lowValue">Input value must be greater than or equal to this</param>
		/// <param name="highValue">Input value must be less than or equal to this</param>
		/// <returns>Double value entered by the user within the expected range</returns>
		/// <remarks>This function will not exit until the user gives a valid input</remarks>
		public static double GetDoubleFromUser(string prompt, double lowValue, double highValue)
		{
			// Remember original console color so we can set it back when exiting
			ConsoleColor originalColor = Console.ForegroundColor;

			try
			{
				bool done = false;
				double value = 0;

				while (!done)
				{
					// Prompt and get user input
					Console.ForegroundColor = PromptColor;
					Console.Write(prompt);

					Console.ForegroundColor = InputColor;
					string input = Console.ReadLine();

					// Check for valid type
					if (double.TryParse(input, NumberStyles.Number, NumberFormatInfo.CurrentInfo, out value))
					{
						// Good type, now check range
						if ((value >= lowValue) && (value <= highValue))
						{
							// This will allow the loop to exit.
							done = true;
						}
						else
						{
							// Out of range
							Console.ForegroundColor = ErrorColor;
							Console.WriteLine("The value entered was not between {0} and {1}.  Please try again.", lowValue, highValue);
						}
					}
					else
					{
						// Not the correct type
						Console.ForegroundColor = ErrorColor;
						Console.WriteLine("The value entered was not a double.  Please try again.");
					}
				}
				return value;
			}
			catch (Exception ex)
			{
				string location = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Namespace + "." + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
				Console.ForegroundColor = ErrorColor;
				Console.WriteLine("An unexpected error occurred in {0}: {1}", location, ex.Message);
				throw ex;
			}
			finally
			{
				Console.ForegroundColor = originalColor;
			}
		}

		/// <summary>
		/// Gets a date from the user
		/// </summary>
		/// <param name="prompt">Prompt to display to the user</param>
		/// <param name="lowValue">Input value must be greater than or equal to this</param>
		/// <param name="highValue">Input value must be less than or equal to this</param>
		/// <returns>DateTime value entered by the user within the expected range</returns>
		/// <remarks>This function will not exit until the user gives a valid input</remarks>
		public static DateTime GetDateFromUser(string prompt, DateTime lowValue, DateTime highValue)
		{
			// Remember original console color so we can set it back when exiting
			ConsoleColor originalColor = Console.ForegroundColor;

			try
			{
				bool done = false;
				DateTime value = DateTime.MinValue;

				while (!done)
				{
					// Prompt and get user input
					Console.ForegroundColor = PromptColor;
					Console.Write(prompt);

					Console.ForegroundColor = InputColor;
					string input = Console.ReadLine();

					// Check for valid type
					if (DateTime.TryParse(input, DateTimeFormatInfo.CurrentInfo, DateTimeStyles.AssumeLocal, out value))
					{
						// Good type, now check range
						if ((value >= lowValue) && (value <= highValue))
						{
							// This will allow the loop to exit.
							done = true;
						}
						else
						{
							// Out of range
							Console.ForegroundColor = ErrorColor;
							Console.WriteLine("The value entered was not between {0:d} and {1:d}.  Please try again.", lowValue, highValue);
						}
					}
					else
					{
						// Not the correct type
						Console.ForegroundColor = ErrorColor;
						Console.WriteLine("The value entered was not a date.  Please try again.");
					}
				}
				return value;
			}
			catch (Exception ex)
			{
				string location = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Namespace + "." + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
				Console.ForegroundColor = ErrorColor;
				Console.WriteLine("An unexpected error occurred in {0}: {1}", location, ex.Message);
				throw ex;
			}
			finally
			{
				Console.ForegroundColor = originalColor;
			}
		}

		/// <summary>
		/// Gets an enum value from the user
		/// </summary>
		/// <param name="prompt">Prompt to display to the user</param>
		/// <param name="enumType">Enumeration type value must belong to</param>
		/// <returns>Enum value entered by the user within the enum's range</returns>
		/// <remarks>This function will not exit until the user gives a valid input.  Also, the input can be either the number or text of the enum.</remarks>
		public static object GetEnumFromUser(string prompt, Type enumType)
		{
			// Remember original console color so we can set it back when exiting
			ConsoleColor originalColor = Console.ForegroundColor;

			try
			{
				bool done = false;
				object value = 0;

				while (!done)
				{
					// Prompt and get user input
					Console.ForegroundColor = PromptColor;
					Console.WriteLine(prompt);
					// Enumerate the values for the user
					Array enumValues = Enum.GetValues(enumType);
					foreach (object enumValue in enumValues)
					{
						Console.ForegroundColor = PromptColor;
						Console.Write("  [");
						Console.ForegroundColor = InputColor;
						Console.Write("{0:D}", enumValue);
						Console.ForegroundColor = PromptColor;
						Console.Write("] - ");
						Console.ForegroundColor = InputColor;
						Console.WriteLine("{0:G}", enumValue);
					}
					Console.ForegroundColor = PromptColor;
					Console.Write("Value: ");
					
					Console.ForegroundColor = InputColor;
					string input = Console.ReadLine();

					// Check for valid type
					try
					{
						value = Enum.Parse(enumType, input, true);  // Set to ignore case: fish == FISH
						if (Enum.IsDefined(enumType, value))
						{
							done = true;
						}
						else
						{
							// Out of range
							Console.ForegroundColor = ErrorColor;
							Console.WriteLine("The value entered does not exist in the value list.  Please try again.");
						}
					}
					catch (Exception)
					{
						// Out of range
						Console.ForegroundColor = ErrorColor;
						Console.WriteLine("The value entered does not exist in the value list.  Please try again.");
					}
				}
				return value;
			}
			catch (Exception ex)
			{
				string location = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Namespace + "." + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
				Console.ForegroundColor = ErrorColor;
				Console.WriteLine("An unexpected error occurred in {0}: {1}", location, ex.Message);
				throw ex;
			}
			finally
			{
				Console.ForegroundColor = originalColor;
			}
		}
		#endregion Public Methods
	}

}
