using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;




// WARNING WARNING : A VERY DYNAMIC ARRAY AHEAD

// examples:

// Bale<int> yourname = new();

// yourname[-1] -- returns end value of the array , python like.
// yourname[-2] -- returns end - 1 value of the array , python like.
// yourname[-3] -- returns end - 2 value of the array , python like.
// and so on.
// same for assignment.

// this array will make the necessary cells before your index , examples:
// yourname[3] -- returns yourname[3] and makes: yourname[0] , yourname[1] , yourname[2] , yourname[3] indexes.
// same for assignment.


// P.S. this code was made by me i just checked a source code of List and started making this. (NO STUPID AI coding)

// Made by dirochik ( DuckTheDir ) on GitHub.

// this Array may have some bugs/mispells.

namespace DataList
{
    /// <summary> 
    /// Bale.
    /// A VERY DYNAMIC ARRAY!!!</summary>

    

    internal class Bale<Type> : IEnumerable<Type> 
    {


        protected Type[] _items = new Type[0];


        public  static  explicit operator object[](Bale<Type> bale)
        {
            
            return bale.GetItemsArray().Cast<object>().ToArray();
        }


        public Bale()
        {
            _items = new Type[0];
        }
        public Bale(Type[] items)
        {
          
            _items = items;
          
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Gets Type of your Bale , very cool , wow.
        /// </summary>
        public System.Type TypeOfArray => typeof(Type);



        /// <summary>
        /// Length of your Bale.
        /// </summary>
        public int Width { 
            get { return _items.Length; }
            set { ChangeCapacity(value); }
        }

        public IEnumerator<Type> GetEnumerator()
        {
            foreach (Type i in _items)
            {
                yield return i;
            }
        }

        // me working for int
        public  Type this[ int i]
        {
            
            get {
                if (i < 0)
                    if (Math.Abs(i) < _items.Length)
                        return _items[_items.Length +i];
                    else return _items[0];
                else { if (i >= _items.Length) Array.Resize(ref _items, i + 1); return _items[i]; } }
            set { Assign(i, value); }
            
        }
        // me working for range
        public  IEnumerable<Type> this[System.Range Range]
        {
            get { return GetThings(int.Parse(Range.Start.ToString()), int.Parse(Range.End.ToString())); }

        }

        


        /// <summary>
        /// Gets index of the first similar value by value.
        /// </summary>
        public virtual int GetIndexOf(Type item)
        {

            for (int i = 0; i < _items.Length; ++i)
            {
                Type cur_THE_item = _items[i];  
                
                if (item.Equals(cur_THE_item)) {  return i;}
                
            }
            return -1;
        }

        /// <summary>
        /// Gets index of all similar values by value and returns IEnumerable<int>.
        /// </summary>
        public virtual IEnumerable<int> GetAllIndexesOf(Type item)
        {
           

            for (int i = 0; i < _items.Length; ++i)
            {
                Type cur_THE_item = _items[i];

                if (item.Equals(cur_THE_item)) {  yield return i; }

            }
           

        }

        /// <summary>
        /// Assigns your "item" by index in other words: bruh[i] = item
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        public void Assign(int index ,Type item)
        {
            int absInt = Math.Abs(index);

            if (absInt >= _items.Length)
                Array.Resize(ref _items, Math.Clamp(absInt + 1, 1, int.MaxValue));
            

            if (index < 0)
                _items[_items.Length + index] = item;
            else _items[index] = item;
            return;
        }

        /// <summary>
        /// Changes capacity of your Bale by size.
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>

        public int ChangeCapacity(int size)
        {
            Array.Resize(ref _items, Math.Clamp(size,1,int.MaxValue));
            return size;
        }

        /// <summary>
        /// Python like append , adds your "item" to the back of Bale.
        /// </summary>
        /// <param name="item"></param>
        public void Add(Type item)
        {
            Assign(_items.Length, item);
            return;
        }

        /// <summary>
        /// Python like insert , adds your "item" to the index and shifts other values.
        /// </summary>
        /// <param name="item"></param>
        public void InterPose(int index, Type item)
        {
            
            if (index < 0)
            {
                index = _items.Length+index+1;
                if (index < 0)index = 0;
            }
           

            Array.Resize(ref _items, _items.Length + 1);
            for (int i = _items.Length - 1; i > index; i--)
            {
                _items[i] = _items[i - 1];
            }
            _items[index] = item;
        }

        /// <summary>
        ///  Just  sets your Bale's size to 0 , in other words : new();
        /// </summary>
        public void Blank() // scary, idk why i made it you could just : nameOfYourBale= new Bale<type>();
        {
            
            _items = new Type[0];
            return;
        }

        /// <summary>
        /// Returns Things of your Bale, in range of ,  it's similiar to the Array[start..end].
        /// </summary>
        /// <param name="amount"></param>
        /// <returns> </returns>
        public virtual IEnumerable<Type> GetThings(int start = -1,int end = -1)
        {
          

            if ((start < 0 || end < 0) ||  (start >  _items.Length || end > _items.Length))
            {
                foreach (Type i in this)
                {yield return i;}
            }
            else
            {
                for (int i = start; i < end; ++i)
                {
                    yield return _items[i];
                }
            }
            
            
        }


        /// <summary>
        /// Length of your Bale.
        /// </summary>
        /// <returns></returns>

        public int Amount(int size = -1)
        {
           if (size <= 0)return _items.Length;
           else { return ChangeCapacity(size); }
        }


        /// <summary>
        /// Returns a combined Bale.
        /// </summary>
        /// <param name="arrayToCombineWith"></param>
        /// <returns></returns>
        public virtual Bale<object> CombineWith<OtherType>(Bale<OtherType> arrayToCombineWith)
        {

                Bale<object> b = new();
                foreach (object i in arrayToCombineWith)
                {
                    b.Add(i);
                }
                return b;

            

        }

        /// <summary>
        /// Returns Bale's array.
        /// </summary>
        /// <returns></returns>
        public Type[] GetItemsArray()
        {
            return _items;
        }


        /// <summary>
        /// Returns summary of int/float/long/double/decimal  in your array if your TypeOfArray equals to  "int/float/long/double/decimal".
        /// D_Bale doesn't need to vailidate their TypeOfArray , it will check for numbers and sum them.
        /// </summary>
        /// <returns></returns>

        public virtual decimal SummaryOfNumbers()
        {

            if (TypeOfArray == typeof(decimal) || TypeOfArray == typeof(int) || TypeOfArray== typeof(float) || TypeOfArray == typeof(double) || TypeOfArray == typeof(long))
            {
                decimal summa = 0;

                foreach (Type i in _items)
                {
                    if (i is int ni) summa += ni;
                    if (i is long nl) summa += nl;
                    else if (i is float nf) summa += (decimal)nf;
                    else if (i is double nd) summa += (decimal)nd;
                    else if (i is decimal ndec) summa += ndec;
                    else continue;
                    
                }
                return summa;

            }
            else return decimal.Zero;
        }

        /// <summary>
        /// Packs all your values of Bale into 1 string and returns it. You can also edit the split between them.
        /// </summary>
        /// <param name="split_thing"></param>
        /// <returns></returns>
        public  string PackIntoString(string split_thing = "")
        {
            string PackedString = "";
            foreach (Type i in _items)
            {
                if (i is null) continue;
                PackedString += i.ToString() +split_thing;
               
            }
            return PackedString;
        }

        /// <summary>
        /// Removes specific value and all it's copies and shifts values.
        /// </summary>
        /// <param name="theValue"></param>
        /// 

        //--------- Not done , you can fix it. ------------
        //public void RemoveSpecific(object theValue)
        //{
        //    int newI = 0;
        //    for (int i = 0; i < Width; ++i)
        //    {
        //        if (!theValue.Equals(_items[i]))
        //        {
        //            Assign(newI, _items[i]);
        //            ++newI;
        //        }

        //    }
        //    Width = newI;
        //}

        //--------- Not done , you can fix it. ------------


        /// <summary>
        /// Randomly shuffles your bale and returns it. If  trulyShuffle set to true it's shuffles your bale and assigns shuffled bale to your bale.
        /// </summary>
        /// <param name="seed"></param>
        /// <returns></returns>
        public Bale<Type> MakeMess(int seed = 0,bool trulyShuffle = false)
        {

            Random r = new Random();
            if (seed != 0) r = new Random(seed);

            int n = Width;

            
            Type[] itms = _items;

            for (int i = n - 1; i > 0; i--)
            {
                int j = r.Next(i + 1);
                Type temp = itms[i];
                itms[i] = itms[j];
                itms[j] = temp;
            }

            if (!trulyShuffle)
            {
               Bale<Type> b = new Bale<Type>();
               b._items = itms;
               return b;
            }
            else
            {
                _items = itms;
                return this;
            }
            

        }


    }


