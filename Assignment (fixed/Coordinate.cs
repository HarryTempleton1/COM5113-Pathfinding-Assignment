using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment__fixed
{
    public readonly struct Coordinate
    {
        private readonly int _row, _col;

        //Constructor to initialise both fields
        public Coordinate(int row, int col)
        {
            this._row = row;
            this._col = col;
        }

        //Getters
        public int Row => _row;
        public int Col => _col;
        //returns string of coordinates for printing
        public string getCoordinate()
        {
            return $"Row: {_row}, Col: {_col}";
        }
    }
}
