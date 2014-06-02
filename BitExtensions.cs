﻿using System;

namespace MetroidPassword {
	internal static partial class Extensions {
		private const int ByteSize = sizeof(byte) * 8;
		private const int ShortSize = sizeof(ushort) * 8;
		private const int IntSize = sizeof(uint) * 8;
		private const int LongSize = sizeof(ulong) * 8;

		public static byte RotateRight(this byte pVal, int pShiftCount) {
			pShiftCount &= ByteSize - 1;
			return (byte)((byte)(pVal >> pShiftCount) | (byte)(pVal << (ByteSize - pShiftCount)));
		}

		public static byte RotateLeft(this byte pVal, int pShiftCount) {
			pShiftCount &= ByteSize - 1;
			return (byte)((byte)(pVal << pShiftCount) | (byte)(pVal >> (ByteSize - pShiftCount)));
		}

		public static ushort RotateRight(this ushort pVal, int pShiftCount) {
			pShiftCount &= ShortSize - 1;
			return (ushort)((ushort)(pVal >> pShiftCount) | (ushort)(pVal << (ShortSize - pShiftCount)));
		}

		public static ushort RotateLeft(this ushort pVal, int pShiftCount) {
			pShiftCount &= ShortSize - 1;
			return (ushort)((ushort)(pVal << pShiftCount) | (ushort)(pVal >> (ShortSize - pShiftCount)));
		}

		public static uint RotateRight(this uint pVal, int pShiftCount) {
			pShiftCount &= IntSize - 1;
			return (uint)((uint)(pVal >> pShiftCount) | (uint)(pVal << (IntSize - pShiftCount)));
		}

		public static uint RotateLeft(this uint pVal, int pShiftCount) {
			pShiftCount &= IntSize - 1;
			return (uint)((uint)(pVal << pShiftCount) | (uint)(pVal >> (IntSize - pShiftCount)));
		}

		public static ulong RotateRight(this ulong pVal, int pShiftCount) {
			pShiftCount &= LongSize - 1;
			return (ulong)((ulong)(pVal >> pShiftCount) | (ulong)(pVal << (LongSize - pShiftCount)));
		}

		public static ulong RotateLeft(this ulong pVal, int pShiftCount) {
			pShiftCount &= LongSize - 1;
			return (ulong)((ulong)(pVal << pShiftCount) | (ulong)(pVal >> (LongSize - pShiftCount)));
		}

		public static byte[] RotateLeft(this byte[] pBytes, int pShiftCount) {
			if (pBytes == null) return null;
			if (pBytes.Length == 0 || pShiftCount == 0) return pBytes;
			pShiftCount %= pBytes.Length * ByteSize;

			byte[] returnVal = new byte[pBytes.Length];
			Buffer.BlockCopy(pBytes, 0, returnVal, 0, pBytes.Length);
			if (pBytes.Length == 1) {
				returnVal[0] = returnVal[0].RotateLeft(pShiftCount);
			}
			else {
				bool bit = true;
				bool tempBit = false;
				int end = pBytes.Length - 1;
				int first = end;
				byte bitValue = 0x01;
				byte bitMatch = 0x80;

				for (int shift = 0; shift < pShiftCount; shift++) {
					byte b = returnVal[first];

					for (int idx = end; idx >= 0; idx--) {
						tempBit = (returnVal[idx] & bitMatch) == bitMatch;
						returnVal[idx] <<= 1;
						returnVal[idx] |= (byte)(bit ? bitValue : 0);
						bit = tempBit;
					}

					tempBit = (b & bitMatch) == bitMatch;
					b <<= 1;
					b |= (byte)(bit ? bitValue : 0);
					returnVal[first] = b;
					bit = tempBit;
				}
			}
			return returnVal;
		}

		public static byte[] RotateRight(this byte[] pBytes, int pShiftCount) {
			if (pBytes == null) return null;
			if (pBytes.Length == 0 || pShiftCount == 0) return pBytes;
			pShiftCount %= pBytes.Length * ByteSize;

			byte[] returnVal = new byte[pBytes.Length];
			Buffer.BlockCopy(pBytes, 0, returnVal, 0, pBytes.Length);
			if (pBytes.Length == 1) {
				returnVal[0] = returnVal[0].RotateRight(pShiftCount);
			}
			else {
				bool bit = true;
				bool tempBit = false;
				int end = pBytes.Length - 1;
				int first = 0;
				byte bitValue = 0x80;
				byte bitMatch = 0x01;

				for (int shift = 0; shift < pShiftCount; shift++) {
					byte b = returnVal[first];

					for (int idx = 0; idx <= end; idx++) {
						tempBit = (returnVal[idx] & bitMatch) == bitMatch;
						returnVal[idx] >>= 1;
						returnVal[idx] |= (byte)(bit ? bitValue : 0);
						bit = tempBit;
					}

					tempBit = (b & bitMatch) == bitMatch;
					b >>= 1;
					b |= (byte)(bit ? bitValue : 0);
					returnVal[first] = b;
					bit = tempBit;
				}
			}
			return returnVal;
		}
	}
}