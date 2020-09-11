﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NavebsiteDAL;

namespace NavebsiteBL
{
    public class Sales
    {
        public DateTime Date { get; set; }
        public int Purchases { get; set; }
        public double Revenue { get; set; }

        public Sales(DataRow dr)
        {
            Date = (DateTime)dr["Timestamp"];
            Purchases = (int)dr["Purchases"];
            Revenue = (double)dr["Revenue"];
        }

        private static List<Sales> DataTableToList(DataTable tb)
        {
            List<Sales> list = new List<Sales>();
            foreach(DataRow dr in tb.Rows)
            {
                list.Add(new Sales(dr));
            }
            return list;
        }

        public static DataTable GameStatsTable(int gameId)
        {
            return (DBStats.GameSalesStats(gameId));
        }

        public static DataTable CompanyStatsTable(int devId)
        {
            return (DBStats.CompanySalesStats(devId));
        }

        public static DataTable AllStatsTable()
        {
            return (DBStats.TotalSalesStats());

        }

        public static List<Sales> GameStats(int gameId)
        {
            return DataTableToList(DBStats.GameSalesStats(gameId));
        }

        public static List<Sales> CompanyStats(int devId)
        {
            return DataTableToList(DBStats.CompanySalesStats(devId));
        }

        public static List<Sales> AllStats()
        {
            return DataTableToList(DBStats.TotalSalesStats());
        }
    }
}
