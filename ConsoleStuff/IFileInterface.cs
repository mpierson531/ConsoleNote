using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleStuff;

public interface IFileInterface
{
    void CreateFile(string fileName);
    void WriteToFile(string fileName);
    void DoesFileNameHaveTXT(string fileName);
}
