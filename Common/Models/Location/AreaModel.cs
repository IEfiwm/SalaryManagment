using System.Collections.Generic;
using System.Linq;

namespace Common.Models.Location
{
    public class AreaModel
    {
        public List<CoordinateModel> Coordinates { get; set; }

        public bool IsValid(List<CoordinateModel> coordinates)
        {
            return coordinates.First() == coordinates.Last();
        }
    }
}
