using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleStuff;

public interface IFileInterface
{
    public abstract void CreateFile(string fileName);
    public abstract void WriteToFile(string fileName);
    public abstract void DoesFileNameHaveTXT(string fileName);
}