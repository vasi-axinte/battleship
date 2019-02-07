using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Ui
{
    public class UserInterface
    {
        public void DisplayPanel(char[,] panel, int panelSize)
        {
            for (int i = 0; i < panelSize; i++)
            {
                for (int j = 0; j < panelSize; j++)
                {
                    Console.Write(panel[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        public void DisplayInfo(string info)
        {
            Console.WriteLine(info);
        }
    }
}
