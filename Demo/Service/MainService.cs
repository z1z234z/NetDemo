using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Dao;
using Demo.Models;

namespace Demo.Service
{
    public class MainService
    {
        private readonly LoseTypesDao loseTypesDao;

        private readonly OwnerDao ownerDao;

        private readonly FinderDao finderDao;

        public MainService(DBContext context)
        {
            loseTypesDao = new LoseTypesDao(context);
            ownerDao = new OwnerDao(context);
            finderDao = new FinderDao(context);
        }

        public List<LoseType> GetLoseTypes()
        {
            return loseTypesDao.Select(null, null);
        }

        public List<Owner> GetOwnerByType(String type, int index)
        {
            LoseType loseType = null;
            var items = loseTypesDao.Select(null, type);
            if (items.Count == 1)
            {
                foreach (LoseType item in items)
                {
                    loseType = item;
                }
                return ownerDao.Select(null, null, null, null, null, null, loseType, null, index);
            }
            else
            {
                return ownerDao.Select(null, null, null, null, null, null, null, null, index);
            }
        }

        public List<Finder> GetFinderByType(String type, int index)
        {
            LoseType loseType = null;
            var items = loseTypesDao.Select(null, type);
            if (items.Count == 1)
            {
                foreach (LoseType item in items)
                {
                    loseType = item;
                }
                return finderDao.Select(null, null, null, null, null, null, null, null, loseType, null, index);
            }
            else
            {
                return finderDao.Select(null, null, null, null, null, null, null, null, null, null, index);
            }
        }
    }
}
