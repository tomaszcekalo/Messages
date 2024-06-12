using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.Services
{
    public interface ICounterService
    {
        public int Increment();
    }

    public class CounterService : ICounterService
    {
        private int _value = 0;

        public int Increment()
        {
            return ++_value;
        }
    }
}