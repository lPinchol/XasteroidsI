///////////////////////////////////////////////////////////////////////////////////////////////////////////
// List
///////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;
///////////////////////////////////////////////////////////////////////////////////////////////////////////
namespace MoonPincho
{
	public class LIST<T>
	{
		//*********************************************************************************************
		bool[] EmptyArray;
		//---------------------------------------------------------------------------------------------
		T[] Array;
		int ArraySize;
		int ArrayCount;
		//---------------------------------------------------------------------------------------------
		int[] FreeArray;
		int FreeArraySize;
		int FreeArrayCount;
		//---------------------------------------------------------------------------------------------
		int[] IndexArray;
		bool IndexArrayFlag;

		//*********************************************************************************************

		//---------------------------------------------------------------------------------------------
		// CONSTRUCTOR
		//---------------------------------------------------------------------------------------------
		public LIST()
		{
			EmptyArray = new bool[0];
			Array = new T[0];
			ArraySize = 0;
			ArrayCount = 0;
			FreeArray = new int[0];
			FreeArraySize = 0;
			FreeArrayCount = 0;
			IndexArray = new int[0];
			IndexArrayFlag = false;
		}
		//---------------------------------------------------------------------------------------------
		// FUNCTIONS
		//---------------------------------------------------------------------------------------------
		public int Add(T obj)
		{
			int index = GetIndex();
			//Not Empty
			EmptyArray[index] = false;
			//Move Object To Array
			Array[index] = obj;
			//Index Array Clear
			IndexArrayFlag = false;

			return index;
		}
		//---------------------------------------------------------------------------------------------
		public void Remove(int index)
		{
			if (EmptyArray[index] == false)
			{
				//Remove From Array
				EmptyArray[index] = true;
				//Index Array Clear
				IndexArrayFlag = false;

				if (FreeArrayCount == FreeArraySize)
				{
					//Increment Free Array
					FreeArraySize = (FreeArraySize << 1) + 1;
					int[] tempFreeArray = new int[FreeArraySize];
					for (int i = 0; i < FreeArrayCount; i++) tempFreeArray[i] = FreeArray[i];
					FreeArray = tempFreeArray;
				}
				//Add Index To FreeArray
				FreeArray[FreeArrayCount++] = index;
			}
		}
		//---------------------------------------------------------------------------------------------
		public void RemoveAll()
		{
			EmptyArray = new bool[0];
			Array = new T[0];
			ArraySize = 0;
			ArrayCount = 0;
			FreeArray = new int[0];
			FreeArraySize = 0;
			FreeArrayCount = 0;
			IndexArray = new int[0]; ;
			IndexArrayFlag = false;
		}
		//---------------------------------------------------------------------------------------------
		int GetIndex()
		{
			if (ArrayCount < ArraySize)
			{
				//Get Index From Array
				return ArrayCount++;
			}
			else
			{
				if (FreeArrayCount > 0)
				{
					//Get Index From Free Array
					return FreeArray[--FreeArrayCount];
				}
				else
				{
					//Increment Array
					ArraySize = (ArraySize << 1) + 1;
					bool[] emptyArrayTemp = new bool[ArraySize];
					T[] arrayTemp = new T[ArraySize];
					for (int i = 0; i < ArrayCount; i++)
					{
						arrayTemp[i] = Array[i];
						emptyArrayTemp[i] = EmptyArray[i];
					}
					EmptyArray = emptyArrayTemp;
					Array = arrayTemp;
					return ArrayCount++;
				}
			}
		}
		//---------------------------------------------------------------------------------------------
		public int GetCount()
		{
			//Get Count
			int count = ArrayCount - FreeArrayCount;
			//Create Index Array
			if (IndexArrayFlag == false)
			{
				if (count > 0)
				{
					IndexArray = new int[count];

					int c = 0;
					for (int i = 0; i < ArrayCount; i++)
					{
						if (EmptyArray[i] == false) IndexArray[c++] = i;
					}
					IndexArrayFlag = true;
				}
			}

			return count;
		}
		//---------------------------------------------------------------------------------------------
		public T this[int i]
		{
			get { return Array[IndexArray[i]]; }
			set { Array[IndexArray[i]] = value; }
		}
		//---------------------------------------------------------------------------------------------
		public void RemoveIndex(int index)
		{  
			Remove(IndexArray[index]);
		}
		//---------------------------------------------------------------------------------------------
		public void Foreach(Action<T> action)
		{
			for (int i = 0; i < GetCount(); i++)
			{
				//Function
				action(Array[IndexArray[i]]);
			}
		}
		//---------------------------------------------------------------------------------------------
		public int Search(Func<T,bool> action)
		{
			for(int i = 0; i < ArrayCount; i++)
			{
				if(EmptyArray[i] == false)
				{
					//Function
					if (action(Array[i])) return i;
				}
			}

			return -1;
		}
		//---------------------------------------------------------------------------------------------
	}
}
///////////////////////////////////////////////////////////////////////////////////////////////////////////
