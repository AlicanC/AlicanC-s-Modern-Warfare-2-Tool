/*
  GameWatch: a c# online game browser
  Copyright (C) 2002 Rodrigo Reyes, reyes@charabia.net
  
  This library is free software; you can redistribute it and/or
  modify it under the terms of the GNU Lesser General Public
  License as published by the Free Software Foundation; either
  version 2.1 of the License, or (at your option) any later version.
  
  This library is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
  Lesser General Public License for more details.
  
  You should have received a copy of the GNU Lesser General Public
  License along with this library; if not, write to the Free Software
  Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
*/

using System;
using System.Text;
using System.Collections;

namespace GameWatch.Utils
{
    public class BitVector
    {
	private byte[] m_data = new byte[0];
	private int m_maxoffset = -1;

	public BitVector()
	{
	}

	public BitVector(byte[]data)
	{
	    m_data = new byte[data.Length];
	    for (int i=0; i<data.Length; i++)
		{
		    m_data[i] = data[i];
		}
	    m_maxoffset = (m_data.Length * 8) - 1;
	}

	public BitVector(long val, int length)
	{
	    AddData(val, length);
	}

	public void AddAscii(string val)
	{
	    for (int i=0; i<val.Length; i++)
		{
		    AddData(val[i], 8);
		}
	}

	public void AddData(long val, int length)
	{
	    int offset = m_maxoffset + 1;
	    for (int i=0; i<length; i++)
		{
		    // Console.WriteLine("bit {0} of {1} = {2} at {3}", (length-i-1), val, (val & (long)(1<<(length-i-1)))!=0, i);
		    Set(offset + i, (val & (long)(1<<(length-i-1))) != 0);		    
		    //		    Console.WriteLine("Added {0} to {1}", (val & (long)(1<<(length-i-1))) != 0, this.ToString());
	    
		}
	    //	    Console.WriteLine("Created {0}", this.ToString());	    
	}

	public byte[] GetByteArray()
	{
	    byte[] result = new byte[m_data.Length];
	    for(int i=0; i<m_data.Length; i++)
		result[i] = (byte)m_data[i];
	    return result;
	}

	public int Length
	{
	    get { return m_maxoffset + 1; }
	}

	public void Set(int offset, bool val)
	{
	    int byteoffset = offset/8;
	    int bitoffset = offset%8;

	    if (byteoffset >= m_data.Length)
		{
		    byte[] data = new byte[byteoffset+1];
		    for (int i=0; i<m_data.Length; i++)
			{
			    data[i] = m_data[i];
			}
		    m_data = data;
		    m_maxoffset = offset;
		}
	    else if (offset > m_maxoffset)
		{
		    m_maxoffset = offset;
		}

	    if (val)
		m_data[byteoffset] |= (byte)(1 << (7-bitoffset));
	    else
		m_data[byteoffset] &= (byte)(0xff - (1 << (7-bitoffset)));
	    //	    Console.WriteLine("data: {0}", m_data[byteoffset]);
	}

	public bool Get(int offset)
	{
	    if (offset > m_maxoffset)
		{
		    throw new Exception("OutOfBound offset " + offset);
		}
	    int byteoffset = offset/8;
	    int bitoffset = offset%8;

	    if (byteoffset >= m_data.Length)
		{
		    throw new Exception("OutOfBound offset " + offset);
		}

	    //	    Console.WriteLine("get {0}.{1} (l:{2}", byteoffset, bitoffset, m_data.Length);
	    return (m_data[byteoffset] & ( 1 << (7 - bitoffset))) != 0;
	}

	public BitVector Range(int start, int length)
	{
	    BitVector result = new BitVector();
	    for (int i=start; i<(start+length); i++)
		{
		    result.Set(i-start, this.Get(i));
		}
	    return result;
	}

	public int LongestCommonPrefix(BitVector other)
	{
	    int i=0;
	    while ((i<=other.m_maxoffset) && (i<=this.m_maxoffset))
		{
		    if (other.Get(i) != this.Get(i))
			{
			    return i;
			}
		    i++;
		}
	    return i;
	}

	override public String ToString()
	{
	    StringBuilder sb = new StringBuilder();
	    for (int i=0; i<=m_maxoffset; i++)
		{
		    sb.Append( (Get(i)==true)?"1":"0" );
		    if (i%8 == 7)
			sb.Append(" ");
		}
	    return sb.ToString();
	}

	static public void Test()
	{
	    BitVector v1 = new BitVector();
	    Console.WriteLine("v1: {0}", v1.ToString());
	    v1.Set(3, true);
	    Console.WriteLine("v1: {0}", v1.ToString());
	    v1.Set(5, true);
	    Console.WriteLine("v1: {0}", v1.ToString());
	    v1.Set(1, true);

	    BitVector v2 = new BitVector();
	    v2.AddData(0x65,8);
	    v2.AddData(-1,32);
	    Console.WriteLine("v2= {0}", v2);
	    byte[]v2b = v2.GetByteArray();
	    for (int i=0; i<v2b.Length; i++)
		{
		    Console.WriteLine(" v2b[{0}]={1}", i, v2b[i]);
		}

	    BitVectorReader r = new BitVectorReader(v2);
	    byte b1 = r.ReadByte();
	    int i1 = r.ReadInt32();

	    Console.WriteLine("Read : {0}, {1}", b1, i1);
	    
	    Console.WriteLine("v1: {0} (should be 010101)", v1.ToString());
	}
	

    }

}

