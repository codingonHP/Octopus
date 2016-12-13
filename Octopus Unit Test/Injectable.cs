using System;
using Octopus.Attributes;

namespace Octopus_Unit_Test
{
    [Injectable]
    public class Hello : IHello
    {
        public string SayHello(string message)
        {
            return "Say hello : " + message;
        }
    }

    [Injectable]
    public class LoginValidator : ILogin
    {
        public bool ValidateLogin(string userName)
        {
            return true;
        }
    }

}