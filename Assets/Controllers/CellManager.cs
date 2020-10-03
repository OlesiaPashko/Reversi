using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Controllers
{
    public class CellManager : MonoBehaviour
    {
        public int x;
        public int y;
        public bool isAvailable;

        public void Init(int x, int y, bool isAvailable)
        {
            this.x = x;
            this.y = y;
            this.isAvailable = isAvailable;
        }
    }
}
