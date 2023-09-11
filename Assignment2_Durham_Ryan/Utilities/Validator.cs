using Assignment2_Durham_Ryan.Interfaces;

namespace Assignment2_Durham_Ryan.Utilities
{
    public class Validator : IValidator
    {
        bool IsTruthy = false;

        public Validator() 
        { 

        }

        public Validator(bool isTruthy) 
        { 
            IsTruthy = isTruthy;
        }

        public bool IsValid(string value)
        {
            return true;
        }
    }
}
