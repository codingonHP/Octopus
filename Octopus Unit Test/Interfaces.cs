using System;

namespace Octopus_Unit_Test
{
    public interface IHello
    {
        string SayHello(string message);
    }

    public interface ILogin
    {
        bool ValidateLogin(string userName);
    }

  
}