using ApiSandbox.DataAccess;
using ApiSandbox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSandbox.Repositories
{
    public class SquaresRepository
    {
        private SquaresDataAccess squaresDA;

        public SquaresRepository()
        {
            squaresDA = new SquaresDataAccess();
        }


        public SquareName[] GetSquareNames()
        {
            return squaresDA.sNames;
        }

        public void SaveSquareNames(SquareName[] squareNames)
        {
            squaresDA.SaveSquareNames(squareNames);
        }
    }
}
