using System;

namespace pizza
{
	public class ValidSlice
	{
		public ValidSlice ()
		{
		
		}

		public int startingRow {
			get;
			set;
		}

		public int startingColumn {
			get;
			set;
		}

		public int endingRow {
			get;
			set;
		}

		public int endingColumn {
			get;
			set;
		}

		public char [,] slice {
			get;
			set;
		}
	}
}

