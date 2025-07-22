using Ay.Utils;
using AY.Entity;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xbd.DataConvertLib;

namespace AY.BusinessServices
{
    public class HistoryDataService
    {
        public bool AddHisotryData(HistoryData historyData)
        {
            string sql = "INSERT INTO HistoryData (InsertTime, PressureIn, PressureOut, TempIn1, TempIn2, TempOut, PressureTank1, PressureTank2, LevelTank1, LevelTank2, PressureTankOut) " +
                         "VALUES (@InsertTime, @PressureIn, @PressureOut, @TempIn1, @TempIn2, @TempOut, @PressureTank1, @PressureTank2, @LevelTank1, @LevelTank2, @PressureTankOut)";
            SQLiteParameter[] parameters = new SQLiteParameter[]
            {
                new SQLiteParameter("@InsertTime", historyData.InsertTime.ToString("yyyy-MM-dd HH:mm:ss")),
                new SQLiteParameter("@PressureIn", historyData.PressureIn),
                new SQLiteParameter("@PressureOut", historyData.PressureOut),
                new SQLiteParameter("@TempIn1", historyData.TempIn1),
                new SQLiteParameter("@TempIn2", historyData.TempIn2),
                new SQLiteParameter("@TempOut", historyData.TempOut),
                new SQLiteParameter("@PressureTank1", historyData.PressureTank1),
                new SQLiteParameter("@PressureTank2", historyData.PressureTank2),
                new SQLiteParameter("@LevelTank1", historyData.LevelTank1),
                new SQLiteParameter("@LevelTank2", historyData.LevelTank2),
                new SQLiteParameter("@PressureTankOut", historyData.PressureTankOut)
            };

            return SQLiteHelper.ExecuteNonQuery(sql, parameters) > 0;


        }

        public OperateResult<List<HistoryData>> GetHisotryDataByTime(DateTime start, DateTime end)
        {
            string sql = "SELECT * FROM HistoryData WHERE InsertTime BETWEEN @Start AND @End";
            SQLiteParameter[] parameters = new SQLiteParameter[]
            {
                new SQLiteParameter("@Start", start.ToString("yyyy-MM-dd HH:mm:ss")),
                new SQLiteParameter("@End", end.ToString("yyyy-MM-dd HH:mm:ss"))
            };
            try
            {


                SQLiteDataReader dataReader = SQLiteHelper.ExecuteReader(sql, parameters);
                List<HistoryData> historyDataList = new List<HistoryData>();
                while (dataReader.Read())
                {
                    HistoryData historyData = new HistoryData()
                    {
                        InsertTime = DateTime.Parse(dataReader["InsertTime"].ToString()),
                        PressureIn = dataReader["PressureIn"].ToString(),
                        PressureOut = dataReader["PressureOut"].ToString(),
                        TempIn1 = dataReader["TempIn1"].ToString(),
                        TempIn2 = dataReader["TempIn2"].ToString(),
                        TempOut = dataReader["TempOut"].ToString(),
                        PressureTank1 = dataReader["PressureTank1"].ToString(),
                        PressureTank2 = dataReader["PressureTank2"].ToString(),
                        LevelTank1 = dataReader["LevelTank1"].ToString(),
                        LevelTank2 = dataReader["LevelTank2"].ToString(),
                        PressureTankOut = dataReader["PressureTankOut"].ToString()
                    };
                    historyDataList.Add(historyData);

                }
                dataReader.Close();
                return OperateResult.CreateSuccessResult(historyDataList);
            }
            catch (Exception ex)
            {
                return OperateResult.CreateFailResult<List<HistoryData>>(ex.Message);
            }
        }
    }
}