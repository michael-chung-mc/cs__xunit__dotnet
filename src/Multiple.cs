using System;

namespace wip__cs__xunit.src.Multiple
{
    internal class Multiple
    {
        // sum of all multiples x or y below z
        public int sumMultiple(int x, int y, int z)  {
            if (x < 0 || y < 0 || z < 0) { return -1; }
            int sum = 0;
            for (int i = 0; i < z; i++) {
                if (i%x == 0 || i%y == 0)  { sum += i; }    
            }
            return sum;
        }
    }
}
