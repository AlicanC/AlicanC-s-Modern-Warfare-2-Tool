/*
  BitVectorReader c# class: Read bitvector data using primary types
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
    public class BitVectorReader
    {
	private BitVector m_data;
	private int m_offset;

	public BitVectorReader(BitVector v)
	{
	    m_data = v;
	    m_offset = 0;
	}

	public bool HasMoreData()
	{
	    if (m_offset < m_data.Length)
		return true;
	    return false;
	}

	public Int32 ReadInt32()
	{
	    Int32 result = 0;
	    for (int max = m_offset+32, offset = 0; (m_offset < max) && (m_offset < m_data.Length); m_offset++, offset++)
		{
		    if (m_data.Get(m_offset))
			result |= (1<<(31 - offset));
		}
	    return result;
	}


	public Int16 ReadInt16()
	{
	    int result = 0;
	    for (int max = m_offset+16, offset = 0; (m_offset < max) && (m_offset < m_data.Length); m_offset++, offset++)
		{
		    if (m_data.Get(m_offset))
			result |= (1<<(15 - offset));
		}
	    return (Int16)result;
	}


	public byte ReadByte()
	{
	    byte result = 0;
	    for (int max = m_offset+8, offset = 0; (m_offset < max) && (m_offset < m_data.Length); m_offset++, offset++)
		{
		    if (m_data.Get(m_offset))
			result |= (byte)(1<<(7 - offset));
		}
	    return result;
	}

	public String ReadAscii(int length)
	{
	    StringBuilder buffer = new StringBuilder();
	    for (int i=0; i<length; i++)
		{
		    buffer.Append((char)ReadByte());
		}
	    return buffer.ToString();
	}

    }

}

