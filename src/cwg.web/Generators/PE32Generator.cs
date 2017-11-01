﻿using System;
using System.IO;

namespace cwg.web.Generators
{
    public class PE32Generator : BaseGenerator
    {
        public override (string sha1, string fileName) Generate()
        {
            var originalBytes = System.IO.File.ReadAllBytes("sourcePE");

            var newBytes = new byte[getRandomInt()];

            FillArray(newBytes);

            for (var y = 0; y < newBytes.Length; y++)
            {
                originalBytes[originalBytes.Length - 1 - y] = newBytes[y];
            }

            var sha1Sum = ComputeSha1(originalBytes);
            
            System.IO.File.WriteAllBytes(Path.Combine(AppContext.BaseDirectory, $"{sha1Sum}.exe"), originalBytes);

            return (sha1Sum, $"{sha1Sum}.exe");
        }
    }
}