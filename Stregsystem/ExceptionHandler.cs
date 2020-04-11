using System;
using System.Collections.Generic;
using System.Text;

namespace Stregsystem
{
    public static class Feedback
    {
        public static void NullReference(string e) { }
        public static void NullReference(string e, int id, dynamic var) { }
        public static void InvalidArg(string e) { }
        public static void InsufficientCreditsException(string e) { }

        public static void StregsystemEvent(string e) { }

    }
}
