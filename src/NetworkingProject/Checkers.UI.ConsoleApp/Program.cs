using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkers.Domain.Interfaces;

namespace Checkers.UI.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            CheckersClientGame cg = new CheckersClientGame(DependencyInjectionContainer.Get<ICheckersRepository>());
            cg.StartAsync().Wait();
        }
    }
}
