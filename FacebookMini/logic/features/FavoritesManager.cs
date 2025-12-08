using System;
using System.Collections.Generic;

namespace FacebookMini.Logic
{
    internal class FavoritesManager
    {
        private readonly Dictionary<eFavoritesCategory, List<object>> r_FavoritesLists = new Dictionary<eFavoritesCategory, List<object>>();

        public List<object> GetList(eFavoritesCategory i_Category)
        {
            return getOrCreateList(i_Category);
        }

        public bool Add(object i_Item, eFavoritesCategory i_Category)
        {
            List<object> list = getOrCreateList(i_Category);

            if (list.Contains(i_Item))
            {
                return false;
            }

            list.Add(i_Item);
            return true;
        }

        public bool Remove(object i_Item, eFavoritesCategory i_Category)
        {
            if (r_FavoritesLists.TryGetValue(i_Category, out List<object> list) == false)
            {
                return false;
            }

            return list.Remove(i_Item);
        }

        public bool Contains(object i_Item, eFavoritesCategory i_Category)
        {
            if(r_FavoritesLists.TryGetValue(i_Category, out List<object> list) == true && list.Contains(i_Item) == true) 
            {
                return true;
            }

            return false;
        }

        private List<object> getOrCreateList(eFavoritesCategory i_Category)
        {
            if (r_FavoritesLists.TryGetValue(i_Category, out List<object> list) == false)
            {
                list = new List<object>();
                r_FavoritesLists[i_Category] = list;
            }

            return list;
        }
    }
}
