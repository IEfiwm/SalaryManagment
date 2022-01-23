using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Extensions.DataConversion
{
    public delegate bool TryParseMethod<T>(string input, out T value);

    public interface ITryParser
    {
        bool TryParse(string input, out object value);
    }

    public class TryParser<T> : ITryParser
    {
        private TryParseMethod<T> ParsingMethod;

        public TryParser(TryParseMethod<T> parsingMethod)
        {
            this.ParsingMethod = parsingMethod;
        }

        public bool TryParse(string input, out object value)
        {
            T parsedOutput;
            bool success = ParsingMethod(input, out parsedOutput);
            value = parsedOutput;
            return success;
        }
    }

}
