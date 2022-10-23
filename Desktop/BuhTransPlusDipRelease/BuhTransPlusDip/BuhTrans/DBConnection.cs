using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace BuhTrans
{
    class DBConnection
    {
        //string nameServer = "DESKTOP-Q58E3C9\MSSQLSERVER01";
        public static SqlConnection connection = null;
        public static SqlCommand command = null;
        public static SqlDataAdapter adapter;
        public static SqlCommandBuilder commandBilder;
        public static string connectionString = @"Data Source = "+Properties.Settings.Default.serv+"; Initial Catalog = BuhTrans; Integrated Security = True;";
        public static string spGetUserData = "sp_GetUserData";
        public static string spInsertUserData = "sp_InsertUserData";
        public static string spGetAllUserDate = "sp_GetAllUserDate";
        public static string spUpdateUserData = "sp_UpdateUserData";
        public static string spDeleteUserDate = "sp_DeleteUserDate";
        public static string spInsertGuideCur = "sp_InsertGuideCur";
        public static string spGetAllGuideCur = "sp_GetAllGuideCur";
        public static string spUpdateGuideCur = "sp_UpdateGuideCur";
        public static string spDeleteGuideCur = "sp_DeleteGuideCur";
        public static string spInsertRate = "sp_InsertRate";
        public static string spGetAllRate = "sp_GetAllRate";
        public static string spUpdateRate = "sp_UpdateRate";
        public static string spDeleteRate = "sp_DeleteRate";
        public static string spInsertCar = "sp_InsertCar";
        public static string spGetAllCar = "sp_GetAllCar";        
        public static string spUpdateCar = "sp_UpdateCar";
        public static string spDeleteCar = "sp_DeleteCar";
        public static string spInsertEmployee = "sp_InsertEmployee";
        public static string spGetAllEmployee = "sp_GetAllEmployee";
        public static string spUpdateEmployee = "sp_UpdateEmployee";
        public static string spDeleteEmployee = "sp_DeleteEmployee";
        public static string spGetAllFuel = "sp_GetAllFuel";
        public static string spGetAllCar22 = "sp_GetAllCar22";
        public static string spGetAllDriver = "sp_GetAllDriver";
        public static string spGetAllItinerary = "sp_GetAllItinerary";
        public static string spGetAllRefil = "sp_GetAllRefil";
        public static string spInsertWaybill = "sp_InsertWaybill";
        public static string spInsertItinerary = "sp_InsertItinerary";
        public static string spInsertRefill = "sp_InsertRefill";
        public static string spUpdateRefill = "sp_UpdateRefill";
        public static string spUpdateItinerary = "sp_UpdateItinerary";
        public static string spUpdateWaybill = "sp_UpdateWaybill";
        public static string spDeleteRefill = "sp_DeleteRefill";
        public static string spDeleteItinerary = "sp_DeleteItinerary";
        public static string spGetAllWayList = "sp_GetAllWayList";
        public static string spPassDecrypt = "sp_PassDecrypt";
        public static string spGetAllRefilNumberWaybills = "sp_GetAllRefilNumberWaybills"; 
        public static string spGetAllNormFuel = "sp_GetAllNormFuel"; 
        public static string spUpdateFuelNorm = "sp_UpdateFuelNorm"; 
        public static string spGetAllWaybill = "sp_GetAllWaybill";
    }
}
