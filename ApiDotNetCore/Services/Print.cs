using System;

namespace Service
{
    public class Print : IPrint
    {
        public string[] printConsole()
        {
            return new string[] { "value1", "value2" };
        }
    }
}
