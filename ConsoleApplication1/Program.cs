using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raikage.Test.Core.ViewModels;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var _firstViewModel = new FirstViewModel();
            var command1 = _firstViewModel.Command1;
            command1.Execute("");
        }
    }
}
