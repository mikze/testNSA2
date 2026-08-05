using System;
using System.Collections.Generic;
using System.Text;

namespace BPX_NSA
{
    //Ważna zmiana users/mz/WaznaZmiana
    public class Zwrot : BPX_NSAModule.ZwrotRow
    {
        override public string ToString() 
            => $"Zwrot: {ID}, Data: {Stamp}, Uzasadnienie: {Uzasadnienie}.";
    }
}
