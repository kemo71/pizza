using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pizza
{
    class Program
    {
        public static int highestRow=0;
        public static int highestColumn=0;

        static void Main(string[] args)
        {
			char[,] pizza = new char[3,5]{ { 'T', 'T', 'T', 'T', 'T' }, { 'T', 'M', 'M', 'M', 'T' }, { 'T', 'T', 'T', 'T', 'T' } };
			List<char[,]> possibleSlicesToCut = makeIndexes (3, 5, 1, 6);
			List<ValidSlice> ValidSlices = createValidSlices (pizza, possibleSlicesToCut, 1);
            List<ValidSlice> FinalVS = new List<ValidSlice>();

            int counter = 0;

            var temp = ValidSlices.ToList();

            temp = temp.OrderByDescending(a => (a.endingRow - a.startingRow + 1)).ThenByDescending(a => (a.endingColumn - a.startingColumn + 1)).ToList();
           
            FinalVS.AddRange(temp);
            foreach (ValidSlice tmp in FinalVS)
			{
				Console.WriteLine ("Begining of slice number:" + counter);
                //Console.WriteLine(tmp.slice.GetLength(0));
                //Console.WriteLine(tmp.slice.GetLength(1));
                Console.WriteLine ("Starting Row: "+ tmp.startingRow+" Starting Column: "+ tmp.startingColumn+" Ending Row: "+ tmp.endingRow+ " Ending Column: "+ tmp.endingColumn);
				for (int i = tmp.startingRow; i < tmp.slice.GetLength (0); i++) {
					for (int j=tmp.startingColumn; j < tmp.slice.GetLength (1); j++) {
						Console.Write (tmp.slice[i,j]);
						Console.Write (' ');
					}
					Console.WriteLine ();
				}

				Console.WriteLine ("Ending of slice number:" + counter);
				Console.WriteLine ();
                
				counter++;
			}
            Console.ReadKey();
        }

//        public bool isValidSlice(char[,] slice, int pizzaRows, int pizzaColumns, int minimumIngredientsPerSlice, int maximumCellsPerSlice)
//        {
//            //if the slice cells exceed maximum cells per slice exit with false
//            if ((slice.GetLength(0) * slice.GetLength(1)) > maximumCellsPerSlice)
//                return false;
//
//            //if slice diementions does not fit in the pizza exit with false
//            if (slice.GetLength(0) > pizzaRows || slice.GetLength(1) > pizzaColumns)
//                return false;
//
//            //count number of tomatoes and number of mushrooms and if they both higher than the minimum number of ingredients per slice return true
//            int tomatoesCounter = 0;
//            int mushroomsCounter = 0;
//
//            for (int i = 0; i < slice.GetLength(0); i++)
//            {
//                for (int j = 0; j < slice.GetLength(1); j++)
//                {
//                    if (slice[i, j] == 'T')
//                        tomatoesCounter++;
//                    else if (slice[i, j] == 'M')
//                        mushroomsCounter++;
//                    else
//                        Console.WriteLine("unexpected char : "+slice[i,j]);
//
//                    if (tomatoesCounter >= minimumIngredientsPerSlice && mushroomsCounter >= minimumIngredientsPerSlice)
//                        return true;
//                }
//            }
//
//
//            if (tomatoesCounter >= minimumIngredientsPerSlice && mushroomsCounter >= minimumIngredientsPerSlice)
//                return true;
//            else
//                return false;
//        }

		public static char[,] isValidSlice( char [,] pizza, int startRow, int startColumn, int endRow, int endColumn,int minimumIngredientsPerSlice)
		{
			int tomatoesCounter = 0;
			int mushroomsCounter = 0;
			int rows = endRow + 1;
			int columns = endColumn + 1;
			char [,] validSlice = new char[rows,columns];
			for (int i=startRow; i<=endRow; i++) 
			{
				for (int j = startColumn; j <= endColumn; j++) 
				{
					if (pizza [i, j] == 'T') {
						validSlice [i, j] = pizza [i, j];
						tomatoesCounter++;
					} else if (pizza [i, j] == 'M') 
					{
						validSlice [i, j] = pizza [i, j];
						mushroomsCounter++;
					}
					else
						Console.WriteLine("unexpected char : "+pizza[i,j]);
				}
			}

			if (tomatoesCounter >= minimumIngredientsPerSlice && mushroomsCounter >= minimumIngredientsPerSlice)
				return validSlice;
			else
				return null;
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

		public static List<ValidSlice> createValidSlices(char[,] pizza, List<char[,]> possibleSlicesToCut, int minimumIngredientsPerSlice)
		{
			List<ValidSlice> validSlices = new List<ValidSlice>();

			int rowsOfPizza = pizza.GetLength (0);
			int columnsOfPizza = pizza.GetLength (1);

			foreach(char[,] slice in possibleSlicesToCut)
			{
				

				int rowsOfSlice = slice.GetLength (0);
				int columnsOfSlice = slice.GetLength (1);

				//the following two fors are moving the search window 
				for (int i=0;i<=(rowsOfPizza-rowsOfSlice); i++)
				{
					for (int j = 0;j <=(columnsOfPizza-columnsOfSlice); j++)
					{
						//check if it is a valid slice add it the ValidSlice list
						char[,] tmp = isValidSlice(pizza,i,j,rowsOfSlice-1+i,columnsOfSlice-1+j,minimumIngredientsPerSlice);
						if (tmp == null)
							continue;
						else 
						{
							ValidSlice validSlice = new ValidSlice ();
							validSlice.startingRow = i;
							validSlice.startingColumn = j;
							validSlice.endingRow = rowsOfSlice-1 + i;
							validSlice.endingColumn = columnsOfSlice-1 + j;
							validSlice.slice = tmp;

							validSlices.Add (validSlice);
						}
					}
				}
			}
			return validSlices;
		}

    }
}

