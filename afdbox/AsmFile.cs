using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace afdbox
{
    internal record AsmFile(string Path, string Name, string Ext)
    {
        public override string ToString()
        {
            return $"Name: {Name}\nExt: {Ext}\nPath: {Path}";
        }

        public static AsmFile? FromArgs(string[] args)
        {
            foreach (var arg in args)
            {
                if (arg.EndsWith(".asm"))
                {
                    return new AsmFile(arg, System.IO.Path.GetFileName(arg), System.IO.Path.GetExtension(arg));
                }
            }

            return null;
        }
    }
}
