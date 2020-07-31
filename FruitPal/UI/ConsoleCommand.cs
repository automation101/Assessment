using System;
using System.Collections.Generic;
using System.Linq;

namespace FruitPal
{
    public class ConsoleCommand
    {
        public string Name { get; set; }
        private List<string> arguments { get; set; }
        public List<object> parameterValueList { get; set; } = new List<object>();

        public ConsoleCommand(string input)
        {
            var stringArray = input.Split(' ');

            arguments = new List<string>();
            for (int i = 0; i < stringArray.Length; i++)
            {
                // The first element is the command:
                if (i == 0)
                {
                    this.Name = stringArray[i].ToLower().Trim();
                }
                else
                {
                    string argument = stringArray[i];
                    arguments.Add(argument);
                }
            }
            Validate();
        }

        public void Validate()
        {
            //Validate the number of arguments, & argument types by the command name
            //Used switch here for extensibility to add other commands and cost calcs for different commodities
            switch (Name)
            {
                case "fruitpal":
                    if (arguments.Count() != 3)
                    {
                        throw new ArgumentException(
                            "Invalid number of arguments. Please use the fruitpal command with the 3 required arguments: <fruit> <price per ton ($)> <trade volume (tons)>");
                    }

                    // Coming from the console, all of our arguments are passed in as 
                    // strings. Parse to the required types                   
                    parameterValueList.Add(ParseArgument(typeof(string), arguments.ElementAt(0)));
                    parameterValueList.Add(ParseArgument(typeof(decimal), arguments.ElementAt(1)));
                    parameterValueList.Add(ParseArgument(typeof(decimal), arguments.ElementAt(2)));
                    break;

                default:
                    throw new ArgumentException($"Unrecognized command '{Name}'. Please enter a valid command.");
            }
        }

        static object ParseArgument(Type requiredType, string inputValue)
        {
            var requiredTypeCode = Type.GetTypeCode(requiredType);
            string exceptionMessage =
                string.Format("The value passed for argument '{0}' cannot be parsed to type '{1}'",
                inputValue, requiredType.Name);

            object result = null;
            switch (requiredTypeCode)
            {
                case TypeCode.String:
                    result = inputValue;
                    break;

                case TypeCode.Int16:
                    short number16;
                    if (Int16.TryParse(inputValue, out number16))
                    {
                        result = number16;
                    }
                    else
                    {
                        throw new ArgumentException(exceptionMessage);
                    }
                    break;

                case TypeCode.Int32:
                    int number32;
                    if (Int32.TryParse(inputValue, out number32))
                    {
                        result = number32;
                    }
                    else
                    {
                        throw new ArgumentException(exceptionMessage);
                    }
                    break;

                case TypeCode.Int64:
                    long number64;
                    if (Int64.TryParse(inputValue, out number64))
                    {
                        result = number64;
                    }
                    else
                    {
                        throw new ArgumentException(exceptionMessage);
                    }
                    break;

                case TypeCode.Boolean:
                    bool trueFalse;
                    if (bool.TryParse(inputValue, out trueFalse))
                    {
                        result = trueFalse;
                    }
                    else
                    {
                        throw new ArgumentException(exceptionMessage);
                    }
                    break;

                case TypeCode.Byte:
                    byte byteValue;
                    if (byte.TryParse(inputValue, out byteValue))
                    {
                        result = byteValue;
                    }
                    else
                    {
                        throw new ArgumentException(exceptionMessage);
                    }
                    break;

                case TypeCode.Char:
                    char charValue;
                    if (char.TryParse(inputValue, out charValue))
                    {
                        result = charValue;
                    }
                    else
                    {
                        throw new ArgumentException(exceptionMessage);
                    }
                    break;

                case TypeCode.DateTime:
                    DateTime dateValue;
                    if (DateTime.TryParse(inputValue, out dateValue))
                    {
                        result = dateValue;
                    }
                    else
                    {
                        throw new ArgumentException(exceptionMessage);
                    }
                    break;
                case TypeCode.Decimal:
                    Decimal decimalValue;
                    if (Decimal.TryParse(inputValue, out decimalValue))
                    {
                        result = decimalValue;
                    }
                    else
                    {
                        throw new ArgumentException(exceptionMessage);
                    }
                    break;
                case TypeCode.Double:
                    Double doubleValue;
                    if (Double.TryParse(inputValue, out doubleValue))
                    {
                        result = doubleValue;
                    }
                    else
                    {
                        throw new ArgumentException(exceptionMessage);
                    }
                    break;
                case TypeCode.Single:
                    Single singleValue;
                    if (Single.TryParse(inputValue, out singleValue))
                    {
                        result = singleValue;
                    }
                    else
                    {
                        throw new ArgumentException(exceptionMessage);
                    }
                    break;
                case TypeCode.UInt16:
                    UInt16 uInt16Value;
                    if (UInt16.TryParse(inputValue, out uInt16Value))
                    {
                        result = uInt16Value;
                    }
                    else
                    {
                        throw new ArgumentException(exceptionMessage);
                    }
                    break;
                case TypeCode.UInt32:
                    UInt32 uInt32Value;
                    if (UInt32.TryParse(inputValue, out uInt32Value))
                    {
                        result = uInt32Value;
                    }
                    else
                    {
                        throw new ArgumentException(exceptionMessage);
                    }
                    break;
                case TypeCode.UInt64:
                    UInt64 uInt64Value;
                    if (UInt64.TryParse(inputValue, out uInt64Value))
                    {
                        result = uInt64Value;
                    }
                    else
                    {
                        throw new ArgumentException(exceptionMessage);
                    }
                    break;
                default:
                    throw new ArgumentException(exceptionMessage);
            }
            return result;
        }
    }
}