    // ---------------------------------------------------------------------------------------

    /// <summary>
    /// D_Bale (Dynamic Bale).
    /// WOW A DYNAMIC VERSION OF VERY DYNAMIC ARRAY!!!
    /// </summary>
    internal class D_Bale  : Bale<object>
    {
        public D_Bale()
        {
           
        }
        public D_Bale(object[] argh) 
        {
            _items = argh;
        }



        public  D_Bale CombineWith<OtherType>(Bale<OtherType> arrayToCombineWith , bool trulyCombine = false)
        {
            if (!trulyCombine)return (D_Bale)base.CombineWith(arrayToCombineWith);
            else
            {
                foreach (object i in arrayToCombineWith)
                {
                    Add(i);
                }
                return this;
            }
        }

        public  IEnumerable<object> GetThings<OtherType>(int start = -1, int end = -1)
        {
            if ((start < 0 || end < 0) || (start > _items.Length || end > _items.Length))
            {
                foreach (var i in this)
                { if (i is OtherType foundI)
                    yield return foundI; }
            }
            else
            {
                for (int i = start; i < end; ++i)
                {
                    if (_items[i] is OtherType foundI)
                    yield return foundI ;
                }
            }
        }

        public override decimal SummaryOfNumbers()
        {
            decimal summa = 0;

            foreach (var i in _items)
            {
                if (i is int ni) summa += ni;
                if (i is long nl) summa += nl;
                else if (i is float nf) summa += (decimal)nf;
                else if (i is double nd) summa += (decimal)nd;
                else if (i is decimal ndec) summa += ndec;
                else continue;
            }
            return summa;
        }

        /// <summary>
        /// Like Summary of Numbers , but for specified number type.
        /// </summary>
        /// <returns></returns>
        public INumber<TofNum> SumOfNum<TofNum>() where TofNum : INumber<TofNum>
        {
            TofNum summa_wow_numbers = TofNum.Zero;
            foreach (var i in _items)
            {
                if (i is TofNum ni) summa_wow_numbers += ni;
             
                else continue;
            }
            return summa_wow_numbers;
        }
        

        
        
        
    }
}



// Some classes/structs i made to help me


// works only for string / object Bales
// - now nothing
