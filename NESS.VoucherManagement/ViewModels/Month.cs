using System;
using System.Linq;

namespace NESS.VoucherManagement.ViewModels
{
    public struct Month
    {
        public override string ToString() => Name;

        public Month(int index, string name)
        {
            Index = index;
            Name = name;
        }

        public int Index { get; }

        public string Name { get; }
    }
}