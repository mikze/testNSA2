using System;
using System.Collections.Generic;
using System.Text;
//Komentarz: Klasa Zamowienia dziedziczy po klasie ZamowienieTable zdefiniowanej w module BPX_NSAModule. Klasa ta reprezentuje tabelę zamówień w systemie NSA i może zawierać dodatkowe właściwości lub metody specyficzne dla zamówień w kontekście modułu BPX_NSA.
namespace BPX_NSA
{
    public class Zamowienia : BPX_NSAModule.ZamowienieTable
    {
    }
}
