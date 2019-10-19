using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using ModelLayer;

namespace BusinessLayer
{
    public class CommonBL
    {
        CommonDL db = new CommonDL();

        public List<Country> BindCountry()
        {
            return db.BindCountry();
        }
        public List<State> BindState(int CountryID)
        {
            return db.BindState(CountryID);
        }

        public List<City> BindCity(int stateID)
        {
            return db.BindCity(stateID);
        }

        public List<Year> BindYear()
        {
            return db.BindYear();
        }

        public List<Month> BindMonth()
        {
            return db.BindMonth();
        }
    }
}
