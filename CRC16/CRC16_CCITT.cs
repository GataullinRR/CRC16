﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace CRC16
{
    public class CRC16_CCITT
    {
        const ushort _poly = 4129;
        const ushort _initialValue = 0xFFFF;
        readonly ushort[] _table = new ushort[256];

        public CRC16_CCITT()
        {
            ushort temp, a;
            for (int i = 0; i < _table.Length; ++i)
            {
                temp = 0;
                a = (ushort)(i << 8);
                for (int j = 0; j < 8; ++j)
                {
                    if (((temp ^ a) & 0x8000) != 0)
                    {
                        temp = (ushort)((temp << 1) ^ _poly);
                    }
                    else
                    {
                        temp <<= 1;
                    }
                    a <<= 1;
                }
                _table[i] = temp;
            }
        }

        public ushort ComputeChecksum(IEnumerable<byte> bytes)
        {
            ushort crc = _initialValue;
            var bytesArray = bytes.ToArray();
            for (int i = 0; i < bytesArray.Length; ++i)
            {
                crc = (ushort)((crc << 8) ^ _table[((crc >> 8) ^ (0xff & bytesArray[i]))]);
            }
            return crc;
        }
    }
}
