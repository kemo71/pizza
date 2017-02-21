using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pizza
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        public bool isValidSlice(char[,] slice, int pizzaRows, int pizzaColumns, int minimumIngredientsPerSlice, int maximumCellsPerSlice)
        {
            //if the slice cells exceed maximum cells per slice exit with false
            if ((slice.GetLength(0) * slice.GetLength(1)) > maximumCellsPerSlice)
                return false;

            //if slice diementions does not fit in the pizza exit with false
            if (slice.GetLength(0) > pizzaRows || slice.GetLength(1) > pizzaColumns)
                return false;

            //count number of tomatoes and number of mushrooms and if they both higher than the minimum number of ingredients per slice return true
            int tomatoesCounter = 0;
            int mushroomsCounter = 0;

            for (int i = 0; i < slice.GetLength(0); i++)
            {
                for (int j = 0; j < slice.GetLength(1); j++)
                {
                    if (slice[i, j] == 'T')
                        tomatoesCounter++;
                    else if (slice[i, j] == 'M')
                        mushroomsCounter++;
                    else
                        Console.WriteLine("unexpected char : "+slice[i,j]);

                    if (tomatoesCounter >= minimumIngredientsPerSlice && mushroomsCounter >= minimumIngredientsPerSlice)
                        return true;
                }
            }


            if (tomatoesCounter >= minimumIngredientsPerSlice && mushroomsCounter >= minimumIngredientsPerSlice)
                return true;
            else
                return false;
        }
        
        public static List<char[,]> makeIndexes(int Rows, int Columns, int MinOfEachIngrediands, int MaxCellSize)
        {
            List<char[,]> createdSlices = new List<char[,]>();
            int InitialCellLength = MinOfEachIngrediands * 2;
            int RowIndex = 1;
            int ColumnIndex = InitialCellLength;
            double LoopsApprxNumDouble = (Rows * Columns) / InitialCellLength;
            int LoopsApprxNumInt = (int)Math.Ceiling(LoopsApprxNumDouble);
            for (int i = RowIndex; i <= Rows; i++)
            {
                for (int j = ColumnIndex; j <= Columns; j++)
                {
                    if (i * j <= MaxCellSize)
                        createdSlices.Add(new char[i, j]);
                }
                ColumnIndex = 1;
            }
            return createdSlices;
        }
    }
}
