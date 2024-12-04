// See https://aka.ms/new-console-template for more information

using DataStructures.BitVector;

var bv = new BitVector(123);
bv.UnsetBit(100);
Console.WriteLine(bv[100]); // false



